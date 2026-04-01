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
        // 1. RAM OPTIMIZATION: Small packet size and reduced pool for 4GB machines
        private const string TuningOptions = "Connect Timeout=3;Pooling=true;Max Pool Size=10;Packet Size=4096;";
        private readonly string devConnectionString = $@"Data Source=DESKTOP-60H5VBK\MSSQLSERVER01;Initial Catalog=NovasystemDB;Integrated Security=True;TrustServerCertificate=True;{TuningOptions}";
        private readonly string clientConnectionString = $@"Data Source=.\SQLEXPRESS;Initial Catalog=NovasystemDB;Integrated Security=True;TrustServerCertificate=True;{TuningOptions}";

        // Static connection string so it only resolves ONCE per app session
        private static string activeConnectionString;

        public Inventory()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            if (activeConnectionString == null) activeConnectionString = ResolveConnectionString();
        }

        private async void Inventory_Load(object sender, EventArgs e)
        {
            SetUserLabel();
            if (ComboCategory.Items.Count > 0) ComboCategory.SelectedIndex = 0;
            // Removed Task.WhenAll and moved to a single sequential refresh
            await RefreshAllData();
        }

        private async Task RefreshAllData()
        {
            string input = ComboCategory.Text.Trim();
            bool showAll = string.IsNullOrWhiteSpace(input) ||
                           input.Equals("All", StringComparison.OrdinalIgnoreCase) ||
                           input.Equals("All Cosmetics", StringComparison.OrdinalIgnoreCase);

            string filter = showAll ? "" : input;

            // Sequential execution is actually FASTER on 4GB RAM than parallel 
            // because it prevents disk/connection contention.
            await UpdateInventoryDashboard(filter);
            await LoadFilteredGrid(filter);
        }

        private async Task UpdateInventoryDashboard(string filter)
        {
            if (string.IsNullOrEmpty(activeConnectionString)) return;

            // Optimized Query: Removed the CTE and used a more direct subquery approach
            string statsQuery = @"
                SELECT 
                    ISNULL(SUM(Quantity), 0) as AvailableQty,
                    ISNULL(SUM(Quantity * SellingPrice), 0) as SalesValue,
                    ISNULL(MIN(NULLIF(Quantity, 0)), 0) as MinStock,
                    COUNT(ProductID) as TotalTypes,
                    (SELECT ISNULL(SUM(SI.Quantity), 0) 
                     FROM SaleItems SI WITH (NOLOCK) 
                     INNER JOIN Products P2 ON SI.ProductID = P2.ProductID 
                     WHERE (@F = '' OR P2.Category = @F OR P2.ProductName LIKE '%' + @F + '%')) as SoldCount
                FROM Products WITH (NOLOCK)
                WHERE (@F = '' OR Category = @F OR ProductName LIKE '%' + @F + '%')";

            try
            {
                using (SqlConnection conn = new SqlConnection(activeConnectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(statsQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@F", filter);
                        using (SqlDataReader r = await cmd.ExecuteReaderAsync())
                        {
                            if (await r.ReadAsync())
                            {
                                lblCosmeticInventory.Text = r["AvailableQty"].ToString();
                                lblItemsSold.Text = r["SoldCount"].ToString();
                                lblCosmeticSales.Text = "Ksh. " + Convert.ToDecimal(r["SalesValue"]).ToString("N2");
                                ProductTotal.Text = r["TotalTypes"].ToString();
                                UpdateStockIndicator(Convert.ToInt32(r["MinStock"]));
                            }
                        }
                    }
                }
            }
            catch { /* Silent catch for speed */ }
        }

        private async Task LoadFilteredGrid(string filter)
        {
            if (string.IsNullOrEmpty(activeConnectionString)) return;

            // TOP 500 added to prevent RAM exhaustion if the database grows huge
            string query = @"
                SELECT TOP 500
                    ProductName as [Product], 
                    Category as [Sub-Category], 
                    Quantity as [Stock Level], 
                    SellingPrice as [Unit Price],
                    (Quantity * SellingPrice) as [Projected Income]
                FROM Products WITH (NOLOCK)
                WHERE (@F = '' OR Category = @F OR ProductName LIKE '%' + @F + '%')
                ORDER BY Quantity ASC";

            try
            {
                using (SqlConnection conn = new SqlConnection(activeConnectionString))
                {
                    await conn.OpenAsync();
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@F", filter);

                    DataTable dt = new DataTable();
                    // Load data in background to keep UI snappy
                    await Task.Run(() => da.Fill(dt));
                    dgvInventory.DataSource = dt;
                    FormatGrid();
                }
            }
            catch { }
        }

        private void FormatGrid()
        {
            if (dgvInventory.Columns.Count > 0)
            {
                dgvInventory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                if (dgvInventory.Columns.Contains("Unit Price"))
                    dgvInventory.Columns["Unit Price"].DefaultCellStyle.Format = "N2";
                if (dgvInventory.Columns.Contains("Projected Income"))
                    dgvInventory.Columns["Projected Income"].DefaultCellStyle.Format = "N2";

                dgvInventory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void UpdateStockIndicator(int minStock)
        {
            if (minStock <= 5 && minStock > 0) SetIndicator("CRITICALLY LOW", Color.Red);
            else if (minStock <= 10 && minStock > 5) SetIndicator("LOW STOCK", Color.OrangeRed);
            else if (minStock == 0 && ProductTotal.Text == "0") SetIndicator("NO DATA", Color.Gray);
            else SetIndicator("STOCK OKAY", Color.Green);
        }

        private void SetIndicator(string text, Color color)
        {
            lblIndicator.Text = text;
            lblIndicator.BackColor = color;
            lblIndicator.ForeColor = Color.White;
        }

        // ================= UTILITIES (FAST REUSE) =================

        private string ResolveConnectionString()
        {
            if (TestConnection(devConnectionString)) return devConnectionString;
            return TestConnection(clientConnectionString) ? clientConnectionString : null;
        }

        private bool TestConnection(string cs)
        {
            try { using (var c = new SqlConnection(cs)) { c.Open(); return true; } }
            catch { return false; }
        }

        private void SetUserLabel() => lblLogin.Text = $"Logged in: {UserSession.Fullname ?? UserSession.Username}";

        private async void ComboCategory_SelectedIndexChanged(object sender, EventArgs e) => await RefreshAllData();

        private async void BtnCheckinventory_Click(object sender, EventArgs e) => await RefreshAllData();

        private void SmoothNavigate(Form nextForm) { nextForm.Show(); this.Hide(); }
        private void btnHome_Click(object sender, EventArgs e) => SmoothNavigate(new Form2());
        private void BtnSales_Click(object sender, EventArgs e) => SmoothNavigate(new Sales());
        private void btnLogout_Click(object sender, EventArgs e) { UserSession.Clear(); new LoginForm().Show(); this.Close(); }
        // ================= NAVIGATION FIXES =================

        private async void BtnInventory_Click(object sender, EventArgs e)
        {
            // Reset category and refresh
            if (ComboCategory.Items.Count > 0) ComboCategory.SelectedIndex = 0;
            await RefreshAllData();
        }

        private void BtnShoewear_Click(object sender, EventArgs e)
        {
            // Navigate to ShoeWear form
            new ShoeWear().Show();
            this.Hide();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            // Navigate to Reports form
            new reports().Show();
            this.Hide();
        }
    }
}