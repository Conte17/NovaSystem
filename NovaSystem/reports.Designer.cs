namespace NovaSystem
{
    partial class reports
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(reports));
            panel1 = new Panel();
            btnLogout = new Button();
            btnSales = new Button();
            BtnInventory = new Button();
            button1 = new Button();
            BtnCosmetics = new Button();
            panel2 = new Panel();
            pictureBox1 = new PictureBox();
            LoginTag = new Label();
            label1 = new Label();
            btnHome = new Button();
            dgvReport = new DataGridView();
            label14 = new Label();
            panel3 = new Panel();
            pictureBox5 = new PictureBox();
            label3 = new Label();
            lbl24earnings = new Label();
            label4 = new Label();
            label2 = new Label();
            lblCosmeticsReport = new Label();
            BtnPrintReport = new Button();
            txtBatchNo = new TextBox();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            cmbStatus = new ComboBox();
            btnFilter = new Button();
            lblStaffsales = new Label();
            txtStaffname = new TextBox();
            label9 = new Label();
            btnStaffFilter = new Button();
            label8 = new Label();
            ComboCategory = new ComboBox();
            cmbBoxProdName = new ComboBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvReport).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.MediumBlue;
            panel1.Controls.Add(btnLogout);
            panel1.Controls.Add(btnSales);
            panel1.Controls.Add(BtnInventory);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(BtnCosmetics);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(btnHome);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(314, 950);
            panel1.TabIndex = 6;
            panel1.Paint += panel1_Paint;
            // 
            // btnLogout
            // 
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Nirmala UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLogout.ForeColor = Color.FromArgb(192, 255, 255);
            btnLogout.ImageAlign = ContentAlignment.MiddleLeft;
            btnLogout.Location = new Point(0, 888);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(314, 50);
            btnLogout.TabIndex = 9;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // btnSales
            // 
            btnSales.FlatAppearance.BorderSize = 0;
            btnSales.FlatStyle = FlatStyle.Flat;
            btnSales.Font = new Font("Nirmala UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSales.ForeColor = Color.FromArgb(192, 255, 255);
            btnSales.Image = (Image)resources.GetObject("btnSales.Image");
            btnSales.ImageAlign = ContentAlignment.MiddleLeft;
            btnSales.Location = new Point(-3, 595);
            btnSales.Name = "btnSales";
            btnSales.Size = new Size(314, 50);
            btnSales.TabIndex = 8;
            btnSales.Text = "Sales";
            btnSales.UseVisualStyleBackColor = true;
            btnSales.Click += btnSales_Click;
            // 
            // BtnInventory
            // 
            BtnInventory.FlatAppearance.BorderSize = 0;
            BtnInventory.FlatStyle = FlatStyle.Flat;
            BtnInventory.Font = new Font("Nirmala UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnInventory.ForeColor = Color.FromArgb(192, 255, 255);
            BtnInventory.Image = (Image)resources.GetObject("BtnInventory.Image");
            BtnInventory.ImageAlign = ContentAlignment.MiddleLeft;
            BtnInventory.Location = new Point(0, 511);
            BtnInventory.Name = "BtnInventory";
            BtnInventory.Size = new Size(314, 50);
            BtnInventory.TabIndex = 7;
            BtnInventory.Text = "Inventory";
            BtnInventory.UseVisualStyleBackColor = true;
            BtnInventory.Click += BtnInventory_Click;
            // 
            // button1
            // 
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Nirmala UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.FromArgb(192, 255, 255);
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.Location = new Point(0, 432);
            button1.Name = "button1";
            button1.Size = new Size(314, 50);
            button1.TabIndex = 6;
            button1.Text = "Shoewear";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // BtnCosmetics
            // 
            BtnCosmetics.FlatAppearance.BorderSize = 0;
            BtnCosmetics.FlatStyle = FlatStyle.Flat;
            BtnCosmetics.Font = new Font("Nirmala UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnCosmetics.ForeColor = Color.FromArgb(192, 255, 255);
            BtnCosmetics.Image = (Image)resources.GetObject("BtnCosmetics.Image");
            BtnCosmetics.ImageAlign = ContentAlignment.MiddleLeft;
            BtnCosmetics.Location = new Point(3, 353);
            BtnCosmetics.Name = "BtnCosmetics";
            BtnCosmetics.Size = new Size(314, 50);
            BtnCosmetics.TabIndex = 5;
            BtnCosmetics.Text = "Cosmetics ";
            BtnCosmetics.UseVisualStyleBackColor = true;
            BtnCosmetics.Click += BtnCosmetics_Click;
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
            // btnHome
            // 
            btnHome.FlatAppearance.BorderSize = 0;
            btnHome.FlatStyle = FlatStyle.Flat;
            btnHome.Font = new Font("Nirmala UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnHome.ForeColor = Color.FromArgb(192, 255, 255);
            btnHome.Image = (Image)resources.GetObject("btnHome.Image");
            btnHome.ImageAlign = ContentAlignment.MiddleLeft;
            btnHome.Location = new Point(0, 275);
            btnHome.Name = "btnHome";
            btnHome.Size = new Size(314, 50);
            btnHome.TabIndex = 3;
            btnHome.Text = "Home";
            btnHome.UseVisualStyleBackColor = true;
            btnHome.Click += btnHome_Click;
            // 
            // dgvReport
            // 
            dgvReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReport.Location = new Point(340, 739);
            dgvReport.Name = "dgvReport";
            dgvReport.RowHeadersWidth = 51;
            dgvReport.Size = new Size(1168, 199);
            dgvReport.TabIndex = 7;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.ForeColor = Color.White;
            label14.Location = new Point(3, 4);
            label14.Name = "label14";
            label14.Size = new Size(343, 34);
            label14.TabIndex = 5;
            label14.Text = "Inventory Status Reports ";
            label14.Click += label14_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(46, 51, 80);
            panel3.Controls.Add(pictureBox5);
            panel3.Controls.Add(label3);
            panel3.Controls.Add(lbl24earnings);
            panel3.Controls.Add(label4);
            panel3.Controls.Add(label14);
            panel3.Location = new Point(340, 12);
            panel3.Name = "panel3";
            panel3.Size = new Size(359, 245);
            panel3.TabIndex = 8;
            panel3.Paint += panel3_Paint;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(216, 47);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(125, 118);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 10;
            pictureBox5.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(12, 200);
            label3.Name = "label3";
            label3.Size = new Size(329, 28);
            label3.TabIndex = 9;
            label3.Text = "A detailed list of all items available";
            // 
            // lbl24earnings
            // 
            lbl24earnings.AutoSize = true;
            lbl24earnings.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl24earnings.ForeColor = Color.FromArgb(255, 128, 0);
            lbl24earnings.Location = new Point(12, 82);
            lbl24earnings.Name = "lbl24earnings";
            lbl24earnings.Size = new Size(176, 34);
            lbl24earnings.TabIndex = 5;
            lbl24earnings.Text = "KSh 234,000";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(12, 163);
            label4.Name = "label4";
            label4.Size = new Size(219, 28);
            label4.TabIndex = 8;
            label4.Text = "Available Stock Report";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(338, 583);
            label2.Name = "label2";
            label2.Size = new Size(341, 34);
            label2.TabIndex = 11;
            label2.Text = "Cosmetics Batch reports";
            // 
            // lblCosmeticsReport
            // 
            lblCosmeticsReport.AutoSize = true;
            lblCosmeticsReport.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCosmeticsReport.ForeColor = Color.FromArgb(255, 128, 0);
            lblCosmeticsReport.Location = new Point(702, 583);
            lblCosmeticsReport.Name = "lblCosmeticsReport";
            lblCosmeticsReport.Size = new Size(176, 34);
            lblCosmeticsReport.TabIndex = 12;
            lblCosmeticsReport.Text = "KSh 234,000";
            lblCosmeticsReport.Click += lblCosmeticsReport_Click;
            // 
            // BtnPrintReport
            // 
            BtnPrintReport.BackColor = Color.FromArgb(46, 51, 76);
            BtnPrintReport.FlatAppearance.BorderSize = 0;
            BtnPrintReport.FlatStyle = FlatStyle.Flat;
            BtnPrintReport.Font = new Font("Nirmala UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnPrintReport.ForeColor = Color.FromArgb(192, 255, 255);
            BtnPrintReport.ImageAlign = ContentAlignment.MiddleLeft;
            BtnPrintReport.Location = new Point(1514, 751);
            BtnPrintReport.Name = "BtnPrintReport";
            BtnPrintReport.Size = new Size(193, 50);
            BtnPrintReport.TabIndex = 23;
            BtnPrintReport.Text = "Print Report";
            BtnPrintReport.UseVisualStyleBackColor = false;
            BtnPrintReport.Click += BtnCheckout_Click;
            // 
            // txtBatchNo
            // 
            txtBatchNo.Location = new Point(462, 646);
            txtBatchNo.Name = "txtBatchNo";
            txtBatchNo.Size = new Size(138, 27);
            txtBatchNo.TabIndex = 24;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.White;
            label5.Location = new Point(340, 643);
            label5.Name = "label5";
            label5.Size = new Size(104, 28);
            label5.TabIndex = 25;
            label5.Text = "Batch No.";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(606, 643);
            label6.Name = "label6";
            label6.Size = new Size(194, 28);
            label6.TabIndex = 27;
            label6.Text = "Cosmetics Category";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.White;
            label7.Location = new Point(1423, 642);
            label7.Name = "label7";
            label7.Size = new Size(71, 28);
            label7.TabIndex = 28;
            label7.Text = "Status";
            // 
            // cmbStatus
            // 
            cmbStatus.Font = new Font("Nirmala UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Items.AddRange(new object[] { "Active", "Completed" });
            cmbStatus.Location = new Point(1514, 636);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(169, 36);
            cmbStatus.TabIndex = 29;
            // 
            // btnFilter
            // 
            btnFilter.BackColor = Color.FromArgb(46, 51, 76);
            btnFilter.FlatAppearance.BorderSize = 0;
            btnFilter.FlatStyle = FlatStyle.Flat;
            btnFilter.Font = new Font("Nirmala UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnFilter.ForeColor = Color.FromArgb(192, 255, 255);
            btnFilter.ImageAlign = ContentAlignment.MiddleRight;
            btnFilter.Location = new Point(1694, 627);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(130, 50);
            btnFilter.TabIndex = 30;
            btnFilter.Text = "filter";
            btnFilter.UseVisualStyleBackColor = false;
            btnFilter.Click += btnFilter_Click;
            // 
            // lblStaffsales
            // 
            lblStaffsales.AutoSize = true;
            lblStaffsales.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStaffsales.ForeColor = Color.White;
            lblStaffsales.Location = new Point(343, 473);
            lblStaffsales.Name = "lblStaffsales";
            lblStaffsales.Size = new Size(257, 34);
            lblStaffsales.TabIndex = 31;
            lblStaffsales.Text = "Staff Sales Reports";
            lblStaffsales.Click += lblStaffsales_Click_1;
            // 
            // txtStaffname
            // 
            txtStaffname.Location = new Point(771, 480);
            txtStaffname.Name = "txtStaffname";
            txtStaffname.Size = new Size(255, 27);
            txtStaffname.TabIndex = 32;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.White;
            label9.Location = new Point(623, 479);
            label9.Name = "label9";
            label9.Size = new Size(115, 28);
            label9.TabIndex = 33;
            label9.Text = "Staff Name";
            // 
            // btnStaffFilter
            // 
            btnStaffFilter.BackColor = Color.FromArgb(46, 51, 76);
            btnStaffFilter.FlatAppearance.BorderSize = 0;
            btnStaffFilter.FlatStyle = FlatStyle.Flat;
            btnStaffFilter.Font = new Font("Nirmala UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnStaffFilter.ForeColor = Color.FromArgb(192, 255, 255);
            btnStaffFilter.ImageAlign = ContentAlignment.MiddleRight;
            btnStaffFilter.Location = new Point(1162, 461);
            btnStaffFilter.Name = "btnStaffFilter";
            btnStaffFilter.Size = new Size(130, 50);
            btnStaffFilter.TabIndex = 34;
            btnStaffFilter.Text = "filter";
            btnStaffFilter.UseVisualStyleBackColor = false;
            btnStaffFilter.Click += btnStaffFilter_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.White;
            label8.Location = new Point(1042, 644);
            label8.Name = "label8";
            label8.Size = new Size(146, 28);
            label8.TabIndex = 39;
            label8.Text = "Product Name";
            // 
            // ComboCategory
            // 
            ComboCategory.Font = new Font("Nirmala UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ComboCategory.FormattingEnabled = true;
            ComboCategory.Items.AddRange(new object[] { "Coconut", "collagen", "Turmeric", "Vitamin  c", "Salicylic Acid", "Cantu", "Niacinamide", "shower gel" });
            ComboCategory.Location = new Point(823, 639);
            ComboCategory.Name = "ComboCategory";
            ComboCategory.Size = new Size(212, 36);
            ComboCategory.TabIndex = 42;
            // 
            // cmbBoxProdName
            // 
            cmbBoxProdName.Font = new Font("Nirmala UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbBoxProdName.FormattingEnabled = true;
            cmbBoxProdName.Items.AddRange(new object[] { "Body lotion", "Emollient  oil ", "Hair mask ", "Lipbalm ", "Hand cream ", "Shower gel ", "Shampoo  ", "Foot cream ", "Sunscreen spray ", "Face cream ", "Hair mask ", "Hand cream", "Eye cream", "Firming  gel", "Eye mask", "Face mask ", "Face toner ", "Face serum ", "Face lotion", "Face cleanser ", "Cleaning  moose  ", "Body lotion ", "Turmeric soap ", "Cleanser", "Toner ", "Body scrub ", "Face serum", "Face lotion ", "Eye cream", "Face cream ", "Face serum ", "Face mask ", "Face lotion ", "Cleanser ", "Eye cream", "Curling cream ", "Custard and shine gel ", "Strawberry lip scrub ", "Orange lip scrub ", "Honey peach lip scrub ", "Coconut lip balm", "Repair serum ", "Coffee scrub ", "Blueberry scrub ", "Mango scrub ", "Sunscreen cream ", "Sunscreen spray ", "Black mask ", "Peel off mask ", "Hair mask ", "Body lotion", "Kiwi  shower gel ", "Blueberry shower gel", "Men cleanser", "Turmeric scrub", "24k gold" });
            cmbBoxProdName.Location = new Point(1194, 637);
            cmbBoxProdName.Name = "cmbBoxProdName";
            cmbBoxProdName.Size = new Size(208, 36);
            cmbBoxProdName.TabIndex = 43;
            // 
            // reports
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(46, 51, 76);
            ClientSize = new Size(1836, 950);
            Controls.Add(cmbBoxProdName);
            Controls.Add(ComboCategory);
            Controls.Add(label8);
            Controls.Add(btnStaffFilter);
            Controls.Add(label9);
            Controls.Add(txtStaffname);
            Controls.Add(lblStaffsales);
            Controls.Add(btnFilter);
            Controls.Add(cmbStatus);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(txtBatchNo);
            Controls.Add(BtnPrintReport);
            Controls.Add(lblCosmeticsReport);
            Controls.Add(label2);
            Controls.Add(panel3);
            Controls.Add(dgvReport);
            Controls.Add(panel1);
            Name = "reports";
            Text = "reports";
            Load += reports_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvReport).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button btnLogout;
        private Button btnSales;
        private Button BtnInventory;
        private Button button1;
        private Button BtnCosmetics;
        private Panel panel2;
        private PictureBox pictureBox1;
        private Label LoginTag;
        private Label label1;
        private Button btnHome;
        private DataGridView dgvReport;
        private Label label14;
        private Panel panel3;
        private Label label3;
        private Label lbl24earnings;
        private Label label4;
        private PictureBox pictureBox5;
        private Label label2;
        private Label lblCosmeticsReport;
        private Button BtnPrintReport;
        private TextBox txtBatchNo;
        private Label label5;
        private Label label6;
        private Label label7;
        private ComboBox cmbStatus;
        private Button btnFilter;
        private Label lblStaffsales;
        private TextBox txtStaffname;
        private Label label9;
        private Button btnStaffFilter;
        private Label label8;
        private ComboBox ComboCategory;
        private ComboBox cmbBoxProdName;
    }
}