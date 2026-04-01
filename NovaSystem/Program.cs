using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace NovaSystem
{
    internal static class Program
    {
        // Globally accessible connection string
        public static string ConnectionString { get; private set; }

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // 1. Find the database before showing any forms
            ConnectionString = ResolveDatabase();

            if (string.IsNullOrEmpty(ConnectionString))
            {
                MessageBox.Show("CRITICAL ERROR: Could not connect to the database (Server or SQLEXPRESS). The application will now close.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Start the app
            Application.Run(new LoginForm());
        }

        private static string ResolveDatabase()
        {
            // Tuning parameters for speed
            string tuning = "Pooling=true;Max Pool Size=20;Connect Timeout=2;TrustServerCertificate=True;Integrated Security=True;";

            string dev = $@"Data Source=DESKTOP-60H5VBK\MSSQLSERVER01; Initial Catalog=NovasystemDB; {tuning}";
            string client = $@"Data Source=.\SQLEXPRESS; Initial Catalog=NovasystemDB; {tuning}";

            if (TryConn(dev)) return dev;
            if (TryConn(client)) return client;

            return null;
        }

        private static bool TryConn(string cs)
        {
            try
            {
                using (var c = new SqlConnection(cs)) { c.Open(); return true; }
            }
            catch { return false; }
        }
    }
}