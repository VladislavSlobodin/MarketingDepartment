namespace MarketingDepartment
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.AddCategoryButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.Add = new System.Windows.Forms.ToolStripMenuItem();
            this.AddProductCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.AddProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.AddCustomer = new System.Windows.Forms.ToolStripMenuItem();
            this.AddBank = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.категориюТовараToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.товарToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.покупателяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.банкToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.просмотретьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.категориюТовараToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.товарToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.покупателяToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.банкToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.категориюТовараToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.товарToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.покупателяToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.банкToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Consigments = new System.Windows.Forms.ToolStripMenuItem();
            this.Prices = new System.Windows.Forms.ToolStripMenuItem();
            this.AddProductButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.AddBankButton = new System.Windows.Forms.Button();
            this.AddCustomerButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddCategoryButton
            // 
            this.AddCategoryButton.Font = new System.Drawing.Font("Segoe UI Light", 11.25F);
            this.AddCategoryButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.AddCategoryButton.Location = new System.Drawing.Point(4, 5);
            this.AddCategoryButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.AddCategoryButton.Name = "AddCategoryButton";
            this.AddCategoryButton.Size = new System.Drawing.Size(100, 40);
            this.AddCategoryButton.TabIndex = 4;
            this.AddCategoryButton.Text = "Ввод";
            this.AddCategoryButton.UseVisualStyleBackColor = true;
            this.AddCategoryButton.Visible = false;
            this.AddCategoryButton.Click += new System.EventHandler(this.AddCategoryButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu,
            this.Consigments,
            this.Prices});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1067, 25);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Menu
            // 
            this.Menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Add,
            this.удалитьToolStripMenuItem,
            this.просмотретьToolStripMenuItem,
            this.изменитьToolStripMenuItem});
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(53, 19);
            this.Menu.Text = "Меню";
            // 
            // Add
            // 
            this.Add.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddProductCategory,
            this.AddProduct,
            this.AddCustomer,
            this.AddBank});
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(148, 22);
            this.Add.Text = "Добавить";
            // 
            // AddProductCategory
            // 
            this.AddProductCategory.Name = "AddProductCategory";
            this.AddProductCategory.Size = new System.Drawing.Size(174, 22);
            this.AddProductCategory.Text = "Категорию товара";
            this.AddProductCategory.Click += new System.EventHandler(this.AddProductCategory_Click);
            // 
            // AddProduct
            // 
            this.AddProduct.Name = "AddProduct";
            this.AddProduct.Size = new System.Drawing.Size(174, 22);
            this.AddProduct.Text = "Товар";
            this.AddProduct.Click += new System.EventHandler(this.AddGoods_Click);
            // 
            // AddCustomer
            // 
            this.AddCustomer.Name = "AddCustomer";
            this.AddCustomer.Size = new System.Drawing.Size(174, 22);
            this.AddCustomer.Text = "Покупателя";
            this.AddCustomer.Click += new System.EventHandler(this.AddCustomer_Click);
            // 
            // AddBank
            // 
            this.AddBank.Name = "AddBank";
            this.AddBank.Size = new System.Drawing.Size(174, 22);
            this.AddBank.Text = "Банк";
            this.AddBank.Click += new System.EventHandler(this.AddBank_Click);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.категориюТовараToolStripMenuItem,
            this.товарToolStripMenuItem,
            this.покупателяToolStripMenuItem,
            this.банкToolStripMenuItem});
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            // 
            // категориюТовараToolStripMenuItem
            // 
            this.категориюТовараToolStripMenuItem.Name = "категориюТовараToolStripMenuItem";
            this.категориюТовараToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.категориюТовараToolStripMenuItem.Text = "Категорию товара";
            // 
            // товарToolStripMenuItem
            // 
            this.товарToolStripMenuItem.Name = "товарToolStripMenuItem";
            this.товарToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.товарToolStripMenuItem.Text = "Товар";
            // 
            // покупателяToolStripMenuItem
            // 
            this.покупателяToolStripMenuItem.Name = "покупателяToolStripMenuItem";
            this.покупателяToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.покупателяToolStripMenuItem.Text = "Покупателя";
            // 
            // банкToolStripMenuItem
            // 
            this.банкToolStripMenuItem.Name = "банкToolStripMenuItem";
            this.банкToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.банкToolStripMenuItem.Text = "Банк";
            // 
            // просмотретьToolStripMenuItem
            // 
            this.просмотретьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.категориюТовараToolStripMenuItem2,
            this.товарToolStripMenuItem2,
            this.покупателяToolStripMenuItem2,
            this.банкToolStripMenuItem2});
            this.просмотретьToolStripMenuItem.Name = "просмотретьToolStripMenuItem";
            this.просмотретьToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.просмотретьToolStripMenuItem.Text = "Просмотреть";
            // 
            // категориюТовараToolStripMenuItem2
            // 
            this.категориюТовараToolStripMenuItem2.Name = "категориюТовараToolStripMenuItem2";
            this.категориюТовараToolStripMenuItem2.Size = new System.Drawing.Size(174, 22);
            this.категориюТовараToolStripMenuItem2.Text = "Категорию товара";
            // 
            // товарToolStripMenuItem2
            // 
            this.товарToolStripMenuItem2.Name = "товарToolStripMenuItem2";
            this.товарToolStripMenuItem2.Size = new System.Drawing.Size(174, 22);
            this.товарToolStripMenuItem2.Text = "Товар";
            // 
            // покупателяToolStripMenuItem2
            // 
            this.покупателяToolStripMenuItem2.Name = "покупателяToolStripMenuItem2";
            this.покупателяToolStripMenuItem2.Size = new System.Drawing.Size(174, 22);
            this.покупателяToolStripMenuItem2.Text = "Покупателя";
            // 
            // банкToolStripMenuItem2
            // 
            this.банкToolStripMenuItem2.Name = "банкToolStripMenuItem2";
            this.банкToolStripMenuItem2.Size = new System.Drawing.Size(174, 22);
            this.банкToolStripMenuItem2.Text = "Банк";
            // 
            // изменитьToolStripMenuItem
            // 
            this.изменитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.категориюТовараToolStripMenuItem1,
            this.товарToolStripMenuItem1,
            this.покупателяToolStripMenuItem1,
            this.банкToolStripMenuItem1});
            this.изменитьToolStripMenuItem.Name = "изменитьToolStripMenuItem";
            this.изменитьToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.изменитьToolStripMenuItem.Text = "Изменить";
            // 
            // категориюТовараToolStripMenuItem1
            // 
            this.категориюТовараToolStripMenuItem1.Name = "категориюТовараToolStripMenuItem1";
            this.категориюТовараToolStripMenuItem1.Size = new System.Drawing.Size(174, 22);
            this.категориюТовараToolStripMenuItem1.Text = "Категорию товара";
            // 
            // товарToolStripMenuItem1
            // 
            this.товарToolStripMenuItem1.Name = "товарToolStripMenuItem1";
            this.товарToolStripMenuItem1.Size = new System.Drawing.Size(174, 22);
            this.товарToolStripMenuItem1.Text = "Товар";
            // 
            // покупателяToolStripMenuItem1
            // 
            this.покупателяToolStripMenuItem1.Name = "покупателяToolStripMenuItem1";
            this.покупателяToolStripMenuItem1.Size = new System.Drawing.Size(174, 22);
            this.покупателяToolStripMenuItem1.Text = "Покупателя";
            // 
            // банкToolStripMenuItem1
            // 
            this.банкToolStripMenuItem1.Name = "банкToolStripMenuItem1";
            this.банкToolStripMenuItem1.Size = new System.Drawing.Size(174, 22);
            this.банкToolStripMenuItem1.Text = "Банк";
            // 
            // Consigments
            // 
            this.Consigments.Name = "Consigments";
            this.Consigments.Size = new System.Drawing.Size(81, 19);
            this.Consigments.Text = "Накладные";
            this.Consigments.Click += new System.EventHandler(this.Consigments_Click);
            // 
            // Prices
            // 
            this.Prices.Name = "Prices";
            this.Prices.Size = new System.Drawing.Size(50, 19);
            this.Prices.Text = "Цены";
            this.Prices.Click += new System.EventHandler(this.Prices_Click);
            // 
            // AddProductButton
            // 
            this.AddProductButton.Font = new System.Drawing.Font("Segoe UI Light", 11.25F);
            this.AddProductButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.AddProductButton.Location = new System.Drawing.Point(4, 54);
            this.AddProductButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.AddProductButton.Name = "AddProductButton";
            this.AddProductButton.Size = new System.Drawing.Size(100, 40);
            this.AddProductButton.TabIndex = 16;
            this.AddProductButton.Text = "Ввод";
            this.AddProductButton.UseVisualStyleBackColor = true;
            this.AddProductButton.Visible = false;
            this.AddProductButton.Click += new System.EventHandler(this.AddProductButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(45, 148);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(783, 402);
            this.dataGridView1.TabIndex = 17;
            this.dataGridView1.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(189, 14);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(132, 27);
            this.textBox1.TabIndex = 18;
            this.textBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(4, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 20);
            this.label1.TabIndex = 19;
            this.label1.Text = "Количество записей";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Location = new System.Drawing.Point(45, 42);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(368, 57);
            this.panel1.TabIndex = 20;
            this.panel1.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.AddBankButton);
            this.panel2.Controls.Add(this.AddCustomerButton);
            this.panel2.Controls.Add(this.AddProductButton);
            this.panel2.Controls.Add(this.AddCategoryButton);
            this.panel2.Location = new System.Drawing.Point(836, 214);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(215, 266);
            this.panel2.TabIndex = 21;
            // 
            // AddBankButton
            // 
            this.AddBankButton.Font = new System.Drawing.Font("Segoe UI Light", 11.25F);
            this.AddBankButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.AddBankButton.Location = new System.Drawing.Point(4, 154);
            this.AddBankButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.AddBankButton.Name = "AddBankButton";
            this.AddBankButton.Size = new System.Drawing.Size(100, 40);
            this.AddBankButton.TabIndex = 18;
            this.AddBankButton.Text = "Ввод";
            this.AddBankButton.UseVisualStyleBackColor = true;
            this.AddBankButton.Visible = false;
            this.AddBankButton.Click += new System.EventHandler(this.AddBankButton_Click);
            // 
            // AddCustomerButton
            // 
            this.AddCustomerButton.Font = new System.Drawing.Font("Segoe UI Light", 11.25F);
            this.AddCustomerButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.AddCustomerButton.Location = new System.Drawing.Point(4, 104);
            this.AddCustomerButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.AddCustomerButton.Name = "AddCustomerButton";
            this.AddCustomerButton.Size = new System.Drawing.Size(100, 40);
            this.AddCustomerButton.TabIndex = 17;
            this.AddCustomerButton.Text = "Ввод";
            this.AddCustomerButton.UseVisualStyleBackColor = true;
            this.AddCustomerButton.Visible = false;
            this.AddCustomerButton.Click += new System.EventHandler(this.AddCustomerButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(696, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 692);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI Light", 11.25F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Отдел маркетинга";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button AddCategoryButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Menu;
        private System.Windows.Forms.ToolStripMenuItem Add;
        private System.Windows.Forms.ToolStripMenuItem AddProduct;
        private System.Windows.Forms.ToolStripMenuItem AddProductCategory;
        private System.Windows.Forms.ToolStripMenuItem AddCustomer;
        private System.Windows.Forms.ToolStripMenuItem AddBank;

        private System.Collections.Generic.List<int> daysInMonth = new System.Collections.Generic.List<int> { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        private System.Windows.Forms.Button AddProductButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button AddBankButton;
        private System.Windows.Forms.Button AddCustomerButton;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem категориюТовараToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem товарToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem покупателяToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem банкToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Consigments;
        private System.Windows.Forms.ToolStripMenuItem просмотретьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изменитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Prices;
        private System.Windows.Forms.ToolStripMenuItem категориюТовараToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem товарToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem покупателяToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem банкToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem категориюТовараToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem товарToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem покупателяToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem банкToolStripMenuItem1;
        private System.Windows.Forms.Button button1;
    }
}

