namespace NovaSystem
{
    partial class Cosmetics
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cosmetics));
            panel1 = new Panel();
            BtnSales = new Button();
            BtnInventory = new Button();
            BtnShoewear = new Button();
            panel2 = new Panel();
            pictureBox1 = new PictureBox();
            LoginTag = new Label();
            label1 = new Label();
            btnHome = new Button();
            label2 = new Label();
            dataGridView1 = new DataGridView();
            BtnSave = new Button();
            label4 = new Label();
            label5 = new Label();
            txtSku = new TextBox();
            txtBarcode = new TextBox();
            label6 = new Label();
            label7 = new Label();
            txtCost = new TextBox();
            txtSellingprice = new TextBox();
            label8 = new Label();
            txtQuantity = new TextBox();
            label9 = new Label();
            button2 = new Button();
            BtnUpdate = new Button();
            BtnDelete = new Button();
            IDcombobox = new ComboBox();
            label11 = new Label();
            label10 = new Label();
            ComboCategory = new ComboBox();
            cmbBoxProdName = new ComboBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
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
            panel1.Size = new Size(314, 1021);
            panel1.TabIndex = 1;
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
            BtnShoewear.Click += button1_Click;
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
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(365, 22);
            label2.Name = "label2";
            label2.Size = new Size(347, 34);
            label2.TabIndex = 3;
            label2.Text = "Cosmetics Management";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(353, 691);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1202, 256);
            dataGridView1.TabIndex = 14;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // BtnSave
            // 
            BtnSave.BackColor = Color.FromArgb(0, 192, 0);
            BtnSave.FlatAppearance.BorderSize = 0;
            BtnSave.FlatStyle = FlatStyle.Flat;
            BtnSave.Font = new Font("Nirmala UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnSave.ForeColor = Color.FromArgb(192, 255, 255);
            BtnSave.ImageAlign = ContentAlignment.MiddleLeft;
            BtnSave.Location = new Point(1474, 560);
            BtnSave.Name = "BtnSave";
            BtnSave.Size = new Size(167, 50);
            BtnSave.TabIndex = 15;
            BtnSave.Text = "Save";
            BtnSave.UseVisualStyleBackColor = false;
            BtnSave.Click += BtnSave_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(365, 87);
            label4.Name = "label4";
            label4.Size = new Size(142, 34);
            label4.TabIndex = 18;
            label4.Text = "Category";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.White;
            label5.Location = new Point(737, 408);
            label5.Name = "label5";
            label5.Size = new Size(65, 34);
            label5.TabIndex = 20;
            label5.Text = "SKU";
            label5.Visible = false;
            // 
            // txtSku
            // 
            txtSku.Font = new Font("Nirmala UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSku.Location = new Point(871, 413);
            txtSku.Name = "txtSku";
            txtSku.Size = new Size(229, 27);
            txtSku.TabIndex = 21;
            txtSku.Visible = false;
            // 
            // txtBarcode
            // 
            txtBarcode.Font = new Font("Nirmala UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBarcode.Location = new Point(525, 193);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Size = new Size(229, 27);
            txtBarcode.TabIndex = 23;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Nirmala UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(376, 182);
            label6.Name = "label6";
            label6.Size = new Size(131, 38);
            label6.TabIndex = 22;
            label6.Text = "Barcode ";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Nirmala UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.White;
            label7.Location = new Point(815, 182);
            label7.Name = "label7";
            label7.Size = new Size(74, 38);
            label7.TabIndex = 24;
            label7.Text = "Cost";
            // 
            // txtCost
            // 
            txtCost.Font = new Font("Nirmala UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCost.Location = new Point(933, 189);
            txtCost.Name = "txtCost";
            txtCost.Size = new Size(229, 27);
            txtCost.TabIndex = 25;
            // 
            // txtSellingprice
            // 
            txtSellingprice.Font = new Font("Nirmala UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSellingprice.Location = new Point(1421, 193);
            txtSellingprice.Name = "txtSellingprice";
            txtSellingprice.Size = new Size(229, 27);
            txtSellingprice.TabIndex = 27;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Nirmala UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.White;
            label8.Location = new Point(1218, 182);
            label8.Name = "label8";
            label8.Size = new Size(178, 38);
            label8.TabIndex = 26;
            label8.Text = "Selling Price";
            // 
            // txtQuantity
            // 
            txtQuantity.Font = new Font("Nirmala UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtQuantity.Location = new Point(1486, 98);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(229, 27);
            txtQuantity.TabIndex = 29;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Nirmala UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.White;
            label9.Location = new Point(1334, 87);
            label9.Name = "label9";
            label9.Size = new Size(132, 38);
            label9.TabIndex = 28;
            label9.Text = "Quantity";
            // 
            // button2
            // 
            button2.BackColor = Color.DarkKhaki;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Nirmala UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.FromArgb(192, 255, 255);
            button2.ImageAlign = ContentAlignment.MiddleLeft;
            button2.Location = new Point(365, 560);
            button2.Name = "button2";
            button2.Size = new Size(210, 50);
            button2.TabIndex = 30;
            button2.Text = "Scan Barcode";
            button2.UseVisualStyleBackColor = false;
            // 
            // BtnUpdate
            // 
            BtnUpdate.BackColor = SystemColors.HotTrack;
            BtnUpdate.FlatAppearance.BorderSize = 0;
            BtnUpdate.FlatStyle = FlatStyle.Flat;
            BtnUpdate.Font = new Font("Nirmala UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnUpdate.ForeColor = Color.FromArgb(192, 255, 255);
            BtnUpdate.ImageAlign = ContentAlignment.MiddleLeft;
            BtnUpdate.Location = new Point(1163, 560);
            BtnUpdate.Name = "BtnUpdate";
            BtnUpdate.Size = new Size(271, 50);
            BtnUpdate.TabIndex = 31;
            BtnUpdate.Text = "Update product";
            BtnUpdate.UseVisualStyleBackColor = false;
            BtnUpdate.Click += BtnUpdate_Click;
            // 
            // BtnDelete
            // 
            BtnDelete.BackColor = Color.IndianRed;
            BtnDelete.FlatAppearance.BorderSize = 0;
            BtnDelete.FlatStyle = FlatStyle.Flat;
            BtnDelete.Font = new Font("Nirmala UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnDelete.ForeColor = Color.FromArgb(192, 255, 255);
            BtnDelete.ImageAlign = ContentAlignment.MiddleLeft;
            BtnDelete.Location = new Point(815, 560);
            BtnDelete.Name = "BtnDelete";
            BtnDelete.Size = new Size(238, 50);
            BtnDelete.TabIndex = 32;
            BtnDelete.Text = "Delete Product";
            BtnDelete.UseVisualStyleBackColor = false;
            BtnDelete.Click += BtnDelete_Click;
            // 
            // IDcombobox
            // 
            IDcombobox.FormattingEnabled = true;
            IDcombobox.Items.AddRange(new object[] { "SNK0101", "CM0101" });
            IDcombobox.Location = new Point(549, 408);
            IDcombobox.Name = "IDcombobox";
            IDcombobox.Size = new Size(88, 28);
            IDcombobox.TabIndex = 13;
            IDcombobox.Visible = false;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.ForeColor = Color.White;
            label11.Location = new Point(380, 402);
            label11.Name = "label11";
            label11.Size = new Size(163, 34);
            label11.TabIndex = 12;
            label11.Text = "Product ID ";
            label11.Visible = false;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.White;
            label10.Location = new Point(789, 87);
            label10.Name = "label10";
            label10.Size = new Size(210, 34);
            label10.TabIndex = 34;
            label10.Text = "Product Name";
            // 
            // ComboCategory
            // 
            ComboCategory.Font = new Font("Nirmala UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ComboCategory.FormattingEnabled = true;
            ComboCategory.Items.AddRange(new object[] { "Coconut", "collagen", "Turmeric", "Vitamin  c", "Salicylic Acid", "Cantu", "Niacinamide", "shower gel" });
            ComboCategory.Location = new Point(525, 87);
            ComboCategory.Name = "ComboCategory";
            ComboCategory.Size = new Size(229, 36);
            ComboCategory.TabIndex = 43;
            // 
            // cmbBoxProdName
            // 
            cmbBoxProdName.Font = new Font("Nirmala UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbBoxProdName.FormattingEnabled = true;
            cmbBoxProdName.Items.AddRange(new object[] { "Body lotion", "Emollient  oil ", "Hair mask ", "Lipbalm ", "Hand cream ", "Shower gel ", "Shampoo  ", "Foot cream ", "Sunscreen spray ", "Face cream ", "Hair mask ", "Hand cream", "Eye cream", "Firming  gel", "Eye mask", "Face mask ", "Face toner ", "Face serum ", "Face lotion", "Face cleanser ", "Cleaning  moose  ", "Body lotion ", "Turmeric soap ", "Cleanser", "Toner ", "Body scrub ", "Face serum", "Face lotion ", "Eye cream", "Face cream ", "Face serum ", "Face mask ", "Face lotion ", "Cleanser ", "Eye cream", "Curling cream ", "Custard and shine gel ", "Strawberry lip scrub ", "Orange lip scrub ", "Honey peach lip scrub ", "Coconut lip balm", "Repair serum ", "Coffee scrub ", "Blueberry scrub ", "Mango scrub ", "Sunscreen cream ", "Sunscreen spray ", "Black mask ", "Peel off mask ", "Hair mask ", "Body lotion", "Kiwi  shower gel ", "Blueberry shower gel", "Men cleanser", "Turmeric scrub", "24k gold" });
            cmbBoxProdName.Location = new Point(1017, 87);
            cmbBoxProdName.Name = "cmbBoxProdName";
            cmbBoxProdName.Size = new Size(280, 36);
            cmbBoxProdName.TabIndex = 44;
            // 
            // Cosmetics
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(46, 51, 76);
            ClientSize = new Size(1771, 1021);
            Controls.Add(cmbBoxProdName);
            Controls.Add(ComboCategory);
            Controls.Add(label10);
            Controls.Add(BtnDelete);
            Controls.Add(BtnUpdate);
            Controls.Add(button2);
            Controls.Add(txtQuantity);
            Controls.Add(label9);
            Controls.Add(txtSellingprice);
            Controls.Add(label8);
            Controls.Add(txtCost);
            Controls.Add(label7);
            Controls.Add(txtBarcode);
            Controls.Add(label6);
            Controls.Add(txtSku);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(BtnSave);
            Controls.Add(dataGridView1);
            Controls.Add(IDcombobox);
            Controls.Add(label11);
            Controls.Add(label2);
            Controls.Add(panel1);
            Name = "Cosmetics";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cosmetics";
            Load += Cosmetics_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
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
        private Label label2;
        private DataGridView dataGridView1;
        private Button BtnSave;
        private Label label4;
        private Label label5;
        private TextBox txtSku;
        private TextBox txtBarcode;
        private Label label6;
        private Label label7;
        private TextBox txtCost;
        private TextBox txtSellingprice;
        private Label label8;
        private TextBox txtQuantity;
        private Label label9;
        private Button button2;
        private Button BtnUpdate;
        private Button BtnDelete;
        private ComboBox IDcombobox;
        private Label label11;
        private Label label10;
        private ComboBox ComboCategory;
        private ComboBox cmbBoxProdName;
    }
}