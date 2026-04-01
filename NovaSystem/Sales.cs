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
        private readonly PrintDocument receiptDocument = new PrintDocument();
        private readonly PrintPreviewDialog receiptPreview = new PrintPreviewDialog();

        // Faster timeout (2s) to prevent long hangs
        private readonly string devConnectionString = @"Data Source=DESKTOP-60H5VBK\MSSQLSERVER01; Initial Catalog=NovasystemDB; Integrated Security=True; TrustServerCertificate=True; Connect Timeout=2; Pooling=True;";
        private readonly string clientConnectionString = @"Data Source=.\SQLEXPRESS; Initial Catalog=NovasystemDB; Integrated Security=True; TrustServerCertificate=True; Connect Timeout=2; Pooling=True;";

        // Static persists across form instances to prevent re-testing connection every time
        private static string cachedConnectionString;

        public Sales()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            InitializeCartGrid();
            InitializeSearchGrid();

            receiptDocument.PrintPage += DrawReceipt;
            txtCash.TextChanged += TxtCash_TextChanged;
            dataGridViewSales.CellClick += dataGridViewSales_CellClick;
        }

        private async void Sales_Load(object sender, EventArgs e)
        {
            LoginTag.Text = $"Logged in: {UserSession.Fullname ?? UserSession.Username}";

            // FIX: Resolve connection in a background task so the Form shows up INSTANTLY
            if (string.IsNullOrEmpty(cachedConnectionString))
            {
                this.Cursor = Cursors.WaitCursor;
                cachedConnectionString = await Task.Run(() => ResolveConnectionString());
                this.Cursor = Cursors.Default;
            }
        }

        private string ResolveConnectionString()
        {
            // Test Dev first, then fallback
            if (TestConnection(devConnectionString)) return devConnectionString;
            return clientConnectionString;
        }

        private bool TestConnection(string cs)
        {
            try
            {
                using (var c = new SqlConnection(cs))
                {
                    c.Open();
                    return true;
                }
            }
            catch { return false; }
        }

        // ========================= SEARCH ENGINE =========================

        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cachedConnectionString)) return;

            string keyword = txtSearch.Text.Trim();
            this.Cursor = Cursors.WaitCursor;
            try
            {
                DataTable dt = await Task.Run(() => {
                    // Optimized query: Only select what you need
                    string query = @"
                        SELECT 'Cosmetic' AS ProductType, P.ProductID, P.ProductName, P.Category, C.SKU, P.SellingPrice
                        FROM Cosmetics C JOIN Products P ON C.ProductID = P.ProductID
                        WHERE C.Status = 'Available' AND (P.ProductName LIKE @K OR P.Category LIKE @K OR C.SKU LIKE @K)
                        UNION ALL
                        SELECT 'Shoe' AS ProductType, P.ProductID, P.ProductName, P.Category, S.SKU, P.SellingPrice
                        FROM Shoes S JOIN Products P ON S.ProductID = P.ProductID
                        WHERE S.Status = 'Available' AND (P.ProductName LIKE @K OR P.Category LIKE @K OR S.SKU LIKE @K)";

                    using (SqlConnection conn = new SqlConnection(cachedConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@K", $"%{keyword}%");
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable tempTable = new DataTable();
                            da.Fill(tempTable);
                            return tempTable;
                        }
                    }
                });

                dataGridViewSales.DataSource = dt;
                if (dataGridViewSales.Columns.Contains("ProductID"))
                    dataGridViewSales.Columns["ProductID"].Visible = false;
            }
            catch (Exception ex) { MessageBox.Show("Search Error: " + ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }

        // ========================= TRANSACTION ENGINE =========================

        private async void BtnCheckout_Click(object sender, EventArgs e)
        {
            if (dgvCart.Rows.Count == 0) return;
            if (!decimal.TryParse(txtCash.Text, out decimal cashValue) || cashValue < cartTotal)
            {
                MessageBox.Show("Please enter a valid cash amount equal to or greater than total.");
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            bool success = await SaveTransactionAsync();
            this.Cursor = Cursors.Default;

            if (success)
            {
                ShowSuccessFeedback();
                receiptPreview.Document = receiptDocument;
                receiptPreview.WindowState = FormWindowState.Maximized; // Better view
                receiptPreview.ShowDialog();
                ResetCartUI();
            }
        }

        private async Task<bool> SaveTransactionAsync()
        {
            using (SqlConnection conn = new SqlConnection(cachedConnectionString))
            {
                await conn.OpenAsync();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Insert Master Sale record
                        string salesSql = "INSERT INTO Sales (UserID, TotalAmount, PaymentMethod, Status, SaleDate) VALUES (@UID, @Tot, 'Cash', 'Completed', GETDATE()); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmd = new SqlCommand(salesSql, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@UID", UserSession.UserId == 0 ? (object)DBNull.Value : UserSession.UserId);
                            cmd.Parameters.AddWithValue("@Tot", cartTotal);
                            lastSaleID = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                        }

                        // 2. Insert Items and Update Stock
                        foreach (DataGridViewRow row in dgvCart.Rows)
                        {
                            int pid = Convert.ToInt32(row.Cells["ProductID"].Value);
                            string sku = row.Cells["SKU"].Value.ToString();
                            string type = row.Cells["ProductType"].Value.ToString();

                            string itemSql = "INSERT INTO SaleItems (SaleID, ProductID, ProductName, ProductType, SKU, Quantity, UnitPrice, UnitID) VALUES (@SID, @PID, @PName, @Type, @SKU, 1, @Price, 1)";
                            using (SqlCommand itemCmd = new SqlCommand(itemSql, conn, trans))
                            {
                                itemCmd.Parameters.AddWithValue("@SID", lastSaleID);
                                itemCmd.Parameters.AddWithValue("@PID", pid);
                                itemCmd.Parameters.AddWithValue("@PName", row.Cells["ProductName"].Value);
                                itemCmd.Parameters.AddWithValue("@Type", type);
                                itemCmd.Parameters.AddWithValue("@SKU", sku);
                                itemCmd.Parameters.AddWithValue("@Price", row.Cells["SellingPrice"].Value);
                                await itemCmd.ExecuteNonQueryAsync();
                            }

                            // Update individual item status
                            string table = (type == "Cosmetic") ? "Cosmetics" : "Shoes";
                            using (SqlCommand updateCmd = new SqlCommand($"UPDATE {table} SET Status = 'Sold' WHERE SKU = @SKU", conn, trans))
                            {
                                updateCmd.Parameters.AddWithValue("@SKU", sku);
                                await updateCmd.ExecuteNonQueryAsync();
                            }

                            // Update product aggregate quantity
                            using (SqlCommand invCmd = new SqlCommand("UPDATE Products SET Quantity = Quantity - 1 WHERE ProductID = @PID AND Quantity > 0", conn, trans))
                            {
                                invCmd.Parameters.AddWithValue("@PID", pid);
                                await invCmd.ExecuteNonQueryAsync();
                            }
                        }
                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show("Transaction Failed: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        // ========================= PRINTING & UI HELPERS =========================

        private void DrawReceipt(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            using (Font fTitle = new Font("Segoe UI", 14, FontStyle.Bold))
            using (Font fReg = new Font("Segoe UI", 9))
            using (Font fBold = new Font("Segoe UI", 9, FontStyle.Bold))
            using (Font fSmall = new Font("Segoe UI", 8))
            {
                float y = 20;
                int margin = 20;
                int width = 280;
                StringFormat center = new StringFormat { Alignment = StringAlignment.Center };
                StringFormat right = new StringFormat { Alignment = StringAlignment.Far };

                g.DrawString("GOLDEN ANGLES AFRICA", fTitle, Brushes.Black, new RectangleF(0, y, width + 40, 30), center);
                y += 25;
                g.DrawString("Tel: 0714484838 / 0116923796", fSmall, Brushes.Black, new RectangleF(0, y, width + 40, 20), center);
                y += 15;
                g.DrawString("Location: Nyamakima CBC Plaza, 3rd Floor, Room T7", fSmall, Brushes.Black, new RectangleF(0, y, width + 40, 20), center);
                y += 25;

                g.DrawRectangle(Pens.Black, margin, y, width - 20, 35);
                g.DrawString("PAYBILL: 542542", fBold, Brushes.Black, margin + 5, y + 5);
                g.DrawString("ACC NO: 609371", fBold, Brushes.Black, margin + 5, y + 18);
                y += 45;

                g.DrawString($"Receipt: #NS-{lastSaleID:D6}", fReg, Brushes.Black, margin, y);
                y += 15;
                g.DrawString($"Date: {DateTime.Now:G}", fReg, Brushes.Black, margin, y);
                y += 20;
                g.DrawLine(Pens.Black, margin, y, width, y);
                y += 5;

                foreach (DataGridViewRow row in dgvCart.Rows)
                {
                    string name = row.Cells["ProductName"].Value.ToString();
                    string price = Convert.ToDecimal(row.Cells["SellingPrice"].Value).ToString("N2");
                    g.DrawString(name.Length > 22 ? name.Substring(0, 20) + ".." : name, fReg, Brushes.Black, margin, y);
                    g.DrawString(price, fReg, Brushes.Black, width, y, right);
                    y += 18;
                }

                y += 10;
                g.DrawLine(Pens.Black, margin, y, width, y);
                y += 10;

                decimal.TryParse(txtCash.Text, out decimal cash);
                g.DrawString("TOTAL AMOUNT:", fBold, Brushes.Black, margin, y);
                g.DrawString(cartTotal.ToString("N2"), fBold, Brushes.Black, width, y, right);
                y += 20;
                g.DrawString("CASH PAID:", fReg, Brushes.Black, margin, y);
                g.DrawString(cash.ToString("N2"), fReg, Brushes.Black, width, y, right);
                y += 15;
                g.DrawString("BALANCE:", fBold, Brushes.Black, margin, y);
                g.DrawString((cash - cartTotal).ToString("N2"), fBold, Brushes.Black, width, y, right);

                y += 40;
                g.DrawString("Thank you for your patronage!", fReg, Brushes.Black, new RectangleF(0, y, width + 40, 20), center);
            }
        }

        private void InitializeSearchGrid()
        {
            dataGridViewSales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewSales.ReadOnly = true;
            dataGridViewSales.RowHeadersVisible = false;
            dataGridViewSales.AllowUserToAddRows = false;
            dataGridViewSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
            dgvCart.Columns["SellingPrice"].DefaultCellStyle.Format = "N2";
            dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCart.AllowUserToAddRows = false;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void CalculateTotal()
        {
            cartTotal = 0;
            foreach (DataGridViewRow row in dgvCart.Rows)
                cartTotal += Convert.ToDecimal(row.Cells["SellingPrice"].Value);

            lblTotalAmount.Text = cartTotal.ToString("N2");
            TxtCash_TextChanged(null, null);
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
            txtSearch.Clear();
        }

        private void dataGridViewSales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                DataGridViewRow selectedRow = dataGridViewSales.Rows[e.RowIndex];
                string sku = selectedRow.Cells["SKU"].Value.ToString();

                foreach (DataGridViewRow r in dgvCart.Rows)
                    if (r.Cells["SKU"].Value?.ToString() == sku) { MessageBox.Show("Item already in cart."); return; }

                dgvCart.Rows.Add(selectedRow.Cells["ProductType"].Value, selectedRow.Cells["Category"].Value, selectedRow.Cells["ProductName"].Value, sku, selectedRow.Cells["SellingPrice"].Value, selectedRow.Cells["ProductID"].Value);
                CalculateTotal();
            }
            catch (Exception ex) { MessageBox.Show("Cart Error: " + ex.Message); }
        }

        private async void ShowSuccessFeedback()
        {
            Panel toast = new Panel { Size = new Size(350, 60), BackColor = Color.FromArgb(40, 167, 69), Location = new Point((this.Width - 350) / 2, -70), BorderStyle = BorderStyle.None };
            Label msg = new Label { Text = "✔ TRANSACTION COMPLETED", ForeColor = Color.White, Font = new Font("Segoe UI", 11, FontStyle.Bold), TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill };
            toast.Controls.Add(msg); this.Controls.Add(toast); toast.BringToFront();

            for (int i = -70; i <= 20; i += 5) { toast.Location = new Point(toast.Location.X, i); await Task.Delay(10); }
            lblTotalAmount.ForeColor = Color.LimeGreen;
            await Task.Delay(1500);
            lblTotalAmount.ForeColor = Color.Black;
            for (int i = 20; i >= -70; i -= 5) { toast.Location = new Point(toast.Location.X, i); await Task.Delay(10); }
            this.Controls.Remove(toast); toast.Dispose();
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvCart.SelectedRows) dgvCart.Rows.Remove(row);
                CalculateTotal();
            }
        }

        private void btnClearcart_Click(object sender, EventArgs e) => ResetCartUI();
        private void BtnConfirm_Click(object sender, EventArgs e) => BtnCheckout_Click(sender, e);
        private void SmoothNavigate(Form f) { f.Show(); this.Hide(); }
        private void btnHome_Click(object sender, EventArgs e) => SmoothNavigate(new Form2());
        private void BtnInventory_Click(object sender, EventArgs e) => SmoothNavigate(new Inventory());
        private void BtnCosmetics_Click(object sender, EventArgs e) => SmoothNavigate(new Cosmetics());
        private void BtnReports_Click(object sender, EventArgs e) => SmoothNavigate(new reports());
        private void label2_Click(object sender, EventArgs e) { /* Not used */ }
        private void textBox2_TextChanged(object sender, EventArgs e) { /* Not used */ }
    }
}