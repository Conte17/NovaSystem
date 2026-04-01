using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace NovaSystem
{
    public partial class Cosmetics : Form
    {
        // RAM OPTIMIZATION: Small packet size and pooling for 4GB machines
        private const string TuningOptions = "Connect Timeout=3;Pooling=true;Max Pool Size=10;Packet Size=4096;";

        private readonly string devConnectionString = $@"Data Source=DESKTOP-60H5VBK\MSSQLSERVER01;Initial Catalog=NovasystemDB;Integrated Security=True;TrustServerCertificate=True;{TuningOptions}";
        private readonly string clientConnectionString = $@"Data Source=.\SQLEXPRESS;Initial Catalog=NovasystemDB;Integrated Security=True;TrustServerCertificate=True;{TuningOptions}";

        // Static prevents the "Resolve" logic from re-running every time you open this form
        private static string activeConnectionString;

        public Cosmetics()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            if (activeConnectionString == null) activeConnectionString = ResolveConnectionString();
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

        private async Task<SqlConnection> GetConnectionAsync()
        {
            if (activeConnectionString == null) return null;
            var conn = new SqlConnection(activeConnectionString);
            await conn.OpenAsync();
            return conn;
        }

        private async void Cosmetics_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            // Whatever you type in the Designer "Items" collection will stay.
            await RefreshDataAsync();
        }

        private void ComboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Logic removed to prevent overwriting Designer-set items
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            string prodName = cmbBoxProdName.Text.Trim();
            string category = ComboCategory.Text.Trim();

            if (string.IsNullOrEmpty(prodName) || string.IsNullOrEmpty(category) ||
                !int.TryParse(txtQuantity.Text, out int qty) ||
                !decimal.TryParse(txtCost.Text, out decimal cp) ||
                !decimal.TryParse(txtSellingprice.Text, out decimal sp))
            {
                MessageBox.Show("Please check all fields and ensure prices/quantities are numbers.");
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            try
            {
                await Task.Run(async () => {
                    using (var conn = new SqlConnection(activeConnectionString))
                    {
                        await conn.OpenAsync();
                        using (var trans = conn.BeginTransaction())
                        {
                            try
                            {
                                // 1. GLOBAL BATCH NUMBER: Max across system + 1
                                // This allows adding the same product again as a new batch immediately
                                SqlCommand bCmd = new SqlCommand("SELECT ISNULL(MAX(BatchNumber), 0) + 1 FROM Products", conn, trans);
                                int batchNumber = Convert.ToInt32(await bCmd.ExecuteScalarAsync());

                                // 2. Insert Main Product Entry
                                SqlCommand pCmd = new SqlCommand(@"
                                    INSERT INTO Products (ProductName, Category, BatchNumber, Quantity, CostPrice, SellingPrice, Status)
                                    VALUES (@N, @C, @B, @Q, @CP, @SP, 'Active'); SELECT SCOPE_IDENTITY();", conn, trans);
                                pCmd.Parameters.AddWithValue("@N", prodName);
                                pCmd.Parameters.AddWithValue("@C", category);
                                pCmd.Parameters.AddWithValue("@B", batchNumber);
                                pCmd.Parameters.AddWithValue("@Q", qty);
                                pCmd.Parameters.AddWithValue("@CP", cp);
                                pCmd.Parameters.AddWithValue("@SP", sp);

                                int productID = Convert.ToInt32(await pCmd.ExecuteScalarAsync());

                                // 3. UNIQUE SKU GENERATION (Optimized for 4GB RAM)
                                string catInit = GetInitials(category);
                                string prodInit = GetInitials(prodName);
                                string barcode = txtBarcode.Text.Trim();

                                // Generate a unique seed based on current time ticks to prevent duplicate SKUs
                                string uniqueSeed = DateTime.Now.Ticks.ToString();
                                uniqueSeed = uniqueSeed.Substring(uniqueSeed.Length - 5);

                                StringBuilder sqlBatch = new StringBuilder();
                                sqlBatch.Append("INSERT INTO Cosmetics (ProductID, BatchNumber, UnitNumber, SKU, Barcode, Status) VALUES ");

                                for (int i = 1; i <= qty; i++)
                                {
                                    // SKU logic: Batch + Initials + UniqueSeed + UnitNo
                                    string sku = $"B{batchNumber:D2}{catInit}{prodInit}{uniqueSeed}{i:D3}";
                                    sqlBatch.Append($"({productID}, {batchNumber}, {i}, '{sku}', '{barcode}', 'Available')");

                                    if (i < qty) sqlBatch.Append(",");

                                    if (i % 100 == 0 || i == qty)
                                    {
                                        using (SqlCommand cCmd = new SqlCommand(sqlBatch.ToString(), conn, trans))
                                        {
                                            await cCmd.ExecuteNonQueryAsync();
                                        }
                                        sqlBatch.Clear();
                                        if (i < qty) sqlBatch.Append("INSERT INTO Cosmetics (ProductID, BatchNumber, UnitNumber, SKU, Barcode, Status) VALUES ");
                                    }
                                }
                                trans.Commit();
                            }
                            catch { trans.Rollback(); throw; }
                        }
                    }
                });

                MessageBox.Show("Product saved successfully!", "Success");

                // Clear inputs after success
                ClearFields();

                await RefreshDataAsync();
            }
            catch (Exception ex) { MessageBox.Show("Save Error: " + ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void ClearFields()
        {
            cmbBoxProdName.Text = "";
            ComboCategory.Text = "";
            txtQuantity.Clear();
            txtCost.Clear();
            txtSellingprice.Clear();
            txtBarcode.Clear();
            IDcombobox.Text = "";
            cmbBoxProdName.Focus();
        }

        private async void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(IDcombobox.Text)) { MessageBox.Show("Select a record to update."); return; }

            try
            {
                using (var conn = await GetConnectionAsync())
                {
                    string q = "UPDATE Products SET SellingPrice = @SP WHERE ProductName = @N";
                    SqlCommand cmd = new SqlCommand(q, conn);
                    cmd.Parameters.AddWithValue("@SP", decimal.Parse(txtSellingprice.Text));
                    cmd.Parameters.AddWithValue("@N", cmbBoxProdName.Text);
                    await cmd.ExecuteNonQueryAsync();
                    MessageBox.Show("Price updated successfully.");
                    await RefreshDataAsync();
                }
            }
            catch (Exception ex) { MessageBox.Show("Update Error: " + ex.Message); }
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(IDcombobox.Text)) return;
            if (MessageBox.Show("Delete this specific unit?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (var conn = await GetConnectionAsync())
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM Cosmetics WHERE CosmeticID = @ID", conn);
                        cmd.Parameters.AddWithValue("@ID", IDcombobox.Text);
                        await cmd.ExecuteNonQueryAsync();
                        await RefreshDataAsync();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Delete Error: " + ex.Message); }
            }
        }

        private async Task RefreshDataAsync()
        {
            try
            {
                using (var conn = await GetConnectionAsync())
                {
                    string q = @"SELECT TOP 500 C.CosmeticID, P.ProductName, P.Category, P.BatchNumber, P.Quantity AS Stock, 
                                 P.SellingPrice, C.SKU, C.Status FROM Cosmetics C 
                                 JOIN Products P ON C.ProductID = P.ProductID ORDER BY C.CosmeticID DESC";

                    SqlDataAdapter da = new SqlDataAdapter(q, conn);
                    DataTable dt = new DataTable();
                    await Task.Run(() => da.Fill(dt));
                    dataGridView1.DataSource = dt;
                }
            }
            catch { }
        }

        private string GetInitials(string text)
        {
            string cleaned = text.Replace(" ", "").ToUpper();
            return cleaned.Length >= 3 ? cleaned.Substring(0, 3) : cleaned.PadRight(3, 'X');
        }

        private void ApplyTheme()
        {
            BackColor = Color.FromArgb(46, 51, 76);
            dataGridView1.BackgroundColor = Color.FromArgb(74, 79, 99);
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.FromArgb(46, 51, 76);
            dataGridView1.RowsDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(50, 55, 80);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.BorderStyle = BorderStyle.None;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow r = dataGridView1.Rows[e.RowIndex];
                IDcombobox.Text = r.Cells["CosmeticID"].Value?.ToString();
                cmbBoxProdName.Text = r.Cells["ProductName"].Value?.ToString();
                ComboCategory.Text = r.Cells["Category"].Value?.ToString();
                txtSellingprice.Text = r.Cells["SellingPrice"].Value?.ToString();
            }
        }

        private void btnHome_Click(object sender, EventArgs e) { new Form2().Show(); this.Hide(); }
        private void BtnInventory_Click(object sender, EventArgs e) { new Inventory().Show(); this.Hide(); }
        private void BtnSales_Click(object sender, EventArgs e) { new Sales().Show(); this.Hide(); }
        private void button1_Click(object sender, EventArgs e) { new ShoeWear().Show(); this.Hide(); }
    }
}