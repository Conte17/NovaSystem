using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace NovaSystem
{
    public partial class Inventory : Form
    {
        // Added Connect Timeout=3 to prevent long hangs on startup
        private readonly string devConnectionString = @"Data Source=DESKTOP-60H5VBK\MSSQLSERVER01;Initial Catalog=NovasystemDB;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=3;";
        private readonly string clientConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=NovasystemDB;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=3;";
        private string activeConnectionString;

        public Inventory()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            activeConnectionString = ResolveConnectionString();
        }

        private async void Inventory_Load(object sender, EventArgs e)
        {
            SetUserLabel();

            // Setup UI
            ComboCategory.Items.Clear();
            ComboCategory.Items.AddRange(new string[] { "All", "Coconut", "Collagen", "Salicylic Acid", "Turmeric", "Vitamin C", "Shoes" });
            ComboCategory.SelectedIndex = 0;

            // Load data asynchronously so the form shows up instantly
            await RefreshAllData();
        }

        private async Task RefreshAllData()
        {
            string filter = ComboCategory.Text;

            // PERFORMANCE BOOST: Runs both DB tasks at the same time
            await Task.WhenAll(
                UpdateInventoryDashboard(filter),
                LoadFilteredGrid(filter, "")
            );
        }

        // ================= OPTIMIZED DASHBOARD LOGIC =================

        private async Task UpdateInventoryDashboard(string categoryFilter)
        {
            if (string.IsNullOrEmpty(activeConnectionString)) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(activeConnectionString))
                {
                    await conn.OpenAsync();

                    // Optimized: Combined main stats and specific shoe stats into ONE query trip
                    string statsQuery = @"
                        SELECT 
                            ISNULL(SUM(Quantity), 0) as DynamicQty,
                            ISNULL(SUM(Quantity * SellingPrice), 0) as DynamicSales,
                            ISNULL(MIN(Quantity), 100) as MinQty,
                            COUNT(ProductID) as TotalProductTypes,
                            (SELECT ISNULL(SUM(Quantity), 0) FROM Products WHERE Category = 'Shoes') as ShoeQty,
                            (SELECT ISNULL(SUM(Quantity * SellingPrice), 0) FROM Products WHERE Category = 'Shoes') as ShoeSales,
                            (SELECT ISNULL(SUM(SI.Quantity), 0) FROM SaleItems SI INNER JOIN Products P ON SI.ProductID = P.ProductID WHERE (@Filter = 'All' OR P.Category = @Filter)) as SoldCount
                        FROM Products
                        WHERE (@Filter = 'All' OR Category = @Filter)";

                    using (SqlCommand cmd = new SqlCommand(statsQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Filter", categoryFilter);
                        using (SqlDataReader r = await cmd.ExecuteReaderAsync())
                        {
                            if (await r.ReadAsync())
                            {
                                lblCosmeticInventory.Text = r["DynamicQty"].ToString();
                                lblCosmeticSales.Text = "Ksh. " + Convert.ToDecimal(r["DynamicSales"]).ToString("N2");
                                lblShoeCounter.Text = r["ShoeQty"].ToString();
                                lblShoeSales.Text = "Ksh. " + Convert.ToDecimal(r["ShoeSales"]).ToString("N2");
                                ProductTotal.Text = r["TotalProductTypes"].ToString();
                                lblItemsSold.Text = r["SoldCount"].ToString();

                                UpdateStockIndicator(Convert.ToInt32(r["MinQty"]));
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine("Dash Error: " + ex.Message); }
        }

        // ================= OPTIMIZED GRID LOGIC =================

        private async Task LoadFilteredGrid(string category, string prodName)
        {
            if (string.IsNullOrEmpty(activeConnectionString)) return;

            string query = @"
                SELECT 
                    ProductName as [Product], 
                    Category, 
                    Quantity as [Stock Level], 
                    CASE 
                        WHEN Quantity <= 5 THEN 'Critically Low'
                        WHEN Quantity <= 10 THEN 'Low'
                        ELSE 'Healthy'
                    END as [Status],
                    SellingPrice as [Unit Price],
                    (Quantity * SellingPrice) as [Projected Income]
                FROM Products 
                WHERE (@Cat = 'All' OR Category = @Cat)
                AND (ProductName LIKE @Prod OR @Prod = '')
                ORDER BY Quantity ASC";

            try
            {
                using (SqlConnection conn = new SqlConnection(activeConnectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@Cat", category);
                    da.SelectCommand.Parameters.AddWithValue("@Prod", "%" + prodName + "%");

                    DataTable dt = new DataTable();
                    // Load the table in the background thread
                    await Task.Run(() => da.Fill(dt));

                    dgvInventory.DataSource = dt;
                    FormatGrid();
                }
            }
            catch (Exception ex) { Console.WriteLine("Grid Error: " + ex.Message); }
        }

        private void FormatGrid()
        {
            if (dgvInventory.Columns.Count > 0)
            {
                if (dgvInventory.Columns.Contains("Projected Income"))
                    dgvInventory.Columns["Projected Income"].DefaultCellStyle.Format = "N2";

                if (dgvInventory.Columns.Contains("Unit Price"))
                    dgvInventory.Columns["Unit Price"].DefaultCellStyle.Format = "N2";

                dgvInventory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void UpdateStockIndicator(int minStock)
        {
            if (minStock <= 5) SetIndicator("CRITICALLY LOW", Color.Red);
            else if (minStock <= 10) SetIndicator("LOW STOCK", Color.OrangeRed);
            else SetIndicator("STOCK OKAY", Color.Green);
        }

        private void SetIndicator(string text, Color color)
        {
            lblIndicator.Text = text;
            lblIndicator.BackColor = color;
            lblIndicator.ForeColor = Color.White;
        }

        // ================= EVENTS & NAVIGATION =================

        private async void ComboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            await RefreshAllData();
        }

        private async void BtnCheckinventory_Click(object sender, EventArgs e) => await RefreshAllData();

        private string ResolveConnectionString()
        {
            if (TestConnection(devConnectionString)) return devConnectionString;
            return clientConnectionString;
        }

        private bool TestConnection(string cs)
        {
            try { using (var c = new SqlConnection(cs)) { c.Open(); return true; } }
            catch { return false; }
        }

        private void SetUserLabel() => lblLogin.Text = $"Logged in: {UserSession.Fullname ?? UserSession.Username}";

        private void SmoothNavigate(Form nextForm)
        {
            nextForm.Show();
            this.Hide();
        }

        private void btnHome_Click(object sender, EventArgs e) => SmoothNavigate(new Form2());
        private void BtnSales_Click(object sender, EventArgs e) => SmoothNavigate(new Sales());
        private void BtnShoewear_Click(object sender, EventArgs e) => SmoothNavigate(new ShoeWear());
        private void btnReports_Click(object sender, EventArgs e) => SmoothNavigate(new reports());
        private void btnLogout_Click(object sender, EventArgs e)
        {
            UserSession.Clear();
            new LoginForm().Show();
            this.Close();
        }

        private async void BtnInventory_Click(object sender, EventArgs e)
        {
            ComboCategory.SelectedIndex = 0;
            await RefreshAllData();
        }
    }
}