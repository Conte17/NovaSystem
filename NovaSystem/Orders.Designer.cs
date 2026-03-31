namespace NovaSystem
{
    partial class Orders
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Orders));
            panel1 = new Panel();
            BtnSales = new Button();
            BtnInventory = new Button();
            BtnShoewear = new Button();
            panel2 = new Panel();
            pictureBox1 = new PictureBox();
            LoginTag = new Label();
            label1 = new Label();
            btnHome = new Button();
            label10 = new Label();
            cmbBoxProdName = new ComboBox();
            ComboCategory = new ComboBox();
            label4 = new Label();
            dataGridView1 = new DataGridView();
            txtQuantity = new TextBox();
            label9 = new Label();
            label2 = new Label();
            cmbUser = new ComboBox();
            txtAmount = new TextBox();
            label3 = new Label();
            panel6 = new Panel();
            label6 = new Label();
            lblStockStatus = new Label();
            lbltotal = new Label();
            label5 = new Label();
            BtnCheckout = new Button();
            btnClearcart = new Button();
            BtnRemove = new Button();
            dgvCart = new DataGridView();
            lblTotalAmount = new Label();
            label18 = new Label();
            BtnSave = new Button();
            txtStaffname = new TextBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCart).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.MediumBlue;
            panel1.Controls.Add(BtnSales);
            panel1.Controls.Add(BtnInventory);
            panel1.Controls.Add(BtnShoewear);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(btnHome);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(314, 1055);
            panel1.TabIndex = 2;
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
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.White;
            label10.Location = new Point(777, 62);
            label10.Name = "label10";
            label10.Size = new Size(210, 34);
            label10.TabIndex = 38;
            label10.Text = "Product Name";
            // 
            // cmbBoxProdName
            // 
            cmbBoxProdName.Font = new Font("Nirmala UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbBoxProdName.FormattingEnabled = true;
            cmbBoxProdName.Items.AddRange(new object[] { "Body lotion", "Emollient  oil ", "Hair mask ", "Lipbalm ", "Hand cream ", "Shower gel ", "Shampoo  ", "Foot cream ", "Sunscreen spray ", "Face cream ", "Hair mask ", "Hand cream", "Eye cream", "Firming  gel", "Eye mask", "Face mask ", "Face toner ", "Face serum ", "Face lotion", "Face cleanser ", "Cleaning  moose  ", "Body lotion ", "Turmeric soap ", "Cleanser", "Toner ", "Body scrub ", "Face serum", "Face lotion ", "Eye cream", "Face cream ", "Face serum ", "Face mask ", "Face lotion ", "Cleanser ", "Eye cream", "Curling cream ", "Custard and shine gel ", "Strawberry lip scrub ", "Orange lip scrub ", "Honey peach lip scrub ", "Coconut lip balm", "Repair serum ", "Coffee scrub ", "Blueberry scrub ", "Mango scrub ", "Sunscreen cream ", "Sunscreen spray ", "Black mask ", "Peel off mask ", "Hair mask ", "Body lotion", "Kiwi  shower gel ", "Blueberry shower gel", "Men cleanser", "Turmeric scrub", "24k gold" });
            cmbBoxProdName.Location = new Point(993, 62);
            cmbBoxProdName.Name = "cmbBoxProdName";
            cmbBoxProdName.Size = new Size(288, 36);
            cmbBoxProdName.TabIndex = 37;
            // 
            // ComboCategory
            // 
            ComboCategory.Font = new Font("Nirmala UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ComboCategory.FormattingEnabled = true;
            ComboCategory.Items.AddRange(new object[] { "Coconut", "collagen", "Turmeric", "Vitamin  c", "Salicylic Acid", "Cantu", "Niacinamide", "shower gel" });
            ComboCategory.Location = new Point(513, 62);
            ComboCategory.Name = "ComboCategory";
            ComboCategory.Size = new Size(231, 36);
            ComboCategory.TabIndex = 36;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(353, 62);
            label4.Name = "label4";
            label4.Size = new Size(142, 34);
            label4.TabIndex = 35;
            label4.Text = "Category";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(411, 827);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1189, 205);
            dataGridView1.TabIndex = 39;
            // 
            // txtQuantity
            // 
            txtQuantity.Font = new Font("Nirmala UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtQuantity.Location = new Point(1462, 69);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(229, 27);
            txtQuantity.TabIndex = 41;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Nirmala UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.White;
            label9.Location = new Point(1301, 58);
            label9.Name = "label9";
            label9.Size = new Size(132, 38);
            label9.TabIndex = 40;
            label9.Text = "Quantity";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(353, 147);
            label2.Name = "label2";
            label2.Size = new Size(141, 34);
            label2.TabIndex = 42;
            label2.Text = "Order By ";
            // 
            // cmbUser
            // 
            cmbUser.Font = new Font("Nirmala UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbUser.FormattingEnabled = true;
            cmbUser.Location = new Point(250, 167);
            cmbUser.Name = "cmbUser";
            cmbUser.Size = new Size(231, 36);
            cmbUser.TabIndex = 43;
            cmbUser.Visible = false;
            cmbUser.SelectedIndexChanged += cmbUser_SelectedIndexChanged;
            // 
            // txtAmount
            // 
            txtAmount.Font = new Font("Nirmala UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtAmount.Location = new Point(1052, 147);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new Size(229, 27);
            txtAmount.TabIndex = 45;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Nirmala UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(777, 143);
            label3.Name = "label3";
            label3.Size = new Size(248, 38);
            label3.TabIndex = 44;
            label3.Text = "Amount Recieved";
            // 
            // panel6
            // 
            panel6.BackColor = Color.FromArgb(46, 51, 80);
            panel6.Controls.Add(label6);
            panel6.Controls.Add(lblStockStatus);
            panel6.Controls.Add(lbltotal);
            panel6.Controls.Add(label5);
            panel6.Controls.Add(BtnCheckout);
            panel6.Controls.Add(btnClearcart);
            panel6.Controls.Add(BtnRemove);
            panel6.Controls.Add(dgvCart);
            panel6.Controls.Add(cmbUser);
            panel6.Controls.Add(lblTotalAmount);
            panel6.Controls.Add(label18);
            panel6.Location = new Point(379, 229);
            panel6.Name = "panel6";
            panel6.Size = new Size(1054, 299);
            panel6.TabIndex = 46;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(656, 0);
            label6.Name = "label6";
            label6.Size = new Size(176, 34);
            label6.TabIndex = 51;
            label6.Text = "Stock Status";
            // 
            // lblStockStatus
            // 
            lblStockStatus.AutoSize = true;
            lblStockStatus.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStockStatus.ForeColor = Color.GreenYellow;
            lblStockStatus.Location = new Point(826, 0);
            lblStockStatus.Name = "lblStockStatus";
            lblStockStatus.Size = new Size(176, 34);
            lblStockStatus.TabIndex = 50;
            lblStockStatus.Text = "Stock Status";
            // 
            // lbltotal
            // 
            lbltotal.AutoSize = true;
            lbltotal.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbltotal.ForeColor = Color.OrangeRed;
            lbltotal.Location = new Point(450, 0);
            lbltotal.Name = "lbltotal";
            lbltotal.Size = new Size(158, 34);
            lbltotal.TabIndex = 49;
            lbltotal.Text = "Order Cart";
            lbltotal.Click += lbltotal_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.White;
            label5.Location = new Point(272, 0);
            label5.Name = "label5";
            label5.Size = new Size(164, 34);
            label5.TabIndex = 48;
            label5.Text = "Order Total";
            // 
            // BtnCheckout
            // 
            BtnCheckout.BackColor = Color.FromArgb(46, 51, 76);
            BtnCheckout.FlatAppearance.BorderSize = 0;
            BtnCheckout.FlatStyle = FlatStyle.Flat;
            BtnCheckout.Font = new Font("Nirmala UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnCheckout.ForeColor = Color.FromArgb(192, 255, 255);
            BtnCheckout.ImageAlign = ContentAlignment.MiddleLeft;
            BtnCheckout.Location = new Point(698, 240);
            BtnCheckout.Name = "BtnCheckout";
            BtnCheckout.Size = new Size(304, 50);
            BtnCheckout.TabIndex = 22;
            BtnCheckout.Text = "Order CheckOut";
            BtnCheckout.UseVisualStyleBackColor = false;
            BtnCheckout.Click += BtnCheckout_Click;
            // 
            // btnClearcart
            // 
            btnClearcart.BackColor = Color.FromArgb(46, 51, 76);
            btnClearcart.FlatAppearance.BorderSize = 0;
            btnClearcart.FlatStyle = FlatStyle.Flat;
            btnClearcart.Font = new Font("Nirmala UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClearcart.ForeColor = Color.Red;
            btnClearcart.ImageAlign = ContentAlignment.MiddleLeft;
            btnClearcart.Location = new Point(807, 132);
            btnClearcart.Name = "btnClearcart";
            btnClearcart.Size = new Size(224, 50);
            btnClearcart.TabIndex = 21;
            btnClearcart.Text = "Clear Cart";
            btnClearcart.UseVisualStyleBackColor = false;
            btnClearcart.Click += btnClearcart_Click;
            // 
            // BtnRemove
            // 
            BtnRemove.BackColor = Color.FromArgb(46, 51, 76);
            BtnRemove.FlatAppearance.BorderSize = 0;
            BtnRemove.FlatStyle = FlatStyle.Flat;
            BtnRemove.Font = new Font("Nirmala UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnRemove.ForeColor = Color.FromArgb(192, 255, 255);
            BtnRemove.ImageAlign = ContentAlignment.MiddleLeft;
            BtnRemove.Location = new Point(807, 45);
            BtnRemove.Name = "BtnRemove";
            BtnRemove.Size = new Size(224, 50);
            BtnRemove.TabIndex = 20;
            BtnRemove.Text = "Remove Item";
            BtnRemove.UseVisualStyleBackColor = false;
            BtnRemove.Click += BtnRemove_Click;
            // 
            // dgvCart
            // 
            dgvCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCart.Location = new Point(3, 40);
            dgvCart.Name = "dgvCart";
            dgvCart.RowHeadersWidth = 51;
            dgvCart.Size = new Size(798, 188);
            dgvCart.TabIndex = 6;
            // 
            // lblTotalAmount
            // 
            lblTotalAmount.AutoSize = true;
            lblTotalAmount.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalAmount.ForeColor = Color.FromArgb(255, 128, 0);
            lblTotalAmount.Location = new Point(692, 252);
            lblTotalAmount.Name = "lblTotalAmount";
            lblTotalAmount.Size = new Size(0, 34);
            lblTotalAmount.TabIndex = 5;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label18.ForeColor = Color.White;
            label18.Location = new Point(3, 0);
            label18.Name = "label18";
            label18.Size = new Size(158, 34);
            label18.TabIndex = 5;
            label18.Text = "Order Cart";
            // 
            // BtnSave
            // 
            BtnSave.BackColor = Color.FromArgb(46, 51, 76);
            BtnSave.FlatAppearance.BorderSize = 0;
            BtnSave.FlatStyle = FlatStyle.Flat;
            BtnSave.Font = new Font("Nirmala UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnSave.ForeColor = Color.FromArgb(192, 255, 255);
            BtnSave.ImageAlign = ContentAlignment.MiddleLeft;
            BtnSave.Location = new Point(1342, 131);
            BtnSave.Name = "BtnSave";
            BtnSave.Size = new Size(304, 50);
            BtnSave.TabIndex = 23;
            BtnSave.Text = "Save Order";
            BtnSave.UseVisualStyleBackColor = false;
            BtnSave.Click += BtnSave_Click;
            // 
            // txtStaffname
            // 
            txtStaffname.Font = new Font("Nirmala UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtStaffname.Location = new Point(513, 154);
            txtStaffname.Name = "txtStaffname";
            txtStaffname.Size = new Size(238, 27);
            txtStaffname.TabIndex = 47;
            // 
            // Orders
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(46, 51, 76);
            ClientSize = new Size(1703, 1055);
            Controls.Add(txtStaffname);
            Controls.Add(BtnSave);
            Controls.Add(panel6);
            Controls.Add(txtAmount);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtQuantity);
            Controls.Add(label9);
            Controls.Add(dataGridView1);
            Controls.Add(label10);
            Controls.Add(panel1);
            Controls.Add(cmbBoxProdName);
            Controls.Add(ComboCategory);
            Controls.Add(label4);
            Name = "Orders";
            Text = "Orders";
            Load += Orders_Load_1;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCart).EndInit();
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
        private Label label10;
        private ComboBox cmbBoxProdName;
        private ComboBox ComboCategory;
        private Label label4;
        private DataGridView dataGridView1;
        private TextBox txtQuantity;
        private Label label9;
        private Label label2;
        private ComboBox cmbUser;
        private TextBox txtAmount;
        private Label label3;
        private Panel panel6;
        private Button btnClearcart;
        private Button BtnRemove;
        private DataGridView dgvCart;
        private Label lblTotalAmount;
        private Label label18;
        private Button BtnCheckout;
        private Button BtnSave;
        private TextBox txtStaffname;
        private Label lbltotal;
        private Label label5;
        private Label label6;
        private Label lblStockStatus;
    }
}