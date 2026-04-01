using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace NovaSystem
{
    public partial class Orders : Form
    {
        // 1. RAM OPTIMIZATION: Small packet size and static connection resolution
        private const string ConnectionTuning = "Pooling=true;Max Pool Size=10;Connect Timeout=3;Packet Size=4096;";
        private readonly string devConnectionString = $@"Data Source=DESKTOP-60H5VBK\MSSQLSERVER01; Initial Catalog=NovasystemDB; Integrated Security=True; TrustServerCertificate=True;{ConnectionTuning}";
        private readonly string clientConnectionString = $@"Data Source=.\SQLEXPRESS; Initial Catalog=NovasystemDB; Integrated Security=True; TrustServerCertificate=True;{ConnectionTuning}";

        private static string activeConnectionString;
        private string currentReceiptID = "0";

        public Orders()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            // CRITICAL: Setup columns immediately in Constructor to avoid "No Columns" error
            SetupCartGrid();

            // Resolve once per app session
            if (activeConnectionString == null) activeConnectionString = ResolveConnectionString();

            txtAmount.TextChanged += TxtAmount_TextChanged;
        }

        private string ResolveConnectionString()
        {
            if (TestConnection(devConnectionString)) return devConnectionString;
            return TestConnection(clientConnectionString) ? clientConnectionString : null;
        }

        private bool TestConnection(string cs)
        {
            try { using (SqlConnection c = new SqlConnection(cs)) { c.Open(); return true; } }
            catch { return false; }
        }

        // ================= LOAD & INITIALIZATION =================
        private async void Orders_Load(object sender, EventArgs e)
        {
            try
            {
                // Parallel load for snappy startup
                await Task.WhenAll(LoadCategoriesAsync(), RefreshOrderHistoryAsync());
                CalculateGrandTotal();
            }
            catch { /* Silent fail to keep UI responsive */ }
        }

        private void SetupCartGrid()
        {
            dgvCart.Columns.Clear();
            dgvCart.Columns.Add("ProductID", "ID");
            dgvCart.Columns.Add("ProductName", "Product");
            dgvCart.Columns.Add("ProductType", "Category");
            dgvCart.Columns.Add("Quantity", "Qty");
            dgvCart.Columns.Add("UnitPrice", "Price");
            dgvCart.Columns.Add("TotalPrice", "Total");

            if (dgvCart.Columns.Contains("ProductID")) dgvCart.Columns["ProductID"].Visible = false;

            dgvCart.Columns["UnitPrice"].DefaultCellStyle.Format = "N2";
            dgvCart.Columns["TotalPrice"].DefaultCellStyle.Format = "N2";
            dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCart.RowHeadersVisible = false;
            dgvCart.AllowUserToAddRows = false;
        }

        private async Task RefreshOrderHistoryAsync()
        {
            if (string.IsNullOrEmpty(activeConnectionString)) return;
            try
            {
                using (var conn = new SqlConnection(activeConnectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT TOP 50 OrderID, OrderDate, StaffName AS [Salesperson], TotalAmount FROM Orders WITH (NOLOCK) ORDER BY OrderID DESC";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    await Task.Run(() => da.Fill(dt));
                    dataGridView1.DataSource = dt;
                }
            }
            catch { }
        }

        // ================= DYNAMIC COMBOBOX LOGIC =================
        private async Task LoadCategoriesAsync()
        {
            ComboCategory.Items.Clear();
            if (string.IsNullOrEmpty(activeConnectionString)) return;
            try
            {
                using (var conn = new SqlConnection(activeConnectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT DISTINCT Category FROM Products WITH (NOLOCK) WHERE Category IS NOT NULL ORDER BY Category ASC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync()) ComboCategory.Items.Add(reader["Category"].ToString());
                    }
                }
            }
            catch { }
        }

        private async void ComboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbBoxProdName.Items.Clear();
            string selectedCat = ComboCategory.Text.Trim();
            if (string.IsNullOrEmpty(selectedCat) || string.IsNullOrEmpty(activeConnectionString)) return;

            try
            {
                using (var conn = new SqlConnection(activeConnectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT DISTINCT ProductName FROM Products WITH (NOLOCK) WHERE Category = @cat ORDER BY ProductName ASC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@cat", selectedCat);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync()) cmbBoxProdName.Items.Add(reader["ProductName"].ToString());
                        }
                    }
                }
            }
            catch { }
        }

        // ================= CART & CHECKOUT =================
        private async void BtnSave_Click(object sender, EventArgs e)
        {
            string prod = cmbBoxProdName.Text.Trim();
            if (string.IsNullOrWhiteSpace(prod) || !int.TryParse(txtQuantity.Text, out int reqQty) || string.IsNullOrEmpty(activeConnectionString)) return;

            try
            {
                using (var conn = new SqlConnection(activeConnectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT TOP 1 ProductID, SellingPrice, Quantity, BatchNumber FROM Products WITH (NOLOCK) WHERE ProductName = @N AND Category = @C AND Quantity > 0 ORDER BY BatchNumber ASC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@N", prod);
                        cmd.Parameters.AddWithValue("@C", ComboCategory.Text);
                        using (var r = await cmd.ExecuteReaderAsync())
                        {
                            if (await r.ReadAsync())
                            {
                                int stock = (int)r["Quantity"];
                                if (reqQty > stock) { MessageBox.Show($"Only {stock} left!"); return; }

                                decimal price = (decimal)r["SellingPrice"];
                                dgvCart.Rows.Add(r["ProductID"], $"{prod} (B{r["BatchNumber"]})", ComboCategory.Text, reqQty, price, (price * reqQty));
                                CalculateGrandTotal();
                                txtQuantity.Clear();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error adding to cart: " + ex.Message); }
        }

        private async void BtnCheckout_Click(object sender, EventArgs e)
        {
            if (dgvCart.Rows.Count == 0 || !decimal.TryParse(txtAmount.Text, out decimal paid)) return;

            decimal total = GetCartTotalValue();
            if (paid < total) { MessageBox.Show("Insufficient payment."); return; }

            try
            {
                using (var conn = new SqlConnection(activeConnectionString))
                {
                    await conn.OpenAsync();
                    using (var trans = conn.BeginTransaction())
                    {
                        try
                        {
                            string ordSql = @"INSERT INTO Orders (OrderDate, StaffName, TotalAmount, AmountPaid, ChangeAmount) 
                                             OUTPUT INSERTED.OrderID VALUES (GETDATE(), @staff, @total, @paid, @chg)";

                            using (SqlCommand cmdOrd = new SqlCommand(ordSql, conn, trans))
                            {
                                cmdOrd.Parameters.AddWithValue("@staff", string.IsNullOrWhiteSpace(txtStaffname.Text) ? "Staff" : txtStaffname.Text);
                                cmdOrd.Parameters.AddWithValue("@total", total);
                                cmdOrd.Parameters.AddWithValue("@paid", paid);
                                cmdOrd.Parameters.AddWithValue("@chg", paid - total);
                                currentReceiptID = (await cmdOrd.ExecuteScalarAsync()).ToString();
                            }

                            foreach (DataGridViewRow row in dgvCart.Rows)
                            {
                                if (row.IsNewRow) continue;
                                using (SqlCommand cmdUp = new SqlCommand("UPDATE Products SET Quantity = Quantity - @qty WHERE ProductID = @pid", conn, trans))
                                {
                                    cmdUp.Parameters.AddWithValue("@qty", row.Cells["Quantity"].Value);
                                    cmdUp.Parameters.AddWithValue("@pid", row.Cells["ProductID"].Value);
                                    await cmdUp.ExecuteNonQueryAsync();
                                }
                            }

                            trans.Commit();
                            ShowReceiptPreview();
                            ClearAllFields();
                            await RefreshOrderHistoryAsync();
                        }
                        catch { trans.Rollback(); throw; }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Checkout Error: " + ex.Message); }
        }

        // ================= PRINTING (RAM SAFE & FULL INFO) =================
        // ================= PRINTING (FULL BUSINESS INFO) =================
        private void PrintReceiptTemplate(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            // Using Segoe UI for a professional look matching the physical invoice
            using (Font fTitle = new Font("Segoe UI", 14, FontStyle.Bold))
            using (Font fReg = new Font("Segoe UI", 9))
            using (Font fBold = new Font("Segoe UI", 9, FontStyle.Bold))
            using (Font fSmall = new Font("Segoe UI", 8))
            {
                float y = 20;
                int margin = 20;
                int width = 280; // Standard receipt width
                StringFormat center = new StringFormat { Alignment = StringAlignment.Center };
                StringFormat right = new StringFormat { Alignment = StringAlignment.Far };

                // 1. COMPANY HEADER
                g.DrawString("GOLDEN ANGLES AFRICA", fTitle, Brushes.Black, new RectangleF(0, y, width + 40, 25), center);
                y += 25;

                // Contact & Location Details
                g.DrawString("Tel: 0714484838 / 0116923796", fSmall, Brushes.Black, new RectangleF(0, y, width + 40, 15), center);
                y += 15;
                g.DrawString("P.O Box: 22450-00400", fSmall, Brushes.Black, new RectangleF(0, y, width + 40, 15), center);
                y += 15;
                g.DrawString("Location: Nyamakima CBC Plaza, 3rd Floor, Room T7", fSmall, Brushes.Black, new RectangleF(0, y, width + 40, 15), center);
                y += 25;

                // 2. PAYMENT INFO (PAYBILL BOX)
                g.DrawRectangle(Pens.Black, margin, y, width - 20, 35);
                g.DrawString("PAYBILL: 542542", fBold, Brushes.Black, margin + 5, y + 5);
                g.DrawString("ACC NO: 609371", fBold, Brushes.Black, margin + 5, y + 18);
                y += 45;

                // 3. TRANSACTION INFO
                g.DrawString($"Receipt: #ORD-{currentReceiptID.PadLeft(6, '0')}", fBold, Brushes.Black, margin, y);
                y += 18;
                g.DrawString($"Date:    {DateTime.Now:dd/MM/yyyy HH:mm}", fReg, Brushes.Black, margin, y);
                y += 15;
                g.DrawString($"Staff:   {(string.IsNullOrWhiteSpace(txtStaffname.Text) ? "General" : txtStaffname.Text)}", fReg, Brushes.Black, margin, y);
                y += 20;

                g.DrawString(new string('-', 45), fReg, Brushes.Black, margin, y);
                y += 15;

                // 4. ITEMS
                foreach (DataGridViewRow row in dgvCart.Rows)
                {
                    if (row.IsNewRow) continue;
                    string name = row.Cells["ProductName"].Value?.ToString() ?? "";
                    string qty = row.Cells["Quantity"].Value?.ToString() ?? "0";
                    string price = Convert.ToDecimal(row.Cells["UnitPrice"].Value).ToString("N2");
                    string lineTotal = Convert.ToDecimal(row.Cells["TotalPrice"].Value).ToString("N2");

                    // Print Product Name
                    g.DrawString(name.Length > 25 ? name.Substring(0, 23) + ".." : name, fReg, Brushes.Black, margin, y);
                    y += 15;
                    // Print Qty x Price and Line Total
                    g.DrawString($"  {qty} x {price}", fSmall, Brushes.Black, margin, y);
                    g.DrawString(lineTotal, fReg, Brushes.Black, margin + width - 20, y, right);
                    y += 18;
                }

                y += 10;
                g.DrawString(new string('-', 45), fReg, Brushes.Black, margin, y);
                y += 15;

                // 5. TOTALS
                decimal totalVal = GetCartTotalValue();
                decimal.TryParse(txtAmount.Text, out decimal cashPaid);

                g.DrawString("TOTAL AMOUNT:", fBold, Brushes.Black, margin, y);
                g.DrawString(totalVal.ToString("N2"), fBold, Brushes.Black, margin + width - 20, y, right);
                y += 20;
                g.DrawString("CASH PAID:", fReg, Brushes.Black, margin, y);
                g.DrawString(cashPaid.ToString("N2"), fReg, Brushes.Black, margin + width - 20, y, right);
                y += 15;
                g.DrawString("BALANCE:", fBold, Brushes.Black, margin, y);
                g.DrawString((cashPaid - totalVal).ToString("N2"), fBold, Brushes.Black, margin + width - 20, y, right);

                y += 40;
                g.DrawString("THANK YOU FOR YOUR PATRONAGE!", fSmall, Brushes.Black, new RectangleF(0, y, width + 40, 20), center);
            }
        }

        // ================= HELPERS & NAVIGATION =================
        private decimal GetCartTotalValue()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvCart.Rows)
                if (row.Cells["TotalPrice"].Value != null) total += Convert.ToDecimal(row.Cells["TotalPrice"].Value);
            return total;
        }

        private void CalculateGrandTotal() => lbltotal.Text = "KSh " + GetCartTotalValue().ToString("N2");

        private void ClearAllFields() { dgvCart.Rows.Clear(); txtAmount.Clear(); txtQuantity.Clear(); CalculateGrandTotal(); }

        private void ShowReceiptPreview()
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += PrintReceiptTemplate;
                PrintPreviewDialog ppd = new PrintPreviewDialog { Document = pd };
                ppd.ShowDialog();
            }
            catch (Exception ex) { MessageBox.Show("Print error: " + ex.Message); }
        }

        private void SmoothNavigate(Form f) { f.Show(); this.Hide(); }

        private void BtnSales_Click(object sender, EventArgs e) => SmoothNavigate(new Sales());
        private void BtnInventory_Click(object sender, EventArgs e) => SmoothNavigate(new Inventory());
        private void btnHome_Click(object sender, EventArgs e) => SmoothNavigate(new Form2());
        private void BtnShoewear_Click(object sender, EventArgs e) => SmoothNavigate(new ShoeWear());

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvCart.SelectedRows) dgvCart.Rows.Remove(row);
                CalculateGrandTotal();
            }
        }

        private void TxtAmount_TextChanged(object sender, EventArgs e) { }
        private void btnClearcart_Click(object sender, EventArgs e) => ClearAllFields();
        private void cmbUser_SelectedIndexChanged(object sender, EventArgs e) { }
        private void lbltotal_Click(object sender, EventArgs e) { }
        private void Orders_Load_1(object sender, EventArgs e) { }
    }
}