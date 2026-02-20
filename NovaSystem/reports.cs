using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace NovaSystem
{
    public partial class reports : Form
    {
        private readonly string devConnectionString = @"Data Source=DESKTOP-60H5VBK\MSSQLSERVER01;Initial Catalog=NovasystemDB;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=5;";
        private readonly string clientConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=NovasystemDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;Connect Timeout=5;";
        private string activeConnectionString;

        private PrintDocument printDoc = new PrintDocument();
        private PrintPreviewDialog previewDlg = new PrintPreviewDialog();
        private int rowIndex = 0;
        private string currentReportTitle = "GOLDEN ANGELS REPORT";

        public reports()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            activeConnectionString = ResolveConnectionString();
            printDoc.PrintPage += PrintDocument_PrintPage;
            printDoc.DefaultPageSettings.Landscape = true;
        }

        private async void reports_Load(object sender, EventArgs e)
        {
            SetUserLabel();

            // Load initial data and counts simultaneously for speed
            await Task.WhenAll(
                RefreshCosmeticsDataAsync(),
                UpdateCosmeticBatchCountAsync()
            );
        }

        private void SetUserLabel()
        {
            LoginTag.Text = $"Logged in: {UserSession.Fullname ?? UserSession.Username}";
        }

        // ========================= CORE DATA ENGINES (FIXED CONVERSION ERROR) =========================

        private async Task RefreshCosmeticsDataAsync()
        {
            currentReportTitle = "GOLDEN ANGLES AFRICA COSMETICS BATCH REPORT";

            // FIX: Added CAST to VARCHAR to prevent "N/A" conversion errors
            string query = @"
            SELECT 
                ISNULL(CAST(C.BatchNumber AS VARCHAR(50)), 'N/A') AS [Batch No],
                P.ProductName AS [Product],
                P.Category,
                P.Status,
                ISNULL(SI.UnitPrice, P.SellingPrice) AS [Price],
                ISNULL(U.Fullname, 'System') AS [Sold By],
                ISNULL(S.SaleDate, C.CreatedAt) AS [Date]
            FROM Products P
            LEFT JOIN Cosmetics C ON P.ProductID = C.ProductID
            LEFT JOIN SaleItems SI ON P.ProductID = SI.ProductID
            LEFT JOIN Sales S ON SI.SaleID = S.SaleID
            LEFT JOIN Users U ON S.UserID = U.UserID
            WHERE 1=1";

            await ApplyFiltersAndExecuteAsync(query);
        }

        private async Task ApplyFiltersAndExecuteAsync(string baseQuery)
        {
            var parameters = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(txtBatchNo.Text))
            {
                // FIX: CAST BatchNumber to VARCHAR so LIKE works with the search string
                baseQuery += " AND (P.ProductName LIKE @batch OR CAST(C.BatchNumber AS VARCHAR(50)) LIKE @batch)";
                parameters.Add(new SqlParameter("@batch", $"%{txtBatchNo.Text.Trim()}%"));
            }

            if (ComboCategory.SelectedIndex > 0 && ComboCategory.Text != "All")
            {
                baseQuery += " AND P.Category = @cat";
                parameters.Add(new SqlParameter("@cat", ComboCategory.Text));
            }

            baseQuery += " ORDER BY P.ProductName ASC";
            await ExecuteReportQueryAsync(baseQuery, parameters);
        }

        private async Task ExecuteReportQueryAsync(string query, List<SqlParameter> parameters)
        {
            dgvReport.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                DataTable dt = await Task.Run(() =>
                {
                    using (SqlConnection conn = new SqlConnection(activeConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            if (parameters != null) cmd.Parameters.AddRange(parameters.ToArray());
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable tempTable = new DataTable();
                            da.Fill(tempTable);
                            return tempTable;
                        }
                    }
                });

                dgvReport.DataSource = dt;
                FormatReportGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report: " + ex.Message);
            }
            finally
            {
                dgvReport.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        private async Task UpdateCosmeticBatchCountAsync()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(activeConnectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(DISTINCT BatchNumber) FROM Cosmetics", conn))
                    {
                        var result = await cmd.ExecuteScalarAsync();
                        lblCosmeticsReport.Text = result?.ToString() ?? "0";
                    }
                }
            }
            catch { lblCosmeticsReport.Text = "0"; }
        }

        private void FormatReportGrid()
        {
            dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReport.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            if (dgvReport.Columns.Contains("Price")) dgvReport.Columns["Price"].DefaultCellStyle.Format = "N2";
            if (dgvReport.Columns.Contains("Total")) dgvReport.Columns["Total"].DefaultCellStyle.Format = "N2";
        }

        // ========================= UI EVENT HANDLERS (DESIGNER COMPATIBILITY) =========================

        private async void btnFilter_Click(object sender, EventArgs e) => await RefreshCosmeticsDataAsync();

        private async void btnStaffFilter_Click(object sender, EventArgs e)
        {
            currentReportTitle = "GOLDEN ANGLES AFRICA STAFF ORDER REPORT";
            string query = @"SELECT OrderID AS [ID], FORMAT(OrderDate, 'dd/MM/yyyy') AS [Date], StaffName AS [Staff], TotalAmount AS [Total] 
                             FROM Orders WHERE StaffName LIKE @staff ORDER BY OrderDate DESC";

            var p = new List<SqlParameter> { new SqlParameter("@staff", $"%{txtStaffname.Text.Trim()}%") };
            await ExecuteReportQueryAsync(query, p);
        }

        private async void lblInventoryStatus_Click(object sender, EventArgs e)
        {
            currentReportTitle = "GOLDEN ANGLES AFRICA INVENTORY STATUS REPORT";
            string query = "SELECT ProductName [Product], Category, Quantity [Stock], Status, SellingPrice [Price] FROM Products";
            await ExecuteReportQueryAsync(query, null);
        }

        private async void lblStaffsales_Click_1(object sender, EventArgs e)
        {
            currentReportTitle = "GOLDEN ANGLES AFRICA STAFF ORDER REPORT";
            string query = "SELECT OrderID [ID], OrderDate [Date], StaffName [Staff], TotalAmount [Total] FROM Orders ORDER BY OrderDate DESC";
            await ExecuteReportQueryAsync(query, null);
        }

        private async void lblCosmeticsReport_Click(object sender, EventArgs e) => await RefreshCosmeticsDataAsync();
        private async void label14_Click(object sender, EventArgs e) => await RefreshCosmeticsDataAsync();

        private void btnLogout_Click(object sender, EventArgs e)
        {
            UserSession.Clear();
            new LoginForm().Show();
            this.Close();
        }

        private void BtnCheckout_Click(object sender, EventArgs e)
        {
            if (dgvReport.Rows.Count == 0) return;
            rowIndex = 0;
            previewDlg.Document = printDoc;
            previewDlg.WindowState = FormWindowState.Maximized;
            previewDlg.ShowDialog();
        }

        // ========================= PRINT ENGINE =========================

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font titleFont = new Font("Arial", 16, FontStyle.Bold);
            Font headFont = new Font("Arial", 9, FontStyle.Bold);
            Font bodyFont = new Font("Arial", 8);
            float x = e.MarginBounds.Left, y = 100;
            int totalWidth = e.MarginBounds.Width;

            e.Graphics.DrawString(currentReportTitle, titleFont, Brushes.Black, x, 40);
            e.Graphics.DrawString($"Report Date: {DateTime.Now:g}", bodyFont, Brushes.Gray, x, 70);
            e.Graphics.DrawLine(Pens.Black, x, 90, e.MarginBounds.Right, 90);

            var cols = dgvReport.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).ToList();
            int colWidth = totalWidth / Math.Max(1, cols.Count);

            for (int i = 0; i < cols.Count; i++)
            {
                e.Graphics.FillRectangle(Brushes.LightGray, x + (i * colWidth), y, colWidth, 25);
                e.Graphics.DrawRectangle(Pens.Black, x + (i * colWidth), y, colWidth, 25);
                e.Graphics.DrawString(cols[i].HeaderText, headFont, Brushes.Black, x + (i * colWidth) + 2, y + 5);
            }
            y += 25;

            while (rowIndex < dgvReport.Rows.Count)
            {
                if (y + 25 > e.MarginBounds.Bottom) { e.HasMorePages = true; return; }
                for (int i = 0; i < cols.Count; i++)
                {
                    string val = dgvReport.Rows[rowIndex].Cells[cols[i].Index].Value?.ToString() ?? "";
                    e.Graphics.DrawRectangle(Pens.Black, x + (i * colWidth), y, colWidth, 20);
                    e.Graphics.DrawString(val, bodyFont, Brushes.Black, new RectangleF(x + (i * colWidth) + 2, y + 2, colWidth - 2, 18));
                }
                y += 20; rowIndex++;
            }
            e.HasMorePages = false; rowIndex = 0;
        }

        // ========================= NAVIGATION & HELPERS =========================

        private void SmoothNavigate(Form nextForm) { nextForm.Show(); this.Hide(); }
        private void btnHome_Click(object sender, EventArgs e) => SmoothNavigate(new Form2());
        private void btnSales_Click(object sender, EventArgs e) => SmoothNavigate(new Sales());
        private void BtnInventory_Click(object sender, EventArgs e) => SmoothNavigate(new Inventory());
        private void BtnCosmetics_Click(object sender, EventArgs e) => SmoothNavigate(new Cosmetics());
        private void button1_Click(object sender, EventArgs e) => SmoothNavigate(new ShoeWear());

        private string ResolveConnectionString() => TestConnection(devConnectionString) ? devConnectionString : clientConnectionString;
        private bool TestConnection(string cs) { try { using (var c = new SqlConnection(cs)) { c.Open(); return true; } } catch { return false; } }

        // Designer required paint methods
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void panel3_Paint(object sender, PaintEventArgs e) { }
    }
}