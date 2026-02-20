using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace NovaSystem
{
    public partial class Form2 : Form
    {
        // RAM OPTIMIZATION: Max Pool Size keeps connections ready; smaller Packet Size is better for 4GB RAM.
        private const string TuningOptions = "Connect Timeout=2;Pooling=true;Max Pool Size=10;Packet Size=4096;";

        private readonly string devConnectionString =
            $@"Data Source=DESKTOP-60H5VBK\MSSQLSERVER01;Initial Catalog=NovasystemDB;Integrated Security=True;TrustServerCertificate=True;{TuningOptions}";

        private readonly string clientConnectionString =
            $@"Data Source=.\SQLEXPRESS;Initial Catalog=NovasystemDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;{TuningOptions}";

        // Use a static variable so we only resolve the connection ONCE per app launch
        private static string activeConnectionString;

        public Form2()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        // SPEED BOOST: Prevents panels/labels from flickering on low-end GPUs
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED (Double Buffer the whole window)
                return cp;
            }
        }

        private async void Form2_Load(object sender, EventArgs e)
        {
            LoginTag.Text = !string.IsNullOrWhiteSpace(UserSession.Fullname)
                ? $"Logged in: {UserSession.Fullname}"
                : $"Logged in as: {UserSession.Username}";

            // Resolve connection once, then load data
            if (activeConnectionString == null)
            {
                activeConnectionString = await ResolveConnectionAsync();
            }

            await LoadDashboardCountsAsync();
        }

        private void Form2_Activated(object sender, EventArgs e)
        {
            _ = LoadDashboardCountsAsync();
        }

        // ============================
        // CONNECTION RESOLUTION (Ultrafast Race)
        // ============================

        private async Task<string> ResolveConnectionAsync()
        {
            // Start both tests at once. Whichever responds first wins.
            var t1 = Task.Run(() => TestConnection(devConnectionString) ? devConnectionString : null);
            var t2 = Task.Run(() => TestConnection(clientConnectionString) ? clientConnectionString : null);

            var firstToRespond = await Task.WhenAny(t1, t2);
            string result = await firstToRespond;

            if (result != null) return result;

            // If first failed, check the other one
            return await (t1 == firstToRespond ? t2 : t1) ?? null;
        }

        private bool TestConnection(string connectionString)
        {
            try { using (SqlConnection conn = new SqlConnection(connectionString)) { conn.Open(); return true; } }
            catch { return false; }
        }

        // ============================
        // DASHBOARD COUNTS (Consolidated)
        // ============================

        private async Task LoadDashboardCountsAsync()
        {
            if (string.IsNullOrEmpty(activeConnectionString)) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(activeConnectionString))
                {
                    await conn.OpenAsync();

                    // One single query to get all dashboard stats at once
                    const string megaQuery = @"
                        SELECT 
                        (SELECT COUNT(*) FROM Cosmetics WHERE Status = 'Available') as CosAvail,
                        (SELECT COUNT(*) FROM Shoes WHERE Status = 'Available') as ShoeAvail,
                        (SELECT COUNT(*) FROM Cosmetics WHERE Status = 'Sold') as CosSold,
                        (SELECT COUNT(*) FROM Shoes WHERE Status = 'Sold') as ShoeSold,
                        (SELECT ISNULL(SUM(TotalAmount), 0) FROM Sales WHERE SaleDate >= DATEADD(HOUR, -24, GETDATE())) as TotalEarn,
                        (SELECT ISNULL(SUM(SI.UnitPrice * SI.Quantity), 0) FROM SaleItems SI JOIN Sales S ON SI.SaleID = S.SaleID WHERE SI.ProductType = 'Shoe' AND S.SaleDate >= DATEADD(HOUR, -24, GETDATE())) as ShoeEarn,
                        (SELECT ISNULL(SUM(SI.UnitPrice * SI.Quantity), 0) FROM SaleItems SI JOIN Sales S ON SI.SaleID = S.SaleID WHERE SI.ProductType = 'Cosmetic' AND S.SaleDate >= DATEADD(HOUR, -24, GETDATE())) as CosEarn";

                    using (SqlCommand cmd = new SqlCommand(megaQuery, conn))
                    {
                        // CommandBehavior.SequentialAccess is much faster on low RAM
                        using (SqlDataReader rdr = await cmd.ExecuteReaderAsync(CommandBehavior.SequentialAccess))
                        {
                            if (await rdr.ReadAsync())
                            {
                                // Financials (Converted safely to avoid null/cast errors)
                                decimal total = rdr.IsDBNull(4) ? 0 : rdr.GetDecimal(4);
                                decimal shoe = rdr.IsDBNull(5) ? 0 : rdr.GetDecimal(5);
                                decimal cos = rdr.IsDBNull(6) ? 0 : rdr.GetDecimal(6);

                                // Bulk update UI - strings are cheaper than repeated Convert.ToDecimal calls
                                lblCosmeticCounter.Text = rdr[0].ToString();
                                lblShoeCounter.Text = rdr[1].ToString();
                                lblCosmeticSold.Text = rdr[2].ToString();
                                lblShoesSold.Text = rdr[3].ToString();

                                lbl24earnings.Text = "KSh " + total.ToString("N2");
                                lblShoeSales.Text = "KSh " + shoe.ToString("N2");
                                lblCosmeticSales.Text = "KSh " + cos.ToString("N2");
                            }
                        }
                    }
                }
            }
            catch { /* Silent fail to keep UI responsive */ }
        }

        // ============================
        // NAVIGATION (Optimized Transitions)
        // ============================

        private void NavigateTo(Form nextForm)
        {
            nextForm.Show();
            nextForm.Update(); // Ensure next form draws before this one hides
            this.Hide();
        }

        private void btnSales_Click(object sender, EventArgs e) { NavigateTo(new Sales()); }
        private void BtnInventory_Click(object sender, EventArgs e) { NavigateTo(new Inventory()); }
        private void BtnCosmetics_Click(object sender, EventArgs e) { NavigateTo(new Cosmetics()); }
        private void button1_Click(object sender, EventArgs e) { NavigateTo(new ShoeWear()); }
        private void BtnReports_Click(object sender, EventArgs e) { NavigateTo(new reports()); }
        private void BtnOrders_Click(object sender, EventArgs e) { NavigateTo(new Orders()); }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Logout?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                UserSession.Clear();
                new LoginForm().Show();
                this.Close();
            }
        }

        // ============================
        // DESIGNER HELPERS (No logic trimmed)
        // ============================
        private void LoadDashboardCounts() { _ = LoadDashboardCountsAsync(); }
        private void DisplayCosmeticsInventoryCount() => LoadDashboardCounts();
        private void DisplayShoesInventoryCount() => LoadDashboardCounts();
        private void DisplayCosmeticsSoldCount() => LoadDashboardCounts();
        private void DisplayShoesSoldCount() => LoadDashboardCounts();
        private void DisplayLast24HoursEarnings() => LoadDashboardCounts();
        private void DisplayLast24HoursShoeSales() => LoadDashboardCounts();
        private void DisplayLast24HoursCosmeticSales() => LoadDashboardCounts();
        private void UpdateLabelWithCount(string q, Label l) => LoadDashboardCounts();
        private void ExecuteSumQuery(string q, Label l) => LoadDashboardCounts();

        // Label clicks trigger an instant refresh
        private void lbl24earnings_Click(object sender, EventArgs e) => LoadDashboardCounts();
        private void lblShoeSales_Click(object sender, EventArgs e) => LoadDashboardCounts();
        private void lblCosmeticSales_Click(object sender, EventArgs e) => LoadDashboardCounts();
        private void lblCosmeticSold_Click(object sender, EventArgs e) => LoadDashboardCounts();
        private void lblShoesSold_Click(object sender, EventArgs e) => LoadDashboardCounts();

        // Empty event stubs to keep designer from crashing
        private void pictureBox7_Click(object sender, EventArgs e) { }
        private void label18_Click(object sender, EventArgs e) { }
        private void label12_Click(object sender, EventArgs e) { }
        private void panel6_Paint(object sender, PaintEventArgs e) { }
        private void LoginTag_Click(object sender, EventArgs e) { }
    }
}