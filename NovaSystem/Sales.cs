using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace NovaSystem
{
    public partial class Sales : Form
    {
        private decimal cartTotal = 0;
        private int lastSaleID = 0;
        private PrintDocument receiptDocument = new PrintDocument();
        private PrintPreviewDialog receiptPreview = new PrintPreviewDialog();

        // Optimized connection strings with Timeout
        private readonly string devConnectionString = @"Data Source=DESKTOP-60H5VBK\MSSQLSERVER01; Initial Catalog=NovasystemDB; Integrated Security=True; TrustServerCertificate=True; Connect Timeout=3;";
        private readonly string clientConnectionString = @"Data Source=.\SQLEXPRESS; Initial Catalog=NovasystemDB; Integrated Security=True; TrustServerCertificate=True; Connect Timeout=3;";
        private string activeConnectionString;

        public Sales()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            activeConnectionString = ResolveConnectionString();
            InitializeCartGrid();
            InitializeSearchGrid();

            receiptDocument.PrintPage += DrawReceipt;
            txtCash.TextChanged += TxtCash_TextChanged;
        }

        private string ResolveConnectionString() => TestConnection(devConnectionString) ? devConnectionString : clientConnectionString;
        private bool TestConnection(string cs) { try { using (var c = new SqlConnection(cs)) { c.Open(); return true; } } catch { return false; } }

        private void InitializeSearchGrid()
        {
            dataGridViewSales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewSales.ReadOnly = true;
            dataGridViewSales.RowHeadersVisible = false;
        }

        private void InitializeCartGrid()
        {
            dgvCart.Columns.Clear();
            dgvCart.Columns.Add("ProductType", "Type");
            dgvCart.Columns.Add("Category", "Category");
            dgvCart.Columns.Add("ProductName", "Product");
            dgvCart.Columns.Add("SKU", "SKU");
            dgvCart.Columns.Add("SellingPrice", "Price");
            dgvCart.Columns.Add("ProductID", "PID");
            dgvCart.Columns["ProductID"].Visible = false;
            dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            this.Cursor = Cursors.WaitCursor;

            try
            {
                DataTable dt = await Task.Run(() => {
                    string query = @"
                        SELECT 'Cosmetic' AS ProductType, C.CosmeticID AS ItemID, P.ProductID, 
                               P.ProductName, P.Category, C.SKU, P.SellingPrice
                        FROM Cosmetics C JOIN Products P ON C.ProductID = P.ProductID
                        WHERE C.Status = 'Available' AND (P.ProductName LIKE @K OR P.Category LIKE @K OR C.SKU LIKE @K)
                        UNION ALL
                        SELECT 'Shoe' AS ProductType, S.ShoeID AS ItemID, P.ProductID, 
                               P.ProductName, P.Category, S.SKU, P.SellingPrice
                        FROM Shoes S JOIN Products P ON S.ProductID = P.ProductID
                        WHERE S.Status = 'Available' AND (P.ProductName LIKE @K OR P.Category LIKE @K OR S.SKU LIKE @K)";

                    using (SqlConnection conn = new SqlConnection(activeConnectionString))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(query, conn);
                        da.SelectCommand.Parameters.AddWithValue("@K", $"%{keyword}%");
                        DataTable tempTable = new DataTable();
                        da.Fill(tempTable);
                        return tempTable;
                    }
                });

                dataGridViewSales.DataSource = dt;
                if (dt.Columns.Contains("ItemID")) dataGridViewSales.Columns["ItemID"].Visible = false;
                if (dt.Columns.Contains("ProductID")) dataGridViewSales.Columns["ProductID"].Visible = false;
            }
            catch (Exception ex) { MessageBox.Show("Search Error: " + ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void dataGridViewSales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dataGridViewSales.Rows[e.RowIndex];

            string sku = row.Cells["SKU"].Value.ToString();
            // Check if already in cart
            foreach (DataGridViewRow r in dgvCart.Rows)
                if (r.Cells["SKU"].Value?.ToString() == sku) return;

            decimal price = Convert.ToDecimal(row.Cells["SellingPrice"].Value);
            dgvCart.Rows.Add(
                row.Cells["ProductType"].Value,
                row.Cells["Category"].Value,
                row.Cells["ProductName"].Value,
                sku,
                price,
                row.Cells["ProductID"].Value
            );

            cartTotal += price;
            lblTotalAmount.Text = cartTotal.ToString("N2");
            TxtCash_TextChanged(null, null);
        }

        private async void BtnCheckout_Click(object sender, EventArgs e)
        {
            if (dgvCart.Rows.Count == 0) return;
            if (!decimal.TryParse(txtCash.Text, out decimal cashValue) || cashValue < cartTotal)
            {
                MessageBox.Show("Invalid cash amount.");
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            bool success = await SaveTransactionAsync();
            this.Cursor = Cursors.Default;

            if (success)
            {
                receiptPreview.Document = receiptDocument;
                receiptPreview.ShowDialog();
                ResetCartUI();
                MessageBox.Show("Transaction Complete.");
            }
        }

        private async Task<bool> SaveTransactionAsync()
        {
            using (SqlConnection conn = new SqlConnection(activeConnectionString))
            {
                await conn.OpenAsync();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Insert Sale record
                        string salesSql = "INSERT INTO Sales (UserID, TotalAmount, PaymentMethod, Status, SaleDate) VALUES (@UID, @Tot, 'Cash', 'Completed', GETDATE()); SELECT SCOPE_IDENTITY();";
                        SqlCommand cmd = new SqlCommand(salesSql, conn, trans);
                        // If UserId is 0 (not logged in properly), you might want to send DBNull, 
                        // otherwise just send the ID.
                        cmd.Parameters.AddWithValue("@UID", UserSession.UserId == 0 ? (object)DBNull.Value : UserSession.UserId);
                        cmd.Parameters.AddWithValue("@Tot", cartTotal);
                        lastSaleID = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                        // 2. Loop items (Save items AND Update Status in ONE Transaction)
                        foreach (DataGridViewRow row in dgvCart.Rows)
                        {
                            string itemSql = @"INSERT INTO SaleItems (SaleID, ProductID, ProductName, ProductType, SKU, Quantity, UnitPrice) 
                                               VALUES (@SID, @PID, @PName, @Type, @SKU, 1, @Price)";
                            SqlCommand itemCmd = new SqlCommand(itemSql, conn, trans);
                            itemCmd.Parameters.AddWithValue("@SID", lastSaleID);
                            itemCmd.Parameters.AddWithValue("@PID", row.Cells["ProductID"].Value);
                            itemCmd.Parameters.AddWithValue("@PName", row.Cells["ProductName"].Value);
                            itemCmd.Parameters.AddWithValue("@Type", row.Cells["ProductType"].Value);
                            itemCmd.Parameters.AddWithValue("@SKU", row.Cells["SKU"].Value);
                            itemCmd.Parameters.AddWithValue("@Price", row.Cells["SellingPrice"].Value);
                            await itemCmd.ExecuteNonQueryAsync();

                            // Update Stock Status
                            string table = row.Cells["ProductType"].Value.ToString() == "Cosmetic" ? "Cosmetics" : "Shoes";
                            string updateSql = $"UPDATE {table} SET Status = 'Sold' WHERE SKU = @SKU";
                            SqlCommand updateCmd = new SqlCommand(updateSql, conn, trans);
                            updateCmd.Parameters.AddWithValue("@SKU", row.Cells["SKU"].Value);
                            await updateCmd.ExecuteNonQueryAsync();
                        }

                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show("DB Error: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        private void DrawReceipt(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font fRegular = new Font("Courier New", 9);
            Font fBold = new Font("Courier New", 10, FontStyle.Bold);
            float y = 20; int margin = 20; int width = 280;

            g.DrawString("GOLDEN ANGLES AFRICA", new Font("Courier New", 14, FontStyle.Bold), Brushes.Black, 20, y);
            y += 40;
            g.DrawString($"Receipt: #NS-{lastSaleID:D6}", fBold, Brushes.Black, margin, y);
            y += 20;
            g.DrawString(new string('-', 40), fRegular, Brushes.Black, margin, y);
            y += 20;

            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                string name = row.Cells["ProductName"].Value.ToString();
                string price = Convert.ToDecimal(row.Cells["SellingPrice"].Value).ToString("N2");
                g.DrawString(name.Length > 25 ? name.Substring(0, 23) + ".." : name, fRegular, Brushes.Black, margin, y);
                g.DrawString(price, fRegular, Brushes.Black, width, y, new StringFormat { Alignment = StringAlignment.Far });
                y += 20;
            }

            y += 10;
            g.DrawString(new string('-', 40), fRegular, Brushes.Black, margin, y);
            y += 20;
            g.DrawString("TOTAL:", fBold, Brushes.Black, margin, y);
            g.DrawString(cartTotal.ToString("N2"), fBold, Brushes.Black, width, y, new StringFormat { Alignment = StringAlignment.Far });
        }

        private void TxtCash_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtCash.Text, out decimal cash))
                txtBalance.Text = (cash - cartTotal).ToString("N2");
            else
                txtBalance.Text = "0.00";
        }

        private void ResetCartUI()
        {
            dgvCart.Rows.Clear();
            cartTotal = 0;
            lblTotalAmount.Text = "0.00";
            txtCash.Clear();
            txtBalance.Text = "0.00";
            dataGridViewSales.DataSource = null;
        }

        private void SmoothNavigate(Form nextForm) { nextForm.Show(); this.Hide(); }
        private void btnHome_Click(object sender, EventArgs e) => SmoothNavigate(new Form2());
        private void BtnInventory_Click(object sender, EventArgs e) => SmoothNavigate(new Inventory());
        // Fixes: Sales_Load
        private void Sales_Load(object sender, EventArgs e)
        {
            LoginTag.Text = !string.IsNullOrWhiteSpace(UserSession.Fullname)
                ? $"Logged in: {UserSession.Fullname}" : $"Logged in as: {UserSession.Username}";
        }

        // Fixes: BtnRemove_Click
        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count > 0)
            {
                var row = dgvCart.SelectedRows[0];
                decimal price = Convert.ToDecimal(row.Cells["SellingPrice"].Value);

                cartTotal -= price;
                lblTotalAmount.Text = cartTotal.ToString("N2");
                dgvCart.Rows.Remove(row);
                TxtCash_TextChanged(null, null);
            }
        }

        // Fixes: btnClearcart_Click
        private void btnClearcart_Click(object sender, EventArgs e)
        {
            ResetCartUI();
        }

        // Fixes: BtnConfirm_Click (Points to your existing checkout logic)
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            BtnCheckout_Click(sender, e);
        }

        // Fixes: Navigation Errors
        private void BtnCosmetics_Click(object sender, EventArgs e) => SmoothNavigate(new Cosmetics());
        private void BtnReports_Click(object sender, EventArgs e) => SmoothNavigate(new reports());

        // Fixes: Placeholder clicks (usually from double-clicking labels by accident)
        private void label2_Click(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
    }
}