namespace NovaSystem
{
    partial class Inventory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inventory));
            panel1 = new Panel();
            btnLogout = new Button();
            BtnSales = new Button();
            BtnInventory = new Button();
            BtnShoewear = new Button();
            panel2 = new Panel();
            lblLogin = new Label();
            pictureBox1 = new PictureBox();
            LoginTag = new Label();
            label1 = new Label();
            btnHome = new Button();
            panel6 = new Panel();
            lblCosmeticInventory = new Label();
            lblCosmeticCounter = new Label();
            pictureBox5 = new PictureBox();
            label13 = new Label();
            label14 = new Label();
            panel7 = new Panel();
            lblShoeCounter = new Label();
            pictureBox6 = new PictureBox();
            label16 = new Label();
            label17 = new Label();
            panel4 = new Panel();
            lblShoeSales = new Label();
            pictureBox3 = new PictureBox();
            label7 = new Label();
            label8 = new Label();
            panel3 = new Panel();
            lblCosmeticSales = new Label();
            pictureBox2 = new PictureBox();
            label4 = new Label();
            label5 = new Label();
            ComboCategory = new ComboBox();
            label2 = new Label();
            ProductTotal = new Label();
            BtnCheckinventory = new Button();
            label3 = new Label();
            label6 = new Label();
            lblItemsSold = new Label();
            dgvInventory = new DataGridView();
            lblIndicator = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvInventory).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.MediumBlue;
            panel1.Controls.Add(btnLogout);
            panel1.Controls.Add(BtnSales);
            panel1.Controls.Add(BtnInventory);
            panel1.Controls.Add(BtnShoewear);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(btnHome);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(280, 769);
            panel1.TabIndex = 3;
            // 
            // btnLogout
            // 
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Nirmala UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLogout.ForeColor = Color.FromArgb(192, 255, 255);
            btnLogout.ImageAlign = ContentAlignment.MiddleLeft;
            btnLogout.Location = new Point(-3, 707);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(314, 50);
            btnLogout.TabIndex = 10;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // BtnSales
            // 
            BtnSales.FlatAppearance.BorderSize = 0;
            BtnSales.FlatStyle = FlatStyle.Flat;
            BtnSales.Font = new Font("Nirmala UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnSales.ForeColor = Color.FromArgb(192, 255, 255);
            BtnSales.Image = (Image)resources.GetObject("BtnSales.Image");
            BtnSales.ImageAlign = ContentAlignment.MiddleLeft;
            BtnSales.Location = new Point(-3, 478);
            BtnSales.Name = "BtnSales";
            BtnSales.Size = new Size(314, 50);
            BtnSales.TabIndex = 8;
            BtnSales.Text = "Sales";
            BtnSales.UseVisualStyleBackColor = true;
            BtnSales.Click += BtnSales_Click;
            // 
            // BtnInventory
            // 
            BtnInventory.FlatAppearance.BorderSize = 0;
            BtnInventory.FlatStyle = FlatStyle.Flat;
            BtnInventory.Font = new Font("Nirmala UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnInventory.ForeColor = Color.FromArgb(192, 255, 255);
            BtnInventory.Image = (Image)resources.GetObject("BtnInventory.Image");
            BtnInventory.ImageAlign = ContentAlignment.MiddleLeft;
            BtnInventory.Location = new Point(0, 413);
            BtnInventory.Name = "BtnInventory";
            BtnInventory.Size = new Size(314, 50);
            BtnInventory.TabIndex = 7;
            BtnInventory.Text = "Inventory";
            BtnInventory.UseVisualStyleBackColor = true;
            BtnInventory.Click += BtnInventory_Click;
            // 
            // BtnShoewear
            // 
            BtnShoewear.FlatAppearance.BorderSize = 0;
            BtnShoewear.FlatStyle = FlatStyle.Flat;
            BtnShoewear.Font = new Font("Nirmala UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnShoewear.ForeColor = Color.FromArgb(192, 255, 255);
            BtnShoewear.Image = (Image)resources.GetObject("BtnShoewear.Image");
            BtnShoewear.ImageAlign = ContentAlignment.MiddleLeft;
            BtnShoewear.Location = new Point(3, 348);
            BtnShoewear.Name = "BtnShoewear";
            BtnShoewear.Size = new Size(314, 50);
            BtnShoewear.TabIndex = 6;
            BtnShoewear.Text = "Shoewear";
            BtnShoewear.UseVisualStyleBackColor = true;
            BtnShoewear.Click += BtnShoewear_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(lblLogin);
            panel2.Controls.Add(pictureBox1);
            panel2.Controls.Add(LoginTag);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(280, 250);
            panel2.TabIndex = 4;
            // 
            // lblLogin
            // 
            lblLogin.AutoSize = true;
            lblLogin.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLogin.ForeColor = Color.White;
            lblLogin.Location = new Point(42, 189);
            lblLogin.Name = "lblLogin";
            lblLogin.Size = new Size(101, 23);
            lblLogin.TabIndex = 49;
            lblLogin.Text = "Order By ";
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
            LoginTag.Size = new Size(0, 34);
            LoginTag.TabIndex = 1;
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
            // panel6
            // 
            panel6.BackColor = Color.FromArgb(46, 51, 83);
            panel6.Controls.Add(lblCosmeticInventory);
            panel6.Controls.Add(lblCosmeticCounter);
            panel6.Controls.Add(pictureBox5);
            panel6.Controls.Add(label13);
            panel6.Controls.Add(label14);
            panel6.Location = new Point(304, 36);
            panel6.Name = "panel6";
            panel6.Size = new Size(378, 145);
            panel6.TabIndex = 10;
            // 
            // lblCosmeticInventory
            // 
            lblCosmeticInventory.AutoSize = true;
            lblCosmeticInventory.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCosmeticInventory.ForeColor = Color.Yellow;
            lblCosmeticInventory.Location = new Point(186, 96);
            lblCosmeticInventory.Name = "lblCosmeticInventory";
            lblCosmeticInventory.Size = new Size(63, 34);
            lblCosmeticInventory.TabIndex = 7;
            lblCosmeticInventory.Text = "300";
            // 
            // lblCosmeticCounter
            // 
            lblCosmeticCounter.AutoSize = true;
            lblCosmeticCounter.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCosmeticCounter.ForeColor = Color.Lavender;
            lblCosmeticCounter.Location = new Point(99, 100);
            lblCosmeticCounter.Name = "lblCosmeticCounter";
            lblCosmeticCounter.Size = new Size(0, 34);
            lblCosmeticCounter.TabIndex = 5;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(294, 8);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(75, 83);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 6;
            pictureBox5.TabStop = false;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label13.ForeColor = Color.White;
            label13.Location = new Point(16, 100);
            label13.Name = "label13";
            label13.Size = new Size(152, 28);
            label13.TabIndex = 3;
            label13.Text = "Stock currently";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.ForeColor = Color.White;
            label14.Location = new Point(6, 9);
            label14.Name = "label14";
            label14.Size = new Size(291, 34);
            label14.TabIndex = 5;
            label14.Text = "Cosmetics Inventory";
            // 
            // panel7
            // 
            panel7.BackColor = Color.FromArgb(46, 51, 89);
            panel7.Controls.Add(lblShoeCounter);
            panel7.Controls.Add(pictureBox6);
            panel7.Controls.Add(label16);
            panel7.Controls.Add(label17);
            panel7.Location = new Point(702, 36);
            panel7.Name = "panel7";
            panel7.Size = new Size(343, 145);
            panel7.TabIndex = 11;
            // 
            // lblShoeCounter
            // 
            lblShoeCounter.AutoSize = true;
            lblShoeCounter.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblShoeCounter.ForeColor = Color.Yellow;
            lblShoeCounter.Location = new Point(174, 94);
            lblShoeCounter.Name = "lblShoeCounter";
            lblShoeCounter.Size = new Size(63, 34);
            lblShoeCounter.TabIndex = 5;
            lblShoeCounter.Text = "300";
            // 
            // pictureBox6
            // 
            pictureBox6.Image = (Image)resources.GetObject("pictureBox6.Image");
            pictureBox6.Location = new Point(252, 9);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(80, 83);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 6;
            pictureBox6.TabStop = false;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label16.ForeColor = Color.White;
            label16.Location = new Point(16, 98);
            label16.Name = "label16";
            label16.Size = new Size(152, 28);
            label16.TabIndex = 3;
            label16.Text = "Stock currently";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label17.ForeColor = Color.White;
            label17.Location = new Point(16, 9);
            label17.Name = "label17";
            label17.Size = new Size(230, 34);
            label17.TabIndex = 5;
            label17.Text = "Shoes Inventory";
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(46, 51, 89);
            panel4.Controls.Add(lblShoeSales);
            panel4.Controls.Add(pictureBox3);
            panel4.Controls.Add(label7);
            panel4.Controls.Add(label8);
            panel4.Location = new Point(1075, 36);
            panel4.Name = "panel4";
            panel4.Size = new Size(351, 145);
            panel4.TabIndex = 12;
            // 
            // lblShoeSales
            // 
            lblShoeSales.AutoSize = true;
            lblShoeSales.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblShoeSales.ForeColor = Color.Fuchsia;
            lblShoeSales.Location = new Point(12, 57);
            lblShoeSales.Name = "lblShoeSales";
            lblShoeSales.Size = new Size(176, 34);
            lblShoeSales.TabIndex = 5;
            lblShoeSales.Text = "KSh 234,000";
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(242, 52);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(91, 76);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 6;
            pictureBox3.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.White;
            label7.Location = new Point(12, 104);
            label7.Name = "label7";
            label7.Size = new Size(201, 28);
            label7.TabIndex = 3;
            label7.Text = "Projected shoe sales";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.White;
            label8.Location = new Point(12, 9);
            label8.Name = "label8";
            label8.Size = new Size(299, 34);
            label8.TabIndex = 5;
            label8.Text = "Projected Shoe Sales";
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(46, 51, 89);
            panel3.Controls.Add(lblCosmeticSales);
            panel3.Controls.Add(pictureBox2);
            panel3.Controls.Add(label4);
            panel3.Controls.Add(label5);
            panel3.Location = new Point(1442, 36);
            panel3.Name = "panel3";
            panel3.Size = new Size(346, 145);
            panel3.TabIndex = 8;
            // 
            // lblCosmeticSales
            // 
            lblCosmeticSales.AutoSize = true;
            lblCosmeticSales.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCosmeticSales.ForeColor = Color.Fuchsia;
            lblCosmeticSales.Location = new Point(19, 58);
            lblCosmeticSales.Name = "lblCosmeticSales";
            lblCosmeticSales.Size = new Size(176, 34);
            lblCosmeticSales.TabIndex = 5;
            lblCosmeticSales.Text = "KSh 234,000";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(246, 13);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(96, 88);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(19, 104);
            label4.Name = "label4";
            label4.Size = new Size(244, 28);
            label4.TabIndex = 3;
            label4.Text = "Projected Cosmetic Sales";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.White;
            label5.Location = new Point(3, 9);
            label5.Name = "label5";
            label5.Size = new Size(234, 27);
            label5.TabIndex = 5;
            label5.Text = "Projected Cosmetic";
            // 
            // ComboCategory
            // 
            ComboCategory.Font = new Font("Nirmala UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ComboCategory.FormattingEnabled = true;
            ComboCategory.Items.AddRange(new object[] { "Coconut", "collagen", "Turmeric", "Vitamin  c", "Salicyclic", "hyaluronic", "Cantu", "Niacinamide", "Salt scrub", "Exfoliating ", "shower gel" });
            ComboCategory.Location = new Point(464, 216);
            ComboCategory.Name = "ComboCategory";
            ComboCategory.Size = new Size(231, 36);
            ComboCategory.TabIndex = 40;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(304, 216);
            label2.Name = "label2";
            label2.Size = new Size(142, 34);
            label2.TabIndex = 39;
            label2.Text = "Category";
            // 
            // ProductTotal
            // 
            ProductTotal.AutoSize = true;
            ProductTotal.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ProductTotal.ForeColor = Color.White;
            ProductTotal.Location = new Point(589, 321);
            ProductTotal.Name = "ProductTotal";
            ProductTotal.Size = new Size(106, 34);
            ProductTotal.TabIndex = 43;
            ProductTotal.Text = " Name";
            // 
            // BtnCheckinventory
            // 
            BtnCheckinventory.BackColor = Color.FromArgb(46, 51, 76);
            BtnCheckinventory.FlatAppearance.BorderSize = 0;
            BtnCheckinventory.FlatStyle = FlatStyle.Flat;
            BtnCheckinventory.Font = new Font("Nirmala UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnCheckinventory.ForeColor = Color.FromArgb(192, 255, 255);
            BtnCheckinventory.ImageAlign = ContentAlignment.MiddleLeft;
            BtnCheckinventory.Location = new Point(954, 204);
            BtnCheckinventory.Name = "BtnCheckinventory";
            BtnCheckinventory.Size = new Size(301, 50);
            BtnCheckinventory.TabIndex = 44;
            BtnCheckinventory.Text = "Check inventory ";
            BtnCheckinventory.UseVisualStyleBackColor = false;
            BtnCheckinventory.Click += BtnCheckinventory_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Yellow;
            label3.Location = new Point(310, 321);
            label3.Name = "label3";
            label3.Size = new Size(260, 34);
            label3.TabIndex = 8;
            label3.Text = "Available in Stock";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Aqua;
            label6.Location = new Point(766, 321);
            label6.Name = "label6";
            label6.Size = new Size(153, 34);
            label6.TabIndex = 45;
            label6.Text = "Items Sold";
            // 
            // lblItemsSold
            // 
            lblItemsSold.AutoSize = true;
            lblItemsSold.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblItemsSold.ForeColor = Color.White;
            lblItemsSold.Location = new Point(1002, 321);
            lblItemsSold.Name = "lblItemsSold";
            lblItemsSold.Size = new Size(98, 34);
            lblItemsSold.TabIndex = 46;
            lblItemsSold.Text = "Name";
            // 
            // dgvInventory
            // 
            dgvInventory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInventory.Location = new Point(331, 558);
            dgvInventory.Name = "dgvInventory";
            dgvInventory.RowHeadersWidth = 51;
            dgvInventory.Size = new Size(1453, 188);
            dgvInventory.TabIndex = 47;
            // 
            // lblIndicator
            // 
            lblIndicator.AutoSize = true;
            lblIndicator.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblIndicator.ForeColor = Color.FromArgb(128, 255, 128);
            lblIndicator.Location = new Point(1391, 321);
            lblIndicator.Name = "lblIndicator";
            lblIndicator.Size = new Size(63, 34);
            lblIndicator.TabIndex = 48;
            lblIndicator.Text = "300";
            // 
            // Inventory
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(46, 51, 76);
            ClientSize = new Size(1800, 769);
            Controls.Add(lblIndicator);
            Controls.Add(dgvInventory);
            Controls.Add(label6);
            Controls.Add(lblItemsSold);
            Controls.Add(label3);
            Controls.Add(BtnCheckinventory);
            Controls.Add(ProductTotal);
            Controls.Add(panel3);
            Controls.Add(panel4);
            Controls.Add(ComboCategory);
            Controls.Add(label2);
            Controls.Add(panel7);
            Controls.Add(panel6);
            Controls.Add(panel1);
            Name = "Inventory";
            Text = "Inventory";
            Load += Inventory_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvInventory).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button BtnSales;
        private Button BtnInventory;
        private Button BtnShoewear;
        private Panel panel2;
        private PictureBox pictureBox1;
        private Label LoginTag;
        private Label label1;
        private Button btnHome;
        private Button btnLogout;
        private Label lblLogin;
        private Panel panel6;
        private Label lblCosmeticCounter;
        private PictureBox pictureBox5;
        private Label label13;
        private Label label14;
        private Label lblCosmeticInventory;
        private Panel panel7;
        private Label lblShoeCounter;
        private PictureBox pictureBox6;
        private Label label16;
        private Label label17;
        private Panel panel4;
        private Label lblShoeSales;
        private PictureBox pictureBox3;
        private Label label7;
        private Label label8;
        private Panel panel3;
        private Label lblCosmeticSales;
        private PictureBox pictureBox2;
        private Label label4;
        private Label label5;
        private ComboBox ComboCategory;
        private Label label2;
        private Label ProductTotal;
        private Button BtnCheckinventory;
        private Label label3;
        private Label label6;
        private Label lblItemsSold;
        private DataGridView dgvInventory;
        private Label lblIndicator;
    }
}