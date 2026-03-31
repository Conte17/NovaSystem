using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace NovaSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
        }

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
            // FIX: This safely identifies the button without needing the name 'button1'
            if (!(sender is Button loginBtn)) return;

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter credentials.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // UI Feedback
            loginBtn.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            bool loginSuccess = await Task.Run(() => PerformLogin(username, password));

            if (loginSuccess)
            {
                Form2 dashboard = new Form2();
                dashboard.Show();
                dashboard.Update();
                this.Hide();
            }
            else
            {
                this.Cursor = Cursors.Default;
                loginBtn.Enabled = true;
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool PerformLogin(string username, string password)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Program.ConnectionString))
                {
                    conn.Open();
                    string hashedPassword = HashPassword(password);

                    const string query = @"SELECT UserId, Username, Fullname, Role FROM Users WITH (NOLOCK) 
                                         WHERE Username = @Username AND PasswordHash = @PasswordHash";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;
                        cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar).Value = hashedPassword;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // UserSession stores the logged-in user's info
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
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = SHA256.HashData(bytes);

            StringBuilder sb = new StringBuilder(64);
            foreach (byte b in hash) sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            new Register().Show();
            this.Hide();
        }
    }

  
    }
