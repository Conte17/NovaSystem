namespace NovaSystem
{
    partial class Register
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Register));
            panel1 = new Panel();
            panel2 = new Panel();
            pictureBox1 = new PictureBox();
            LoginTag = new Label();
            label1 = new Label();
            txtFullName = new TextBox();
            label2 = new Label();
            txtUsername = new TextBox();
            label3 = new Label();
            txtPassword = new TextBox();
            label4 = new Label();
            cmbRole = new ComboBox();
            label5 = new Label();
            BtnSave = new Button();
            label6 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.MediumBlue;
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(314, 785);
            panel1.TabIndex = 28;
            // 
            // panel2
            // 
            panel2.Controls.Add(pictureBox1);
            panel2.Controls.Add(LoginTag);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(314, 250);
            panel2.TabIndex = 4;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(26, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(235, 178);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // LoginTag
            // 
            LoginTag.AutoSize = true;
            LoginTag.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LoginTag.ForeColor = Color.White;
            LoginTag.Location = new Point(42, 182);
            LoginTag.Name = "LoginTag";
            LoginTag.Size = new Size(182, 34);
            LoginTag.TabIndex = 1;
            LoginTag.Text = "Sales Admin";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(42, 216);
            label1.Name = "label1";
            label1.Size = new Size(209, 28);
            label1.TabIndex = 2;
            label1.Text = "Sales@company.com";
            // 
            // txtFullName
            // 
            txtFullName.Font = new Font("Nirmala UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtFullName.Location = new Point(531, 55);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(354, 31);
            txtFullName.TabIndex = 33;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(407, 55);
            label2.Name = "label2";
            label2.Size = new Size(115, 28);
            label2.TabIndex = 32;
            label2.Text = "Staff Name";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Nirmala UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUsername.Location = new Point(1050, 55);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(354, 31);
            txtUsername.TabIndex = 35;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(926, 55);
            label3.Name = "label3";
            label3.Size = new Size(80, 28);
            label3.TabIndex = 34;
            label3.Text = "Staff ID";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Nirmala UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPassword.Location = new Point(531, 182);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(354, 31);
            txtPassword.TabIndex = 37;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(407, 182);
            label4.Name = "label4";
            label4.Size = new Size(101, 28);
            label4.TabIndex = 36;
            label4.Text = "Password";
            // 
            // cmbRole
            // 
            cmbRole.FormattingEnabled = true;
            cmbRole.Items.AddRange(new object[] { "Admin ", "Sales " });
            cmbRole.Location = new Point(1050, 182);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(320, 28);
            cmbRole.TabIndex = 38;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.White;
            label5.Location = new Point(926, 182);
            label5.Name = "label5";
            label5.Size = new Size(94, 28);
            label5.TabIndex = 39;
            label5.Text = "User Roll";
            // 
            // BtnSave
            // 
            BtnSave.BackColor = Color.FromArgb(0, 192, 0);
            BtnSave.FlatAppearance.BorderSize = 0;
            BtnSave.FlatStyle = FlatStyle.Flat;
            BtnSave.Font = new Font("Nirmala UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnSave.ForeColor = Color.FromArgb(192, 255, 255);
            BtnSave.ImageAlign = ContentAlignment.MiddleLeft;
            BtnSave.Location = new Point(869, 369);
            BtnSave.Name = "BtnSave";
            BtnSave.Size = new Size(167, 50);
            BtnSave.TabIndex = 40;
            BtnSave.Text = "Save";
            BtnSave.UseVisualStyleBackColor = false;
            BtnSave.Click += BtnSave_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(827, 522);
            label6.Name = "label6";
            label6.Size = new Size(101, 28);
            label6.TabIndex = 41;
            label6.Text = "Password";
            // 
            // Register
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(46, 51, 76);
            ClientSize = new Size(1424, 785);
            Controls.Add(label6);
            Controls.Add(BtnSave);
            Controls.Add(label5);
            Controls.Add(cmbRole);
            Controls.Add(txtPassword);
            Controls.Add(label4);
            Controls.Add(txtUsername);
            Controls.Add(label3);
            Controls.Add(txtFullName);
            Controls.Add(label2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Register";
            Text = "Register";
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private PictureBox pictureBox1;
        private Label LoginTag;
        private Label label1;
        private TextBox txtFullName;
        private Label label2;
        private TextBox txtUsername;
        private Label label3;
        private TextBox txtPassword;
        private Label label4;
        private ComboBox cmbRole;
        private Label label5;
        private Button BtnSave;
        private Label label6;
    }
}