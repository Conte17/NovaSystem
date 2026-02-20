using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace NovaSystem
{
    public partial class Register : Form
    {
        // Your database connection string
        private readonly string connectionString =
            @"Data Source=DESKTOP-60H5VBK\MSSQLSERVER01;
              Initial Catalog=NovasystemDB;
              Integrated Security=True;
              Trust Server Certificate=True;";

        public Register()
        {
            InitializeComponent();

            // Wire events
            this.Load += Register_Load;
            BtnSave.Click += BtnSave_Click;    // FIXED BUTTON NAME
        }

        // Load roles when form opens
        private void Register_Load(object sender, EventArgs e)
        {
            cmbRole.Items.Clear();
            cmbRole.Items.Add("Admin");
            cmbRole.Items.Add("Sales");
            cmbRole.SelectedIndex = -1;
        }

        // Password hashing (SHA256)
        private string HashPassword(string password)
        {
            if (password == null) return string.Empty;

            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
            }
        }

        // SAVE USER
        private void BtnSave_Click(object sender, EventArgs e)
        {
            string fullname = txtFullName.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text; // do not trim
            string role = cmbRole.SelectedItem?.ToString();

            // Validation
            if (string.IsNullOrWhiteSpace(fullname) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrWhiteSpace(role))
            {
                MessageBox.Show("Please fill in all fields.",
                    "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hashedPassword = HashPassword(password);

            string query =
                "INSERT INTO Users (Fullname, Username, PasswordHash, Role) " +
                "VALUES (@Fullname, @Username, @PasswordHash, @Role)";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Fullname", fullname);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                    cmd.Parameters.AddWithValue("@Role", role);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("User saved successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear fields
                txtFullName.Clear();
                txtUsername.Clear();
                txtPassword.Clear();
                cmbRole.SelectedIndex = -1;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Duplicate username
                {
                    MessageBox.Show("Username already exists.",
                        "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Database error: " + ex.Message,
                        "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
