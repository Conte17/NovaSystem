using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace NovaSystem
{
    public partial class LoginForm : Form
    {
        private readonly string devConnectionString =
            @"Data Source=DESKTOP-60H5VBK\MSSQLSERVER01;Initial Catalog=NovasystemDB;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=2;Pooling=true;";

        private readonly string clientConnectionString =
            @"Data Source=.\SQLEXPRESS;Initial Catalog=NovasystemDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;Connect Timeout=2;Pooling=true;";

        public LoginForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
        }

        // SPEED BOOST: This trick tells Windows to "pre-paint" the form.
        // It prevents the white-screen flicker common on 4GB RAM PCs.
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Button loginBtn = sender as Button;
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter credentials.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (loginBtn != null) loginBtn.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            bool loginSuccess = await Task.Run(() =>
            {
                string hashedPassword = HashPassword(password);
                return TryLogin(devConnectionString, username, hashedPassword) ||
                       TryLogin(clientConnectionString, username, hashedPassword);
            });

            if (loginSuccess)
            {
                // FIX: Instead of Hiding immediately, we load Form2 first.
                Form2 dashboard = new Form2();

                // Show the dashboard while the login is still visible
                dashboard.Show();

                // Force the dashboard to draw its buttons/panels immediately
                dashboard.Update();

                // Now hide the login form - no more "disappearing" window!
                this.Hide();
            }
            else
            {
                this.Cursor = Cursors.Default;
                if (loginBtn != null) loginBtn.Enabled = true;
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool TryLogin(string connectionString, string username, string passwordHash)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    const string query = @"SELECT UserId, Username, Fullname, Role FROM Users 
                                         WHERE Username = @Username AND PasswordHash = @PasswordHash";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;
                        cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar).Value = passwordHash;

                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.Read())
                            {
                                UserSession.UserId = Convert.ToInt32(reader["UserId"]);
                                UserSession.Username = reader["Username"].ToString();
                                UserSession.Fullname = reader["Fullname"]?.ToString();
                                UserSession.Role = reader["Role"].ToString();
                                return true;
                            }
                        }
                    }
                }
            }
            catch { return false; }
            return false;
        }

        private string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return string.Empty;
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder(64);
                for (int i = 0; i < hash.Length; i++) sb.Append(hash[i].ToString("x2"));
                return sb.ToString();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Register().Show();
        }
    }
}