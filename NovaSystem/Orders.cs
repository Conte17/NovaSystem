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
        // ================= CONFIGURATION & CONNECTION =================
        private readonly string devConnectionString = @"Data Source=DESKTOP-60H5VBK\MSSQLSERVER01; Initial Catalog=NovasystemDB; Integrated Security=True; TrustServerCertificate=True;";
        private readonly string clientConnectionString = @"Data Source=.\SQLEXPRESS; Initial Catalog=NovasystemDB; Integrated Security=True; TrustServerCertificate=True;";

        private string activeConnectionString;
        private string currentReceiptID = "0";

        public Orders()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            activeConnectionString = ResolveConnectionString();

            // Link the search update to the amount text box
            txtAmount.TextChanged += TxtAmount_TextChanged;
        }

        private string ResolveConnectionString()
        {
            if (TestConnection(devConnectionString)) return devConnectionString;
            if (TestConnection(clientConnectionString)) return clientConnectionString;
            return null;
        }

        private bool TestConnection(string cs)
        {
            try { using (SqlConnection c = new SqlConnection(cs)) { c.Open(); return true; } }
            catch { return false; }
        }

        private SqlConnection GetConnection()
        {
            if (string.IsNullOrEmpty(activeConnectionString))
            {
                MessageBox.Show("No database connection available!", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return new SqlConnection(activeConnectionString);
        }

        // ================= NAVIGATION (SMOOTH TRANSITION) =================
        private void SmoothNavigate(Form nextForm)
        {
            nextForm.Show();
            this.Hide();
        }

        // ================= LOAD & INITIALIZATION =================
        private async void Orders_Load(object sender, EventArgs e)
        {
            try
            {
                SetupCartGrid();
                await Task.WhenAll(LoadCategoriesAsync(), RefreshOrderHistoryAsync());
                CalculateGrandTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing form: " + ex.Message);
            }
        }

        private void SetupCartGrid()
        {
            dgvCart.Columns.Clear();
            dgvCart.Rows.Clear();

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
            dgvCart.AllowUserToAddRows = false;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private async Task RefreshOrderHistoryAsync()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    if (conn == null) return;
                    await conn.OpenAsync();
                    string query = @"SELECT TOP 50 OrderID, OrderDate, StaffName AS [Salesperson], TotalAmount, AmountPaid, ChangeAmount 
                                     FROM Orders ORDER BY OrderID DESC";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex) { Console.WriteLine("History Load Error: " + ex.Message); }
        }

        // ================= DYNAMIC COMBOBOX LOGIC =================
        private async Task LoadCategoriesAsync()
        {
            ComboCategory.Items.Clear();
            try
            {
                using (var conn = GetConnection())
                {
                    if (conn == null) return;
                    await conn.OpenAsync();
                    string query = "SELECT DISTINCT LTRIM(RTRIM(Category)) as Category FROM Products WHERE Category IS NOT NULL AND Category <> '' ORDER BY Category ASC";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                            ComboCategory.Items.Add(reader["Category"].ToString());
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading categories: " + ex.Message); }
        }

        private async void ComboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbBoxProdName.Items.Clear();
            cmbBoxProdName.Text = "";
            string selectedCat = ComboCategory.Text.Trim();
            if (string.IsNullOrEmpty(selectedCat)) return;

            try
            {
                using (var conn = GetConnection())
                {
                    if (conn == null) return;
                    await conn.OpenAsync();
                    string query = "SELECT DISTINCT LTRIM(RTRIM(ProductName)) as ProductName FROM Products WHERE Category LIKE @cat ORDER BY ProductName ASC";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@cat", "%" + selectedCat + "%");

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                            cmbBoxProdName.Items.Add(reader["ProductName"].ToString());
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading products: " + ex.Message); }
        }

        // ================= CART LOGIC =================
        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (dgvCart.Columns.Count == 0) SetupCartGrid();

            string cat = ComboCategory.Text.Trim();
            string prod = cmbBoxProdName.Text.Trim();

            if (string.IsNullOrWhiteSpace(prod) || !int.TryParse(txtQuantity.Text, out int reqQty) || reqQty <= 0)
            {
                MessageBox.Show("Please select a valid product and enter a quantity.");
                return;
            }

            try
            {
                using (var conn = GetConnection())
                {
                    if (conn == null) return;
                    await conn.OpenAsync();

                    string query = @"SELECT TOP 1 ProductID, SellingPrice, Quantity, BatchNumber 
                                     FROM Products 
                                     WHERE (ProductName = @exactN)
                                     AND (Category = @exactC)
                                     AND Quantity > 0
                                     ORDER BY BatchNumber ASC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@exactN", prod);
                    cmd.Parameters.AddWithValue("@exactC", cat);

                    using (SqlDataReader r = await cmd.ExecuteReaderAsync())
                    {
                        if (await r.ReadAsync())
                        {
                            int pId = (int)r["ProductID"];
                            int stock = (int)r["Quantity"];
                            decimal price = (decimal)r["SellingPrice"];
                            int batch = (int)r["BatchNumber"];

                            if (reqQty > stock)
                            {
                                MessageBox.Show($"Insufficient stock! Only {stock} available in Batch #{batch}.", "Stock Alert");
                                return;
                            }

                            dgvCart.Rows.Add(pId, $"{prod} (B{batch})", cat, reqQty, price, (price * reqQty));
                            CalculateGrandTotal();
                            txtQuantity.Clear();
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error adding to cart: " + ex.Message); }
        }

        // ================= CHECKOUT & RECEIPT =================
        private async void BtnCheckout_Click(object sender, EventArgs e)
        {
            if (dgvCart.Rows.Count == 0) return;
            if (!decimal.TryParse(txtAmount.Text, out decimal paid)) { MessageBox.Show("Enter amount paid."); return; }

            decimal total = GetCartTotalValue();
            decimal change = paid - total;
            if (change < 0) { MessageBox.Show("Insufficient payment."); return; }

            using (var conn = GetConnection())
            {
                if (conn == null) return;
                await conn.OpenAsync();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    string ordSql = @"INSERT INTO Orders (OrderDate, StaffName, TotalAmount, AmountPaid, ChangeAmount) 
                                      OUTPUT INSERTED.OrderID VALUES (GETDATE(), @staff, @total, @paid, @chg)";
                    SqlCommand cmdOrd = new SqlCommand(ordSql, conn, trans);
                    cmdOrd.Parameters.AddWithValue("@staff", string.IsNullOrEmpty(txtStaffname.Text) ? "General Staff" : txtStaffname.Text);
                    cmdOrd.Parameters.AddWithValue("@total", total);
                    cmdOrd.Parameters.AddWithValue("@paid", paid);
                    cmdOrd.Parameters.AddWithValue("@chg", change);
                    currentReceiptID = (await cmdOrd.ExecuteScalarAsync()).ToString();

                    foreach (DataGridViewRow row in dgvCart.Rows)
                    {
                        int pid = (int)row.Cells["ProductID"].Value;
                        int qty = (int)row.Cells["Quantity"].Value;

                        SqlCommand cmdUp = new SqlCommand("UPDATE Products SET Quantity = Quantity - @qty WHERE ProductID = @pid", conn, trans);
                        cmdUp.Parameters.AddWithValue("@qty", qty);
                        cmdUp.Parameters.AddWithValue("@pid", pid);
                        await cmdUp.ExecuteNonQueryAsync();
                    }

                    trans.Commit();
                    MessageBox.Show("Order processed successfully!");
                    ShowReceiptPreview();
                    ClearAllFields();
                    await RefreshOrderHistoryAsync();
                }
                catch (Exception ex) { trans.Rollback(); MessageBox.Show("Checkout Failed: " + ex.Message); }
            }
        }

        private void ShowReceiptPreview()
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += new PrintPageEventHandler(PrintReceiptTemplate);
                PrintPreviewDialog ppd = new PrintPreviewDialog
                {
                    Document = pd,
                    WindowState = FormWindowState.Maximized
                };
                ppd.ShowDialog();
            }
            catch (Exception ex) { MessageBox.Show("Printing Error: " + ex.Message); }
        }

        private void PrintReceiptTemplate(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font fSmall = new Font("Courier New", 8);
            Font fRegular = new Font("Courier New", 9);
            Font fBold = new Font("Courier New", 10, FontStyle.Bold);
            Font fHeader = new Font("Courier New", 14, FontStyle.Bold);

            float y = 20; int margin = 20; int width = 260;
            StringFormat center = new StringFormat { Alignment = StringAlignment.Center };
            StringFormat right = new StringFormat { Alignment = StringAlignment.Far };

            // BRANDING
            g.DrawString("GOLDEN ANGLES AFRICA", fHeader, Brushes.Black, new RectangleF(0, y, width + 40, 25), center);
            y += 30;
            g.DrawString("OFFICIAL SALES RECEIPT", fRegular, Brushes.Black, new RectangleF(0, y, width + 40, 20), center);
            y += 25;

            // HEADER INFO
            g.DrawString($"Receipt No:  #ORD-{currentReceiptID.PadLeft(6, '0')}", fBold, Brushes.Black, margin, y);
            y += 15;
            g.DrawString($"Date/Time:   {DateTime.Now:dd/MM/yyyy HH:mm}", fRegular, Brushes.Black, margin, y);
            y += 15;
            string cashierName = !string.IsNullOrWhiteSpace(txtStaffname.Text) ? txtStaffname.Text : "General Staff";
            g.DrawString($"Cashier:     {cashierName}", fRegular, Brushes.Black, margin, y);
            y += 20;

            g.DrawString(new string('-', 35), fRegular, Brushes.Black, margin, y);
            y += 15;

            // ITEM LIST
            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                string name = row.Cells["ProductName"].Value.ToString();
                string qty = row.Cells["Quantity"].Value.ToString();
                string price = Convert.ToDecimal(row.Cells["UnitPrice"].Value).ToString("N2");
                string lineTotal = Convert.ToDecimal(row.Cells["TotalPrice"].Value).ToString("N2");

                g.DrawString(name.Length > 25 ? name.Substring(0, 23) + ".." : name, fRegular, Brushes.Black, margin, y);
                y += 15;
                g.DrawString($"  {qty} x {price}", fSmall, Brushes.DimGray, margin, y);
                g.DrawString(lineTotal, fRegular, Brushes.Black, margin + width, y, right);
                y += 18;
            }

            y += 10;
            g.DrawString(new string('-', 35), fRegular, Brushes.Black, margin, y);
            y += 15;

            // TOTALS
            decimal totalVal = GetCartTotalValue();
            decimal.TryParse(txtAmount.Text, out decimal cashPaid);

            g.DrawString("TOTAL AMOUNT:", fBold, Brushes.Black, margin, y);
            g.DrawString(totalVal.ToString("N2"), fBold, Brushes.Black, margin + width, y, right);
            y += 20;

            g.DrawString("CASH PAID:", fRegular, Brushes.Black, margin, y);
            g.DrawString(cashPaid.ToString("N2"), fRegular, Brushes.Black, margin + width, y, right);
            y += 15;

            g.DrawString("BALANCE:", fBold, Brushes.Black, margin, y);
            g.DrawString((cashPaid - totalVal).ToString("N2"), fBold, Brushes.Black, margin + width, y, right);

            y += 40;
            g.DrawString("THANK YOU FOR YOUR PATRONAGE!", fRegular, Brushes.Black, new RectangleF(0, y, width + 40, 20), center);
        }

        // ================= HELPERS & EVENTS =================
        private decimal GetCartTotalValue()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvCart.Rows)
                total += Convert.ToDecimal(row.Cells["TotalPrice"].Value ?? 0);
            return total;
        }

        private void CalculateGrandTotal() => lbltotal.Text = "KSh " + GetCartTotalValue().ToString("N2");

        private void ClearAllFields()
        {
            dgvCart.Rows.Clear();
            txtAmount.Clear();
            txtQuantity.Clear();
            CalculateGrandTotal();
        }

        private void TxtAmount_TextChanged(object sender, EventArgs e)
        {
            // Optional: Update a balance label on the UI if you have one
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvCart.SelectedRows) dgvCart.Rows.Remove(row);
                CalculateGrandTotal();
            }
        }

        private void btnClearcart_Click(object sender, EventArgs e) => ClearAllFields();
        private void BtnSales_Click(object sender, EventArgs e) => SmoothNavigate(new Sales());
        private void BtnInventory_Click(object sender, EventArgs e) => SmoothNavigate(new Inventory());
        private void btnHome_Click(object sender, EventArgs e) => SmoothNavigate(new Form2());
        private void BtnShoewear_Click(object sender, EventArgs e) => SmoothNavigate(new ShoeWear());

        // Dummy handlers to prevent context errors
        private void cmbUser_SelectedIndexChanged(object sender, EventArgs e) { }
        private void lbltotal_Click(object sender, EventArgs e) { }
        private void Orders_Load_1(object sender, EventArgs e) { }
    }
}