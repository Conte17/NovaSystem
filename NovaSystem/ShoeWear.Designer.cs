namespace NovaSystem
{
    partial class ShoeWear
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShoeWear));
            label5 = new Label();
            panel1 = new Panel();
            btnSales = new Button();
            BtnInventory = new Button();
            BtnCosmetics = new Button();
            panel2 = new Panel();
            pictureBox1 = new PictureBox();
            LoginTag = new Label();
            label1 = new Label();
            BtnHome = new Button();
            label2 = new Label();
            dataGridViewShoes = new DataGridView();
            txtShoeName = new TextBox();
            txtBrand = new TextBox();
            label3 = new Label();
            label4 = new Label();
            cmbCategory = new ComboBox();
            txtColor = new TextBox();
            label6 = new Label();
            label7 = new Label();
            txtSize = new TextBox();
            label8 = new Label();
            txtSKU = new TextBox();
            label9 = new Label();
            txtBarcode = new TextBox();
            label10 = new Label();
            txtCostPrice = new TextBox();
            label11 = new Label();
            txtSellingPrice = new TextBox();
            label12 = new Label();
            txtQuantity = new TextBox();
            BtnSave = new Button();
            BtnDelete = new Button();
            BtnUpdate = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewShoes).BeginInit();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.White;
            label5.Location = new Point(349, 18);
            label5.Name = "label5";
            label5.Size = new Size(296, 34);
            label5.TabIndex = 28;
            label5.Text = "SHOE MANAGEMENT";
            // 
            // panel1
            // 
            panel1.BackColor = Color.MediumBlue;
            panel1.Controls.Add(btnSales);
            panel1.Controls.Add(BtnInventory);
            panel1.Controls.Add(BtnCosmetics);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(BtnHome);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(314, 939);
            panel1.TabIndex = 27;
            // 
            // btnSales
            // 
            btnSales.FlatAppearance.BorderSize = 0;
            btnSales.FlatStyle = FlatStyle.Flat;
            btnSales.Font = new Font("Nirmala UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSales.ForeColor = Color.FromArgb(192, 255, 255);
            btnSales.Image = (Image)resources.GetObject("btnSales.Image");
            btnSales.ImageAlign = ContentAlignment.MiddleLeft;
            btnSales.Location = new Point(-3, 478);
            btnSales.Name = "btnSales";
            btnSales.Size = new Size(314, 50);
            btnSales.TabIndex = 8;
            btnSales.Text = "Sales";
            btnSales.UseVisualStyleBackColor = true;
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
            // 
            // BtnCosmetics
            // 
            BtnCosmetics.FlatAppearance.BorderSize = 0;
            BtnCosmetics.FlatStyle = FlatStyle.Flat;
            BtnCosmetics.Font = new Font("Nirmala UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnCosmetics.ForeColor = Color.FromArgb(192, 255, 255);
            BtnCosmetics.Image = (Image)resources.GetObject("BtnCosmetics.Image");
            BtnCosmetics.ImageAlign = ContentAlignment.MiddleLeft;
            BtnCosmetics.Location = new Point(3, 348);
            BtnCosmetics.Name = "BtnCosmetics";
            BtnCosmetics.Size = new Size(314, 50);
            BtnCosmetics.TabIndex = 6;
            BtnCosmetics.Text = "Cosmetics";
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
            // BtnHome
            // 
            BtnHome.FlatAppearance.BorderSize = 0;
            BtnHome.FlatStyle = FlatStyle.Flat;
            BtnHome.Font = new Font("Nirmala UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnHome.ForeColor = Color.FromArgb(192, 255, 255);
            BtnHome.Image = (Image)resources.GetObject("BtnHome.Image");
            BtnHome.ImageAlign = ContentAlignment.MiddleLeft;
            BtnHome.Location = new Point(0, 275);
            BtnHome.Name = "BtnHome";
            BtnHome.Size = new Size(314, 50);
            BtnHome.TabIndex = 3;
            BtnHome.Text = "Home";
            BtnHome.UseVisualStyleBackColor = true;
            BtnHome.Click += btnHome_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(334, 85);
            label2.Name = "label2";
            label2.Size = new Size(118, 28);
            label2.TabIndex = 29;
            label2.Text = "Shoe Name";
            // 
            // dataGridViewShoes
            // 
            dataGridViewShoes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewShoes.Location = new Point(349, 712);
            dataGridViewShoes.Name = "dataGridViewShoes";
            dataGridViewShoes.RowHeadersWidth = 51;
            dataGridViewShoes.Size = new Size(1229, 201);
            dataGridViewShoes.TabIndex = 30;
            // 
            // txtShoeName
            // 
            txtShoeName.Font = new Font("Nirmala UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtShoeName.Location = new Point(458, 85);
            txtShoeName.Name = "txtShoeName";
            txtShoeName.Size = new Size(354, 31);
            txtShoeName.TabIndex = 31;
            // 
            // txtBrand
            // 
            txtBrand.Font = new Font("Nirmala UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBrand.Location = new Point(981, 85);
            txtBrand.Name = "txtBrand";
            txtBrand.Size = new Size(292, 31);
            txtBrand.TabIndex = 33;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(857, 85);
            label3.Name = "label3";
            label3.Size = new Size(119, 28);
            label3.TabIndex = 32;
            label3.Text = "Shoe Brand";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(1318, 85);
            label4.Name = "label4";
            label4.Size = new Size(95, 28);
            label4.TabIndex = 34;
            label4.Text = "Category";
            // 
            // cmbCategory
            // 
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Items.AddRange(new object[] { "Sneakers", "Boots", "Heels", "Sandals", "Formal", "Kids" });
            cmbCategory.Location = new Point(1470, 90);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(207, 28);
            cmbCategory.TabIndex = 35;
            // 
            // txtColor
            // 
            txtColor.Font = new Font("Nirmala UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtColor.Location = new Point(458, 185);
            txtColor.Name = "txtColor";
            txtColor.Size = new Size(354, 31);
            txtColor.TabIndex = 36;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(349, 186);
            label6.Name = "label6";
            label6.Size = new Size(61, 28);
            label6.TabIndex = 37;
            label6.Text = "Color";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.White;
            label7.Location = new Point(857, 184);
            label7.Name = "label7";
            label7.Size = new Size(47, 28);
            label7.TabIndex = 39;
            label7.Text = "Size";
            // 
            // txtSize
            // 
            txtSize.Font = new Font("Nirmala UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSize.Location = new Point(1069, 185);
            txtSize.Name = "txtSize";
            txtSize.Size = new Size(204, 31);
            txtSize.TabIndex = 38;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.White;
            label8.Location = new Point(1336, 181);
            label8.Name = "label8";
            label8.Size = new Size(50, 28);
            label8.TabIndex = 41;
            label8.Text = "SKU";
            // 
            // txtSKU
            // 
            txtSKU.Font = new Font("Nirmala UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSKU.Location = new Point(1443, 181);
            txtSKU.Name = "txtSKU";
            txtSKU.Size = new Size(284, 31);
            txtSKU.TabIndex = 40;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.White;
            label9.Location = new Point(345, 275);
            label9.Name = "label9";
            label9.Size = new Size(89, 28);
            label9.TabIndex = 43;
            label9.Text = "Barcode";
            // 
            // txtBarcode
            // 
            txtBarcode.Font = new Font("Nirmala UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBarcode.Location = new Point(469, 272);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Size = new Size(300, 31);
            txtBarcode.TabIndex = 42;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.White;
            label10.Location = new Point(809, 275);
            label10.Name = "label10";
            label10.Size = new Size(104, 28);
            label10.TabIndex = 45;
            label10.Text = "Cost price";
            // 
            // txtCostPrice
            // 
            txtCostPrice.Font = new Font("Nirmala UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCostPrice.Location = new Point(938, 272);
            txtCostPrice.Name = "txtCostPrice";
            txtCostPrice.Size = new Size(253, 31);
            txtCostPrice.TabIndex = 44;
            txtCostPrice.TextChanged += txtCostPrice_TextChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label11.ForeColor = Color.White;
            label11.Location = new Point(1318, 275);
            label11.Name = "label11";
            label11.Size = new Size(123, 28);
            label11.TabIndex = 47;
            label11.Text = "Selling Price";
            // 
            // txtSellingPrice
            // 
            txtSellingPrice.Font = new Font("Nirmala UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSellingPrice.Location = new Point(1474, 274);
            txtSellingPrice.Name = "txtSellingPrice";
            txtSellingPrice.Size = new Size(253, 31);
            txtSellingPrice.TabIndex = 46;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Calibri", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.White;
            label12.Location = new Point(349, 363);
            label12.Name = "label12";
            label12.Size = new Size(93, 28);
            label12.TabIndex = 49;
            label12.Text = "Quantity";
            // 
            // txtQuantity
            // 
            txtQuantity.Font = new Font("Nirmala UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtQuantity.Location = new Point(478, 360);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(253, 31);
            txtQuantity.TabIndex = 48;
            // 
            // BtnSave
            // 
            BtnSave.BackColor = Color.FromArgb(0, 192, 0);
            BtnSave.FlatAppearance.BorderSize = 0;
            BtnSave.FlatStyle = FlatStyle.Flat;
            BtnSave.Font = new Font("Nirmala UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnSave.ForeColor = Color.FromArgb(192, 255, 255);
            BtnSave.ImageAlign = ContentAlignment.MiddleLeft;
            BtnSave.Location = new Point(1549, 533);
            BtnSave.Name = "BtnSave";
            BtnSave.Size = new Size(167, 50);
            BtnSave.TabIndex = 50;
            BtnSave.Text = "Save";
            BtnSave.UseVisualStyleBackColor = false;
            BtnSave.Click += BtnSave_Click;
            // 
            // BtnDelete
            // 
            BtnDelete.BackColor = Color.IndianRed;
            BtnDelete.FlatAppearance.BorderSize = 0;
            BtnDelete.FlatStyle = FlatStyle.Flat;
            BtnDelete.Font = new Font("Nirmala UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnDelete.ForeColor = Color.FromArgb(192, 255, 255);
            BtnDelete.ImageAlign = ContentAlignment.MiddleLeft;
            BtnDelete.Location = new Point(561, 533);
            BtnDelete.Name = "BtnDelete";
            BtnDelete.Size = new Size(238, 50);
            BtnDelete.TabIndex = 52;
            BtnDelete.Text = "Delete Product";
            BtnDelete.UseVisualStyleBackColor = false;
            BtnDelete.Click += BtnDelete_Click_1;
            // 
            // BtnUpdate
            // 
            BtnUpdate.BackColor = SystemColors.HotTrack;
            BtnUpdate.FlatAppearance.BorderSize = 0;
            BtnUpdate.FlatStyle = FlatStyle.Flat;
            BtnUpdate.Font = new Font("Nirmala UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnUpdate.ForeColor = Color.FromArgb(192, 255, 255);
            BtnUpdate.ImageAlign = ContentAlignment.MiddleLeft;
            BtnUpdate.Location = new Point(909, 533);
            BtnUpdate.Name = "BtnUpdate";
            BtnUpdate.Size = new Size(271, 50);
            BtnUpdate.TabIndex = 51;
            BtnUpdate.Text = "Update product";
            BtnUpdate.UseVisualStyleBackColor = false;
            BtnUpdate.Click += BtnUpdate_Click_1;
            // 
            // ShoeWear
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(46, 51, 76);
            ClientSize = new Size(1739, 939);
            Controls.Add(BtnDelete);
            Controls.Add(BtnUpdate);
            Controls.Add(BtnSave);
            Controls.Add(label12);
            Controls.Add(txtQuantity);
            Controls.Add(label11);
            Controls.Add(txtSellingPrice);
            Controls.Add(label10);
            Controls.Add(txtCostPrice);
            Controls.Add(label9);
            Controls.Add(txtBarcode);
            Controls.Add(label8);
            Controls.Add(txtSKU);
            Controls.Add(label7);
            Controls.Add(txtSize);
            Controls.Add(label6);
            Controls.Add(txtColor);
            Controls.Add(cmbCategory);
            Controls.Add(label4);
            Controls.Add(txtBrand);
            Controls.Add(label3);
            Controls.Add(txtShoeName);
            Controls.Add(dataGridViewShoes);
            Controls.Add(label2);
            Controls.Add(label5);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ShoeWear";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ShoeWear";
            Load += ShoeWear_Load_1;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewShoes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label5;
        private Panel panel1;
        private Button btnSales;
        private Button BtnInventory;
        private Button BtnCosmetics;
        private Panel panel2;
        private PictureBox pictureBox1;
        private Label LoginTag;
        private Label label1;
        private Button BtnHome;
        private DataGridView dataGridViewShoes;
        private Label label2;
        private TextBox txtShoeName;
        private TextBox txtBrand;
        private Label label3;
        private Label label4;
        private ComboBox cmbCategory;
        private TextBox txtColor;
        private Label label6;
        private Label label7;
        private TextBox txtSize;
        private Label label8;
        private TextBox txtSKU;
        private Label label9;
        private TextBox txtBarcode;
        private Label label10;
        private TextBox txtCostPrice;
        private Label label11;
        private TextBox txtSellingPrice;
        private Label label12;
        private TextBox txtQuantity;
        private Button BtnSave;
        private Button BtnDelete;
        private Button BtnUpdate;
    }
}