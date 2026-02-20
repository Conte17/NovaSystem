using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks; // Added for async
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace NovaSystem
{
    public partial class ShoeWear : Form
    {
        private readonly string devConnectionString =
            @"Data Source=DESKTOP-60H5VBK\MSSQLSERVER01; Initial Catalog=NovasystemDB; Integrated Security=True; TrustServerCertificate=True;";

        private readonly string clientConnectionString =
            @"Data Source=.\SQLEXPRESS; Initial Catalog=NovasystemDB; Integrated Security=True; TrustServerCertificate=True;";

        private string activeConnectionString;
        private string selectedSKU = "";

        public ShoeWear()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // Reduces flicker
            activeConnectionString = ResolveConnectionString();
        }

        // ================= CONNECTION & NAVIGATION =================

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

        private void SmoothNavigate(Form nextForm)
        {
            nextForm.Show();
            this.Hide();
        }

        // ================= LOAD & UI =================

        private async void ShoeWear_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            await LoadGridAsync(); // Initial load

            LoginTag.Text = !string.IsNullOrWhiteSpace(UserSession.Fullname)
                ? $"Logged in: {UserSession.Fullname}"
                : $"Logged in as: {UserSession.Username}";
        }

        private void ApplyTheme()
        {
            BackColor = Color.FromArgb(46, 51, 76);
            dataGridViewShoes.BackgroundColor = Color.FromArgb(74, 79, 99);
            dataGridViewShoes.ReadOnly = true;
            dataGridViewShoes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewShoes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // ================= GRID (ASYNC FOR SPEED) =================

        private async Task LoadGridAsync()
        {
            if (string.IsNullOrEmpty(activeConnectionString)) return;

            DataTable dt = await Task.Run(() =>
            {
                string q = @"
                SELECT 
                    S.ShoeID,
                    S.ShoeName,
                    S.Brand,
                    S.Category,
                    S.Size,
                    S.Color,
                    S.BatchNumber,
                    P.Quantity AS BatchQuantity,
                    S.UnitNumber,
                    S.SKU,
                    S.Barcode,
                    S.CostPrice,
                    S.SellingPrice,
                    S.Status
                FROM Shoes S
                JOIN Products P ON S.ProductID = P.ProductID
                ORDER BY S.ShoeID DESC";

                DataTable tempTable = new DataTable();
                try
                {
                    using (var conn = new SqlConnection(activeConnectionString))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(q, conn);
                        da.Fill(tempTable);
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error loading grid: " + ex.Message); }
                return tempTable;
            });

            dataGridViewShoes.DataSource = dt;
        }

        // ================= BATCH LOGIC =================

        private int GetNextBatch(SqlConnection conn, string productName)
        {
            SqlCommand cmd = new SqlCommand(
                "SELECT ISNULL(MAX(BatchNumber),0)+1 FROM Products WHERE ProductName=@N", conn);
            cmd.Parameters.AddWithValue("@N", productName);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        private string CategoryInitial(string cat)
        {
            if (string.IsNullOrEmpty(cat)) return "GEN";
            return cat.Length >= 3 ? cat.Substring(0, 3).ToUpper() : "SHS";
        }

        // ================= SAVE (INDIVIDUAL UNITS) =================

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtQuantity.Text, out int qty) || qty <= 0)
            {
                MessageBox.Show("Invalid quantity");
                return;
            }

            try
            {
                using (var conn = new SqlConnection(activeConnectionString))
                {
                    await conn.OpenAsync();
                    int batchNumber = GetNextBatch(conn, txtShoeName.Text);
                    string catInit = CategoryInitial(cmbCategory.Text);

                    // 1️⃣ INSERT PRODUCT (BATCH LEVEL)
                    SqlCommand pCmd = new SqlCommand(@"
                        INSERT INTO Products
                        (ProductName, Category, BatchNumber, Quantity, CostPrice, SellingPrice, Status)
                        VALUES (@N,@C,@B,@Q,@CP,@SP,'Active');
                        SELECT SCOPE_IDENTITY();", conn);

                    pCmd.Parameters.AddWithValue("@N", txtShoeName.Text.Trim());
                    pCmd.Parameters.AddWithValue("@C", cmbCategory.Text);
                    pCmd.Parameters.AddWithValue("@B", batchNumber);
                    pCmd.Parameters.AddWithValue("@Q", qty);
                    pCmd.Parameters.AddWithValue("@CP", decimal.Parse(txtCostPrice.Text));
                    pCmd.Parameters.AddWithValue("@SP", decimal.Parse(txtSellingPrice.Text));

                    int productID = Convert.ToInt32(pCmd.ExecuteScalar());

                    // 2️⃣ INSERT INDIVIDUAL SHOES
                    for (int i = 1; i <= qty; i++)
                    {
                        string sku = $"SHB{batchNumber:D2}{catInit}{txtSize.Text}{i:D3}";

                        SqlCommand sCmd = new SqlCommand(@"
                            INSERT INTO Shoes
                            (ProductID, ShoeName, Brand, Category, Size, Color,
                             BatchNumber, UnitNumber, SKU, Barcode,
                             CostPrice, SellingPrice, Status)
                            VALUES
                            (@PID,@N,@BR,@C,@S,@CL,
                             @BN,@UN,@SKU,@BC,
                             @CP,@SP,'Available')", conn);

                        sCmd.Parameters.AddWithValue("@PID", productID);
                        sCmd.Parameters.AddWithValue("@N", txtShoeName.Text.Trim());
                        sCmd.Parameters.AddWithValue("@BR", txtBrand.Text.Trim());
                        sCmd.Parameters.AddWithValue("@C", cmbCategory.Text);
                        sCmd.Parameters.AddWithValue("@S", txtSize.Text.Trim());
                        sCmd.Parameters.AddWithValue("@CL", txtColor.Text.Trim());
                        sCmd.Parameters.AddWithValue("@BN", batchNumber);
                        sCmd.Parameters.AddWithValue("@UN", i);
                        sCmd.Parameters.AddWithValue("@SKU", sku);
                        sCmd.Parameters.AddWithValue("@BC", txtBarcode.Text);
                        sCmd.Parameters.AddWithValue("@CP", decimal.Parse(txtCostPrice.Text));
                        sCmd.Parameters.AddWithValue("@SP", decimal.Parse(txtSellingPrice.Text));

                        await sCmd.ExecuteNonQueryAsync();
                    }
                }

                MessageBox.Show("Shoe batch and individual units saved successfully");
                ClearForm();
                await LoadGridAsync();
            }
            catch (Exception ex) { MessageBox.Show("Save failed: " + ex.Message); }
        }

        // ================= UPDATE =================

        private async void BtnUpdate_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedSKU)) return;

            try
            {
                using (var conn = new SqlConnection(activeConnectionString))
                {
                    await conn.OpenAsync();
                    SqlCommand cmd = new SqlCommand(@"
                        UPDATE Shoes SET
                        Barcode=@B,
                        CostPrice=@CP,
                        SellingPrice=@SP
                        WHERE SKU=@SKU", conn);

                    cmd.Parameters.AddWithValue("@SKU", selectedSKU);
                    cmd.Parameters.AddWithValue("@B", txtBarcode.Text);
                    cmd.Parameters.AddWithValue("@CP", decimal.Parse(txtCostPrice.Text));
                    cmd.Parameters.AddWithValue("@SP", decimal.Parse(txtSellingPrice.Text));

                    await cmd.ExecuteNonQueryAsync();
                }
                await LoadGridAsync();
            }
            catch (Exception ex) { MessageBox.Show("Update failed: " + ex.Message); }
        }

        // ================= DELETE =================

        private async void BtnDelete_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedSKU)) return;

            if (MessageBox.Show("Delete this shoe unit?", "Confirm",
                MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            try
            {
                using (var conn = new SqlConnection(activeConnectionString))
                {
                    await conn.OpenAsync();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Shoes WHERE SKU=@SKU", conn);
                    cmd.Parameters.AddWithValue("@SKU", selectedSKU);
                    await cmd.ExecuteNonQueryAsync();
                }

                await LoadGridAsync();
                ClearForm();
            }
            catch (Exception ex) { MessageBox.Show("Delete failed: " + ex.Message); }
        }

        // ================= HELPERS & NAVIGATION =================

        private void ClearForm()
        {
            selectedSKU = "";
            txtShoeName.Clear();
            txtBrand.Clear();
            txtSize.Clear();
            txtColor.Clear();
            txtBarcode.Clear();
            txtCostPrice.Clear();
            txtSellingPrice.Clear();
            txtQuantity.Clear();
        }

        private void dataGridViewShoes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var r = dataGridViewShoes.Rows[e.RowIndex];
            selectedSKU = r.Cells["SKU"].Value.ToString();
            txtShoeName.Text = r.Cells["ShoeName"].Value.ToString();
            txtBrand.Text = r.Cells["Brand"].Value.ToString();
            txtSize.Text = r.Cells["Size"].Value.ToString();
            txtColor.Text = r.Cells["Color"].Value.ToString();
            txtBarcode.Text = r.Cells["Barcode"].Value.ToString();
            txtCostPrice.Text = r.Cells["CostPrice"].Value.ToString();
            txtSellingPrice.Text = r.Cells["SellingPrice"].Value.ToString();
        }

        private void BtnCosmetics_Click(object sender, EventArgs e) => SmoothNavigate(new Cosmetics());
        private void btnHome_Click(object sender, EventArgs e) => SmoothNavigate(new Form2());

        private void txtCostPrice_TextChanged(object sender, EventArgs e) { }
        private void ShoeWear_Load_1(object sender, EventArgs e) { /* Placeholder */ }
    }
}