using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NovaSystem
{
    public partial class Credit : Form
    {
        // Connection Tuning for 4GB RAM systems
        private const string ConnectionTuning = "Connect Timeout=5;Max Pool Size=10;Pooling=True;";
        private readonly string devConnectionString = $@"Data Source=DESKTOP-60H5VBK\MSSQLSERVER01; Initial Catalog=NovasystemDB; Integrated Security=True; TrustServerCertificate=True; {ConnectionTuning}";
        private readonly string clientConnectionString = $@"Data Source=.\SQLEXPRESS; Initial Catalog=NovasystemDB; Integrated Security=True; TrustServerCertificate=True; {ConnectionTuning}";
        private string activeConnectionString;

        private decimal cartTotal = 0;
        private int lastSaleID = 0;

        public Credit()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            activeConnectionString = ResolveConnectionString();
            SetupCartGrid();

            // Attach event for visual styling
            gridCreditRecords.DataBindingComplete += gridCreditRecords_DataBindingComplete;
        }

        private string ResolveConnectionString() => TestConnection(devConnectionString) ? devConnectionString : clientConnectionString;

        private bool TestConnection(string cs)
        {
            try { using (var c = new SqlConnection(cs)) { c.Open(); return true; } }
            catch { return false; }
        }

        private void SetupCartGrid()
        {
            dgvCart.Columns.Clear();
            dgvCart.Columns.Add("ItemType", "Type");
            dgvCart.Columns.Add("ProductName", "Product");
            dgvCart.Columns.Add("SKU", "SKU");
            dgvCart.Columns.Add("Price", "Price");
            dgvCart.Columns.Add("ItemID", "ID");
            dgvCart.Columns["ItemID"].Visible = false;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCart.MultiSelect = false;
            dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private async void Credit_Load(object sender, EventArgs e)
        {
            await Task.WhenAll(LoadDefaultProducts(), LoadCreditRecords());
        }

        // --- VISUAL RESPONSE LOGIC ---
        private async void FlashError(TextBox tb)
        {
            Color original = tb.BackColor;
            tb.BackColor = Color.MistyRose; // Soft Red
            await Task.Delay(500);
            tb.BackColor = original;
        }

        private void gridCreditRecords_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in gridCreditRecords.Rows)
            {
                if (row.Cells["Status"].Value?.ToString() == "Paid")
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                    row.DefaultCellStyle.ForeColor = Color.DarkGreen;
                }
            }
        }

        private async Task LoadDefaultProducts()
        {
            try
            {
                DataTable dt = await Task.Run(() =>
                {
                    string query = @"SELECT 'Cosmetic' AS ItemType, C.CosmeticID AS ItemID, P.ProductName, P.Category, C.SKU, P.SellingPrice, C.Status
                                     FROM Cosmetics C INNER JOIN Products P ON C.ProductID = P.ProductID WHERE C.Status = 'Available'";
                    using (SqlConnection conn = new SqlConnection(activeConnectionString))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(query, conn);
                        DataTable temp = new DataTable();
                        da.Fill(temp);
                        return temp;
                    }
                });
                productDatagrid.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show("Load Products Error: " + ex.Message); }
        }

        private async Task LoadCreditRecords()
        {
            try
            {
                DataTable dt = await Task.Run(() =>
                {
                    string query = @"
                        SELECT S.SaleID, C.FullName, C.PhoneNumber, S.TotalAmount, S.AmountPaid, 
                               (S.TotalAmount - S.AmountPaid) AS BalanceOwed, S.SaleDate, S.Status, C.CustomerID
                        FROM Sales S
                        INNER JOIN Customers C ON S.CustomerID = C.CustomerID
                        WHERE S.PaymentMethod = 'Credit'
                        ORDER BY S.SaleDate DESC";
                    using (SqlConnection conn = new SqlConnection(activeConnectionString))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(query, conn);
                        DataTable temp = new DataTable();
                        da.Fill(temp);
                        return temp;
                    }
                });
                gridCreditRecords.DataSource = dt;
                if (dt.Columns.Contains("CustomerID")) gridCreditRecords.Columns["CustomerID"].Visible = false;
            }
            catch (Exception ex) { Console.WriteLine("Load Records Error: " + ex.Message); }
        }

        // --- SEARCH LOGIC (PRODUCTS) ---
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword)) { await LoadDefaultProducts(); return; }
            btnSearch.Enabled = false;
            this.UseWaitCursor = true;
            try
            {
                DataTable dt = await Task.Run(() =>
                {
                    string query = @"
                        SELECT 'Cosmetic' AS ItemType, C.CosmeticID AS ItemID, P.ProductName, P.Category, C.SKU, P.SellingPrice, C.Status
                        FROM Cosmetics C INNER JOIN Products P ON C.ProductID = P.ProductID
                        WHERE C.Status = 'Available' AND (P.ProductName LIKE @K OR C.SKU LIKE @K)
                        UNION ALL
                        SELECT 'Shoe' AS ItemType, S.ShoeID AS ItemID, S.ShoeName AS ProductName, S.Category, S.SKU, S.SellingPrice, S.Status
                        FROM Shoes S WHERE S.Status = 'Available' AND (S.ShoeName LIKE @K OR S.SKU LIKE @K)";

                    using (SqlConnection conn = new SqlConnection(activeConnectionString))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(query, conn);
                        da.SelectCommand.Parameters.AddWithValue("@K", $"%{keyword}%");
                        DataTable temp = new DataTable();
                        da.Fill(temp);
                        return temp;
                    }
                });
                productDatagrid.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show("Search Error: " + ex.Message); }
            finally { btnSearch.Enabled = true; this.UseWaitCursor = false; }
        }

        // --- SEARCH LOGIC (CUSTOMERS) ---
        private async void btnCustomersearch_Click(object sender, EventArgs e)
        {
            string keyword = txtcustomersearch.Text.Trim();
            btnCustomersearch.Enabled = false;
            try
            {
                DataTable dt = await Task.Run(() =>
                {
                    string query = @"
                        SELECT S.SaleID, C.FullName, C.PhoneNumber, S.TotalAmount, S.AmountPaid, 
                               (S.TotalAmount - S.AmountPaid) AS BalanceOwed, S.SaleDate, S.Status, C.CustomerID
                        FROM Sales S
                        INNER JOIN Customers C ON S.CustomerID = C.CustomerID
                        WHERE S.PaymentMethod = 'Credit' AND (C.FullName LIKE @K OR C.PhoneNumber LIKE @K)
                        ORDER BY S.SaleDate DESC";

                    using (SqlConnection conn = new SqlConnection(activeConnectionString))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(query, conn);
                        da.SelectCommand.Parameters.AddWithValue("@K", $"%{keyword}%");
                        DataTable temp = new DataTable();
                        da.Fill(temp);
                        return temp;
                    }
                });
                gridCreditRecords.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show("Customer Search Error: " + ex.Message); }
            finally { btnCustomersearch.Enabled = true; }
        }

        // --- CART LOGIC ---
        private void productDatagrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = productDatagrid.Rows[e.RowIndex];

            dgvCart.Rows.Add(
                row.Cells["ItemType"].Value,
                row.Cells["ProductName"].Value,
                row.Cells["SKU"].Value,
                row.Cells["SellingPrice"].Value,
                row.Cells["ItemID"].Value
            );

            RecalculateCartTotal();
        }

        private void RecalculateCartTotal()
        {
            cartTotal = 0;
            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                if (!row.IsNewRow && row.Cells["Price"].Value != null)
                {
                    cartTotal += Convert.ToDecimal(row.Cells["Price"].Value);
                }
            }
            lblProductTotal.Text = "KSh " + cartTotal.ToString("N2");
        }

        private void gridCreditRecords_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = gridCreditRecords.Rows[e.RowIndex];
            txtNames.Text = row.Cells["FullName"].Value?.ToString() ?? "";
            txtPhone.Text = row.Cells["PhoneNumber"].Value?.ToString() ?? "";

            if (row.Cells["BalanceOwed"].Value != null)
            {
                decimal owed = Convert.ToDecimal(row.Cells["BalanceOwed"].Value);
                txtAmount.Text = owed > 0 ? owed.ToString("0.##") : "";
            }
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvCart.SelectedRows)
                {
                    if (!row.IsNewRow) dgvCart.Rows.Remove(row);
                }
                RecalculateCartTotal();
            }
            else
            {
                MessageBox.Show("Please select a full row in the cart to remove.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClearcart_Click(object sender, EventArgs e)
        {
            if (dgvCart.Rows.Count > 0)
            {
                dgvCart.Rows.Clear();
                RecalculateCartTotal();
            }
        }

        // --- CHECKOUT LOGIC ---
        private async void BtnCheckout_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(txtNames.Text)) { FlashError(txtNames); isValid = false; }
            if (string.IsNullOrWhiteSpace(txtPhone.Text)) { FlashError(txtPhone); isValid = false; }
            if (!isValid) return;

            if (dgvCart.Rows.Count == 0 || (dgvCart.Rows.Count == 1 && dgvCart.Rows[0].IsNewRow))
            {
                MessageBox.Show("Cart is empty!");
                return;
            }

            decimal paidToday = decimal.TryParse(txtAmount.Text, out var p) ? p : 0;
            this.UseWaitCursor = true;

            try
            {
                using (SqlConnection conn = new SqlConnection(activeConnectionString))
                {
                    await conn.OpenAsync();
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            string custSql = @"
                                IF NOT EXISTS (SELECT 1 FROM Customers WHERE PhoneNumber = @Phone)
                                    INSERT INTO Customers (FullName, PhoneNumber, Location, TotalDebt) VALUES (@Name, @Phone, @Loc, 0);
                                SELECT CustomerID FROM Customers WHERE PhoneNumber = @Phone;";
                            SqlCommand cCmd = new SqlCommand(custSql, conn, trans);
                            cCmd.Parameters.AddWithValue("@Name", txtNames.Text.Trim());
                            cCmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
                            cCmd.Parameters.AddWithValue("@Loc", txtLocation.Text.Trim());
                            int custID = Convert.ToInt32(await cCmd.ExecuteScalarAsync());

                            string saleSql = @"INSERT INTO Sales (SaleDate, UserID, TotalAmount, AmountPaid, PaymentMethod, Status, CustomerID) 
                                               VALUES (GETDATE(), @UID, @Tot, @Paid, 'Credit', 'Owed', @CID); SELECT SCOPE_IDENTITY();";
                            SqlCommand sCmd = new SqlCommand(saleSql, conn, trans);
                            sCmd.Parameters.AddWithValue("@UID", UserSession.UserId);
                            sCmd.Parameters.AddWithValue("@Tot", cartTotal);
                            sCmd.Parameters.AddWithValue("@Paid", paidToday);
                            sCmd.Parameters.AddWithValue("@CID", custID);
                            lastSaleID = Convert.ToInt32(await sCmd.ExecuteScalarAsync());

                            decimal debtToTrack = cartTotal - paidToday;
                            string dbtSql = "UPDATE Customers SET TotalDebt = TotalDebt + @Debt WHERE CustomerID = @CID";
                            SqlCommand dCmd = new SqlCommand(dbtSql, conn, trans);
                            dCmd.Parameters.AddWithValue("@Debt", debtToTrack);
                            dCmd.Parameters.AddWithValue("@CID", custID);
                            await dCmd.ExecuteNonQueryAsync();

                            foreach (DataGridViewRow row in dgvCart.Rows)
                            {
                                if (row.IsNewRow) continue;
                                string type = row.Cells["ItemType"].Value.ToString();
                                string table = (type == "Cosmetic") ? "Cosmetics" : "Shoes";
                                string idCol = (type == "Cosmetic") ? "CosmeticID" : "ShoeID";
                                string upInv = $"UPDATE {table} SET Status = 'Sold' WHERE {idCol} = @ID";
                                SqlCommand invCmd = new SqlCommand(upInv, conn, trans);
                                invCmd.Parameters.AddWithValue("@ID", row.Cells["ItemID"].Value);
                                await invCmd.ExecuteNonQueryAsync();
                            }

                            trans.Commit();
                            MessageBox.Show("Credit Transaction Finalized!");
                            PrintReceipt();
                            ResetForm();
                        }
                        catch (Exception ex) { trans.Rollback(); MessageBox.Show("Error: " + ex.Message); }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("System Error: " + ex.Message); }
            finally { this.UseWaitCursor = false; }
        }

        // --- UPDATE RECORD (PAY DEBT) ---
        private async void btnupdate_Click(object sender, EventArgs e)
        {
            bool cartHasItems = false;
            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                if (!row.IsNewRow && row.Cells["ProductName"].Value != null)
                {
                    cartHasItems = true;
                    break;
                }
            }

            if (cartHasItems)
            {
                MessageBox.Show("Cannot process debt payment while items are in the checkout cart. Clear the cart first.", "Action Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (gridCreditRecords.CurrentRow == null) { MessageBox.Show("Select a record to update."); return; }
            if (!decimal.TryParse(txtAmount.Text, out decimal payingAmount) || payingAmount <= 0)
            {
                FlashError(txtAmount);
                return;
            }

            int sID = Convert.ToInt32(gridCreditRecords.CurrentRow.Cells["SaleID"].Value);
            int cID = Convert.ToInt32(gridCreditRecords.CurrentRow.Cells["CustomerID"].Value);
            decimal currentOwed = Convert.ToDecimal(gridCreditRecords.CurrentRow.Cells["BalanceOwed"].Value);

            if (currentOwed <= 0) { MessageBox.Show("This record is already fully paid."); return; }

            this.UseWaitCursor = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(activeConnectionString))
                {
                    await conn.OpenAsync();
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        string sql1 = "UPDATE Sales SET AmountPaid = AmountPaid + @P WHERE SaleID = @SID";
                        SqlCommand cmd1 = new SqlCommand(sql1, conn, trans);
                        cmd1.Parameters.AddWithValue("@P", payingAmount);
                        cmd1.Parameters.AddWithValue("@SID", sID);
                        await cmd1.ExecuteNonQueryAsync();

                        string sql2 = "UPDATE Customers SET TotalDebt = TotalDebt - @P WHERE CustomerID = @CID";
                        SqlCommand cmd2 = new SqlCommand(sql2, conn, trans);
                        cmd2.Parameters.AddWithValue("@P", payingAmount);
                        cmd2.Parameters.AddWithValue("@CID", cID);
                        await cmd2.ExecuteNonQueryAsync();

                        if (payingAmount >= currentOwed)
                        {
                            string sql3 = "UPDATE Sales SET Status = 'Paid', AmountPaid = TotalAmount WHERE SaleID = @SID";
                            SqlCommand cmd3 = new SqlCommand(sql3, conn, trans);
                            cmd3.Parameters.AddWithValue("@SID", sID);
                            await cmd3.ExecuteNonQueryAsync();
                        }

                        trans.Commit();
                        MessageBox.Show("Payment recorded successfully!");
                        await LoadCreditRecords();
                        ResetForm();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { this.UseWaitCursor = false; }
        }

        private void PrintReceipt()
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += (s, ev) =>
            {
                Graphics g = ev.Graphics;
                Font f = new Font("Courier New", 9);
                g.DrawString("GOLDEN ANGLES AFRICA", new Font("Courier New", 12, FontStyle.Bold), Brushes.Black, 10, 10);
                g.DrawString($"CREDIT VOUCHER: CR-{lastSaleID:D5}", f, Brushes.Black, 10, 35);
                g.DrawString($"Date: {DateTime.Now:G}", f, Brushes.Black, 10, 55);
                g.DrawString($"Customer: {txtNames.Text}", f, Brushes.Black, 10, 75);
                g.DrawString(new string('-', 40), f, Brushes.Black, 10, 95);

                int y = 115;
                foreach (DataGridViewRow row in dgvCart.Rows)
                {
                    if (row.IsNewRow) continue;
                    g.DrawString($"{row.Cells["ProductName"].Value} - {row.Cells["Price"].Value}", f, Brushes.Black, 10, y);
                    y += 20;
                }

                g.DrawString(new string('-', 40), f, Brushes.Black, 10, y);
                g.DrawString($"Total Amount: {cartTotal:N2}", f, Brushes.Black, 10, y + 20);
                g.DrawString($"Amount Paid: {txtAmount.Text}", f, Brushes.Black, 10, y + 40);
                g.DrawString($"BALANCE OWED: {cartTotal - (decimal.TryParse(txtAmount.Text, out var p) ? p : 0):N2}",
                               new Font("Courier New", 10, FontStyle.Bold), Brushes.Black, 10, y + 70);
            };
            PrintPreviewDialog ppd = new PrintPreviewDialog { Document = pd };
            ppd.ShowDialog();
        }

        private void ResetForm()
        {
            dgvCart.Rows.Clear();
            cartTotal = 0;
            lblProductTotal.Text = "KSh 0.00";
            txtNames.Clear();
            txtPhone.Clear();
            txtLocation.Clear();
            txtAmount.Clear();
            _ = LoadCreditRecords();
        }

        private void SmoothNavigate(Form nextForm) { nextForm.Show(); this.Hide(); }
        private void btnHome_Click(object sender, EventArgs e) => SmoothNavigate(new Form2());
        private void BtnCosmetics_Click(object sender, EventArgs e) => SmoothNavigate(new Cosmetics());
        private void BtnInventory_Click(object sender, EventArgs e) => SmoothNavigate(new Inventory());
        private void button1_Click(object sender, EventArgs e) => SmoothNavigate(new ShoeWear());
        private void BtnOrders_Click(object sender, EventArgs e) => SmoothNavigate(new Orders());
        private void BtnReports_Click(object sender, EventArgs e) => SmoothNavigate(new reports());
    }
}