namespace NovaSystem
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            label1 = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            pictureBox4 = new PictureBox();
            pictureBox3 = new PictureBox();
            BtnLogin = new Button();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.New_Project__18_;
            pictureBox1.Location = new Point(-26, -203);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1297, 1105);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.New_Project__19_;
            pictureBox2.Location = new Point(997, 2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(192, 169);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Calibri", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlDarkDark;
            label1.Location = new Point(967, 192);
            label1.Name = "label1";
            label1.Size = new Size(262, 41);
            label1.TabIndex = 3;
            label1.Text = "Login To continue";
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.LightGray;
            txtUsername.BorderStyle = BorderStyle.None;
            txtUsername.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUsername.Location = new Point(967, 297);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(335, 25);
            txtUsername.TabIndex = 5;
            txtUsername.Text = "Enter your username";
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.Silver;
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPassword.Location = new Point(967, 413);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(335, 25);
            txtPassword.TabIndex = 7;
            txtPassword.Text = "Enter your Password";
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Transparent;
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(915, 243);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(406, 134);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 6;
            pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Transparent;
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(915, 357);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(406, 134);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 8;
            pictureBox3.TabStop = false;
            // 
            // BtnLogin
            // 
            BtnLogin.Font = new Font("Century Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnLogin.ForeColor = SystemColors.ButtonHighlight;
            BtnLogin.Image = Properties.Resources.New_Project__23_;
            BtnLogin.ImageAlign = ContentAlignment.BottomCenter;
            BtnLogin.Location = new Point(985, 519);
            BtnLogin.Name = "BtnLogin";
            BtnLogin.Size = new Size(317, 71);
            BtnLogin.TabIndex = 9;
            BtnLogin.Text = "Login";
            BtnLogin.UseVisualStyleBackColor = true;
            BtnLogin.Click += button1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Calibri", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ControlDarkDark;
            label2.Location = new Point(141, 600);
            label2.Name = "label2";
            label2.Size = new Size(201, 41);
            label2.TabIndex = 10;
            label2.Text = "Register User";
            label2.Click += label2_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1350, 707);
            Controls.Add(label2);
            Controls.Add(BtnLogin);
            Controls.Add(txtPassword);
            Controls.Add(pictureBox3);
            Controls.Add(txtUsername);
            Controls.Add(pictureBox4);
            Controls.Add(label1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Label label1;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private PictureBox pictureBox4;
        private PictureBox pictureBox3;
        private Button BtnLogin;
        private Label label2;
    }
}