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
        // RAM optimization and faster timeout to prevent UI hanging
        private readonly string devConnectionString = @"Data Source=DESKTOP-60H5VBK\MSSQLSERVER01;Initial Catalog=NovasystemDB;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=2;";
        private readonly string clientConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=NovasystemDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;Connect Timeout=2;";

        // Static cache ensures we only "search" for the database once per application session
        private static string cachedConnectionString;

        private PrintDocument printDoc = new PrintDocument();
        private PrintPreviewDialog previewDlg = new PrintPreviewDialog();
        private int rowIndex = 0;
        private string currentReportTitle = "GENERAL REPORT";

        public reports()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            // Set up printing defaults
            printDoc.PrintPage += PrintDocument_PrintPage;
            printDoc.DefaultPageSettings.Landscape = true; // Better for wide data tables
        }

        private async void reports_Load(object sender, EventArgs e)
        {
            SetUserLabel();

            try
            {
                // FIX: Resolve connection in a background task to stop the 4-second hang
                if (string.IsNullOrEmpty(cachedConnectionString))
                {
                    this.Cursor = Cursors.WaitCursor;
                    cachedConnectionString = await Task.Run(() => ResolveConnectionString());
                    this.Cursor = Cursors.Default;
                }

                // Load initial data and counts simultaneously
                await Task.WhenAll(
                    RefreshCosmeticsDataAsync(),
                    UpdateCosmeticBatchCountAsync()
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Startup Error: " + ex.Message);
            }
        }

        private void SetUserLabel()
        {
            LoginTag.Text = $"Logged in: {UserSession.Fullname ?? UserSession.Username}";
        }

        private string ResolveConnectionString()
        {
            if (TestConnection(devConnectionString)) return devConnectionString;
            if (TestConnection(clientConnectionString)) return clientConnectionString;
            return clientConnectionString; // Default fallback
        }

        private bool TestConnection(string cs)
        {
            try { using (SqlConnection c = new SqlConnection(cs)) { c.Open(); return true; } }
            catch { return false; }
        }

        // ========================= CORE DATA ENGINES =========================

        private async Task RefreshCosmeticsDataAsync()
        {
            if (string.IsNullOrEmpty(cachedConnectionString)) return;
            currentReportTitle = "COSMETICS BATCH REPORT";

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
            if (string.IsNullOrEmpty(cachedConnectionString)) return;

            dgvReport.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                DataTable dt = await Task.Run(() =>
                {
                    using (SqlConnection conn = new SqlConnection(cachedConnectionString))
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
            if (string.IsNullOrEmpty(cachedConnectionString)) return;
            try
            {
                using (SqlConnection conn = new SqlConnection(cachedConnectionString))
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
            dgvReport.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            if (dgvReport.Columns.Contains("Price")) dgvReport.Columns["Price"].DefaultCellStyle.Format = "N2";
            if (dgvReport.Columns.Contains("Total")) dgvReport.Columns["Total"].DefaultCellStyle.Format = "N2";
        }

        // ========================= UI EVENT HANDLERS =========================

        private async void btnFilter_Click(object sender, EventArgs e) => await RefreshCosmeticsDataAsync();

        private async void btnStaffFilter_Click(object sender, EventArgs e)
        {
            currentReportTitle = "STAFF ORDER REPORT";
            string query = @"SELECT OrderID AS [ID], FORMAT(OrderDate, 'dd/MM/yyyy') AS [Date], StaffName AS [Staff], TotalAmount AS [Total] 
                             FROM Orders WHERE StaffName LIKE @staff ORDER BY OrderDate DESC";

            var p = new List<SqlParameter> { new SqlParameter("@staff", $"%{txtStaffname.Text.Trim()}%") };
            await ExecuteReportQueryAsync(query, p);
        }

        private async void lblInventoryStatus_Click(object sender, EventArgs e)
        {
            currentReportTitle = "INVENTORY STATUS REPORT";
            string query = "SELECT ProductName [Product], Category, Quantity [Stock], Status, SellingPrice [Price] FROM Products";
            await ExecuteReportQueryAsync(query, null);
        }

        private async void lblStaffsales_Click_1(object sender, EventArgs e)
        {
            currentReportTitle = "STAFF ORDER REPORT";
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
            if (dgvReport.Rows.Count == 0)
            {
                MessageBox.Show("No data available to print.", "Print Report", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            rowIndex = 0;
            previewDlg.Document = printDoc;
            previewDlg.WindowState = FormWindowState.Maximized;
            previewDlg.ShowDialog();
        }

        // ========================= PRINT ENGINE (PROFESSIONAL HEADER) =========================

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font titleFont = new Font("Segoe UI", 18, FontStyle.Bold);
            Font subTitleFont = new Font("Segoe UI", 10, FontStyle.Regular);
            Font headFont = new Font("Segoe UI", 9, FontStyle.Bold);
            Font bodyFont = new Font("Segoe UI", 8, FontStyle.Regular);

            float x = e.MarginBounds.Left;
            float y = 40;
            int totalWidth = e.MarginBounds.Width;
            StringFormat center = new StringFormat { Alignment = StringAlignment.Center };

            // 1. BUSINESS HEADER
            g.DrawString("GOLDEN ANGLES AFRICA", titleFont, Brushes.Black, new RectangleF(0, y, e.PageBounds.Width, 35), center);
            y += 35;
            g.DrawString("Location: Nyamakima CBC Plaza, 3rd Floor, Room T7 | P.O Box: 22450-00400", subTitleFont, Brushes.Black, new RectangleF(0, y, e.PageBounds.Width, 20), center);
            y += 18;
            g.DrawString("Tel: 0714484838 / 0116923796", subTitleFont, Brushes.Black, new RectangleF(0, y, e.PageBounds.Width, 20), center);
            y += 30;

            // 2. REPORT METADATA
            g.DrawString(currentReportTitle, headFont, Brushes.Black, x, y);
            string meta = $"Printed on: {DateTime.Now:F} | User: {UserSession.Fullname ?? UserSession.Username}";
            g.DrawString(meta, bodyFont, Brushes.DimGray, e.MarginBounds.Right - g.MeasureString(meta, bodyFont).Width, y);
            y += 12;
            g.DrawLine(new Pen(Color.Black, 1.5f), x, y, e.MarginBounds.Right, y);
            y += 15;

            // 3. TABLE HEADERS
            var visibleCols = dgvReport.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).ToList();
            if (visibleCols.Count == 0) return;
            int colWidth = totalWidth / visibleCols.Count;

            for (int i = 0; i < visibleCols.Count; i++)
            {
                g.FillRectangle(Brushes.LightGray, x + (i * colWidth), y, colWidth, 25);
                g.DrawRectangle(Pens.Black, x + (i * colWidth), y, colWidth, 25);
                g.DrawString(visibleCols[i].HeaderText, headFont, Brushes.Black, x + (i * colWidth) + 5, y + 5);
            }
            y += 25;

            // 4. DATA ROWS
            while (rowIndex < dgvReport.Rows.Count)
            {
                if (y + 25 > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }

                DataGridViewRow row = dgvReport.Rows[rowIndex];
                for (int i = 0; i < visibleCols.Count; i++)
                {
                    string cellValue = row.Cells[visibleCols[i].Index].Value?.ToString() ?? "";
                    g.DrawRectangle(Pens.Black, x + (i * colWidth), y, colWidth, 22);
                    g.DrawString(cellValue, bodyFont, Brushes.Black, new RectangleF(x + (i * colWidth) + 5, y + 4, colWidth - 5, 18));
                }
                y += 22;
                rowIndex++;
            }

            e.HasMorePages = false;
            rowIndex = 0;
        }

        // ========================= NAVIGATION & HELPERS =========================

        private void SmoothNavigate(Form nextForm) { nextForm.Show(); this.Hide(); }
        private void btnHome_Click(object sender, EventArgs e) => SmoothNavigate(new Form2());
        private void btnSales_Click(object sender, EventArgs e) => SmoothNavigate(new Sales());
        private void BtnInventory_Click(object sender, EventArgs e) => SmoothNavigate(new Inventory());
        private void BtnCosmetics_Click(object sender, EventArgs e) => SmoothNavigate(new Cosmetics());
        private void button1_Click(object sender, EventArgs e) => SmoothNavigate(new ShoeWear());

        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void panel3_Paint(object sender, PaintEventArgs e) { }
    }
}