namespace MarketingDepartment.Forms
{
    partial class ProductsForm
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
            this.MenuPanel = new System.Windows.Forms.Panel();
            this.Edit = new System.Windows.Forms.Button();
            this.View = new System.Windows.Forms.Button();
            this.Delete = new System.Windows.Forms.Button();
            this.Add = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Submit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MenuPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuPanel
            // 
            this.MenuPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.MenuPanel.Controls.Add(this.Edit);
            this.MenuPanel.Controls.Add(this.View);
            this.MenuPanel.Controls.Add(this.Delete);
            this.MenuPanel.Controls.Add(this.Add);
            this.MenuPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.MenuPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MenuPanel.Location = new System.Drawing.Point(0, 0);
            this.MenuPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MenuPanel.Name = "MenuPanel";
            this.MenuPanel.Size = new System.Drawing.Size(120, 461);
            this.MenuPanel.TabIndex = 1;
            // 
            // Edit
            // 
            this.Edit.Dock = System.Windows.Forms.DockStyle.Top;
            this.Edit.FlatAppearance.BorderSize = 0;
            this.Edit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Edit.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.Edit.ForeColor = System.Drawing.Color.Gainsboro;
            this.Edit.Location = new System.Drawing.Point(0, 90);
            this.Edit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Edit.Name = "Edit";
            this.Edit.Size = new System.Drawing.Size(120, 30);
            this.Edit.TabIndex = 4;
            this.Edit.Text = "Изменить";
            this.Edit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Edit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Edit.UseVisualStyleBackColor = true;
            this.Edit.Click += new System.EventHandler(this.Edit_Click);
            // 
            // View
            // 
            this.View.Dock = System.Windows.Forms.DockStyle.Top;
            this.View.FlatAppearance.BorderSize = 0;
            this.View.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.View.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.View.ForeColor = System.Drawing.Color.Gainsboro;
            this.View.Location = new System.Drawing.Point(0, 60);
            this.View.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.View.Name = "View";
            this.View.Size = new System.Drawing.Size(120, 30);
            this.View.TabIndex = 3;
            this.View.Text = "Просмотреть";
            this.View.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.View.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.View.UseVisualStyleBackColor = true;
            this.View.Click += new System.EventHandler(this.View_Click);
            // 
            // Delete
            // 
            this.Delete.Dock = System.Windows.Forms.DockStyle.Top;
            this.Delete.FlatAppearance.BorderSize = 0;
            this.Delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Delete.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.Delete.ForeColor = System.Drawing.Color.Gainsboro;
            this.Delete.Location = new System.Drawing.Point(0, 30);
            this.Delete.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(120, 30);
            this.Delete.TabIndex = 2;
            this.Delete.Text = "Удалить";
            this.Delete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // Add
            // 
            this.Add.Dock = System.Windows.Forms.DockStyle.Top;
            this.Add.FlatAppearance.BorderSize = 0;
            this.Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Add.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.Add.ForeColor = System.Drawing.Color.Gainsboro;
            this.Add.Location = new System.Drawing.Point(0, 0);
            this.Add.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(120, 30);
            this.Add.TabIndex = 1;
            this.Add.Text = "Добавить";
            this.Add.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Add.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(389, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(345, 150);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Количество записей";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(133, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Submit
            // 
            this.Submit.FlatAppearance.BorderSize = 0;
            this.Submit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Submit.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.Submit.ForeColor = System.Drawing.Color.Gainsboro;
            this.Submit.Location = new System.Drawing.Point(127, 132);
            this.Submit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Submit.Name = "Submit";
            this.Submit.Size = new System.Drawing.Size(120, 30);
            this.Submit.TabIndex = 7;
            this.Submit.Text = "OK";
            this.Submit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Submit.UseVisualStyleBackColor = true;
            this.Submit.Visible = false;
            this.Submit.Click += new System.EventHandler(this.Submit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(127, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(256, 48);
            this.panel1.TabIndex = 10;
            this.panel1.Visible = false;
            // 
            // ProductsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 461);
            this.Controls.Add(this.Submit);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.MenuPanel);
            this.Name = "ProductsForm";
            this.Text = "Продукты";
            this.MenuPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MenuPanel;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button Edit;
        private System.Windows.Forms.Button View;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Submit;
        private System.Windows.Forms.Panel panel1;
    }
}