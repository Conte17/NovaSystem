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
        private const string TuningOptions = "Connect Timeout=2;Pooling=true;Max Pool Size=10;Packet Size=4096;";

        private readonly string devConnectionString =
            $@"Data Source=DESKTOP-60H5VBK\MSSQLSERVER01;Initial Catalog=NovasystemDB;Integrated Security=True;TrustServerCertificate=True;{TuningOptions}";

        private readonly string clientConnectionString =
            $@"Data Source=.\SQLEXPRESS;Initial Catalog=NovasystemDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;{TuningOptions}";

        private static string activeConnectionString;

        public Form2()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED - Smooth UI painting
                return cp;
            }
        }

        private async void Form2_Load(object sender, EventArgs e)
        {
            LoginTag.Text = !string.IsNullOrWhiteSpace(UserSession.Fullname)
                ? $"Logged in: {UserSession.Fullname}"
                : $"Logged in as: {UserSession.Username}";

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

        private async Task<string> ResolveConnectionAsync()
        {
            var t1 = Task.Run(() => TestConnection(devConnectionString) ? devConnectionString : null);
            var t2 = Task.Run(() => TestConnection(clientConnectionString) ? clientConnectionString : null);

            var firstToRespond = await Task.WhenAny(t1, t2);
            string result = await firstToRespond;
            if (result != null) return result;
            return await (t1 == firstToRespond ? t2 : t1) ?? null;
        }

        private bool TestConnection(string connectionString)
        {
            try { using (SqlConnection conn = new SqlConnection(connectionString)) { conn.Open(); return true; } }
            catch { return false; }
        }

        private async Task LoadDashboardCountsAsync()
        {
            if (string.IsNullOrEmpty(activeConnectionString)) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(activeConnectionString))
                {
                    await conn.OpenAsync();
                    const string megaQuery = @"
                        SELECT 
                        (SELECT COUNT(*) FROM Cosmetics WHERE Status = 'Available'),
                        (SELECT COUNT(*) FROM Shoes WHERE Status = 'Available'),
                        (SELECT COUNT(*) FROM Cosmetics WHERE Status = 'Sold'),
                        (SELECT COUNT(*) FROM Shoes WHERE Status = 'Sold'),
                        (SELECT ISNULL(SUM(TotalAmount), 0) FROM Sales WHERE SaleDate >= DATEADD(HOUR, -24, GETDATE())),
                        (SELECT ISNULL(SUM(SI.UnitPrice * SI.Quantity), 0) FROM SaleItems SI JOIN Sales S ON SI.SaleID = S.SaleID WHERE SI.ProductType = 'Shoe' AND S.SaleDate >= DATEADD(HOUR, -24, GETDATE())),
                        (SELECT ISNULL(SUM(SI.UnitPrice * SI.Quantity), 0) FROM SaleItems SI JOIN Sales S ON SI.SaleID = S.SaleID WHERE SI.ProductType = 'Cosmetic' AND S.SaleDate >= DATEADD(HOUR, -24, GETDATE()))";

                    using (SqlCommand cmd = new SqlCommand(megaQuery, conn))
                    {
                        using (SqlDataReader rdr = await cmd.ExecuteReaderAsync(CommandBehavior.SequentialAccess))
                        {
                            if (await rdr.ReadAsync())
                            {
                                // Ordinal access is the fastest possible way to read SQL data
                                lblCosmeticCounter.Text = rdr.GetInt32(0).ToString();
                                lblShoeCounter.Text = rdr.GetInt32(1).ToString();
                                lblCosmeticSold.Text = rdr.GetInt32(2).ToString();
                                lblShoesSold.Text = rdr.GetInt32(3).ToString();

                                lbl24earnings.Text = "KSh " + (rdr.IsDBNull(4) ? 0m : rdr.GetDecimal(4)).ToString("N2");
                                lblShoeSales.Text = "KSh " + (rdr.IsDBNull(5) ? 0m : rdr.GetDecimal(5)).ToString("N2");
                                lblCosmeticSales.Text = "KSh " + (rdr.IsDBNull(6) ? 0m : rdr.GetDecimal(6)).ToString("N2");
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void NavigateTo(Form nextForm)
        {
            nextForm.Show();
            nextForm.Update();
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

        private void lbl24earnings_Click(object sender, EventArgs e) => LoadDashboardCounts();
        private void lblShoeSales_Click(object sender, EventArgs e) => LoadDashboardCounts();
        private void lblCosmeticSales_Click(object sender, EventArgs e) => LoadDashboardCounts();
        private void lblCosmeticSold_Click(object sender, EventArgs e) => LoadDashboardCounts();
        private void lblShoesSold_Click(object sender, EventArgs e) => LoadDashboardCounts();

        private void pictureBox7_Click(object sender, EventArgs e) { }
        private void label18_Click(object sender, EventArgs e) { }
        private void label12_Click(object sender, EventArgs e) { }
        private void panel6_Paint(object sender, PaintEventArgs e) { }
        private void LoginTag_Click(object sender, EventArgs e) { }

        private void btnCredit_Click(object sender, EventArgs e)
        {
            new Credit().Show();
            this.Hide();
        }
    }
}