using MarketingDepartment.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MarketingDepartment.Forms
{
    public partial class CategoriesForm : Form
    {
        public CategoriesForm()
        {
            InitializeComponent();
        }

        private Function selectedFunction;
        private Button currentButton;

        private Color GetColor()
        {
            return Color.BlueViolet;
        }

        private void ActivateButton(object buttonSender)
        {
            if (currentButton != (Button)buttonSender)
            {
                DisableButtons();
                currentButton = (Button)buttonSender;
                currentButton.BackColor = GetColor();
                currentButton.ForeColor = Color.White;
                currentButton.Font = new System.Drawing.Font("Segoe UI Light", 14F);
            }
        }

        private void DisableButtons()
        {
            foreach (Control element in MenuPanel.Controls)
            {
                if (element.GetType() == typeof(Button))
                {
                    element.BackColor = Color.FromArgb(51, 51, 76);
                    element.ForeColor = Color.Gainsboro;
                    element.Font = new System.Drawing.Font("Segoe UI Light", 12F);
                }
            }
        }

        void ShowComponents()
        {
            panel1.Show();
            Submit.Show();
            dataGridView1.Show();
            dataGridView1.Columns.Clear();
        }

        void HideComponents()
        {
            dataGridView1.Hide();
            panel1.Hide();
            Submit.Hide();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            selectedFunction = Function.Add;
            ShowComponents();
            ActivateButton(sender);
            dataGridView1.Columns.Add("Categories", "Категория");
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            selectedFunction = Function.Delete;
            ShowComponents();
            ActivateButton(sender);
            using (var context = new MarketingDepartmentDatabaseEntities1())
            {
                var categories = context.Categories.Select(p => new NameAndId { Name = p.CategoryName, Id = p.CategoryId }).ToList();
                var productsComboBoxColumn = new DataGridViewComboBoxColumn
                {
                    HeaderText = "Категория",
                    DisplayMember = nameof(NameAndId.Name),
                    ValueMember = nameof(NameAndId.Id)
                };
                productsComboBoxColumn.DataSource = categories;
                dataGridView1.Columns.Add(productsComboBoxColumn);
            }
        }

        private void View_Click(object sender, EventArgs e)
        {
            selectedFunction = Function.View;
            ShowComponents();
            panel1.Hide();
            ActivateButton(sender);
            dataGridView1.Columns.Add(string.Empty, "Категория");

            using (var context = new MarketingDepartmentDatabaseEntities1())
            {
                var list = context.Categories.ToList();
                dataGridView1.Rows.Add(list.Count < 10 ? list.Count : 10);
                for (int i = 0; i < list.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = list[i].CategoryName;
                }
            }
        }

        private void Change_Click(object sender, EventArgs e)
        {
            selectedFunction = Function.Edit;
            ShowComponents();
            ActivateButton(sender);
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            switch (selectedFunction)
            {
                case Function.Add:
                    AddFunction();
                    break;
                case Function.Delete:
                    DeleteFunction();
                    break;
                case Function.View:
                    ViewFunction();
                    break;
                case Function.Edit:
                    ChangeFunction();
                    break;
            }
        }

        void AddFunction()
        {
            var categories = dataGridView1.Rows
                .Cast<DataGridViewRow>()
                .Select(r => TryGetCategory(r, out var c) ? c : null)
                .ToList();

            using (var context = new MarketingDepartmentDatabaseEntities1())
            {
                var categoryNamesToAdd = categories.Select(c => c.CategoryName).ToList();
                var duplicateCategoryNames = context.Categories
                    .Where(c => categoryNamesToAdd.Contains(c.CategoryName))
                    .Select(c => c.CategoryName)
                    .ToList();
                (string, string) data;
                if (duplicateCategoryNames.Any())
                {
                    var categoryNames = string.Join("\n", duplicateCategoryNames);

                    //duplicateCategoryNames.Count == 1 ? data = ("я", "е") : data = ("и", "ю");

                    if (duplicateCategoryNames.Count == 1)
                    {
                        data.Item1 = "я";
                        data.Item2 = "е";
                    }
                    else
                    {
                        data.Item1 = "и";
                        data.Item2 = "ю";
                    }
                    MessageBox.Show(this, $"Категори{data.Item1} уже существу{data.Item2}т:\n {categoryNames}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                context.Categories.AddRange(categories);
                context.SaveChanges();
                if (categories.Count == 1)
                {
                    data.Item1 = "я";
                    data.Item2 = "а";
                }
                else
                {
                    data.Item1 = "и";
                    data.Item2 = "ы";
                }
                MessageBox.Show(this, $"Категори{data.Item1} успешно добавлен{data.Item2}!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.None);
                HideComponents();
            }
        }

        void DeleteFunction()
        {
            if (!TryGetIds(dataGridView1, out var ids))
            {
                MessageBox.Show(this, "Не все ячейки заполнены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var context = new MarketingDepartmentDatabaseEntities1())
            {
                context.Categories.RemoveRange(context.Categories.Select(p => p).Where(p => ids.Contains(p.CategoryId)));
                context.SaveChanges();
            }
            HideComponents();
        }

        void ViewFunction()
        {
            HideComponents();
        }

        void ChangeFunction()
        {

        }

        private static bool TryGetCategory(DataGridViewRow row, out Category category)
        {
            var isCategoryNameValid = Parser.TryGetCellValueString(row.Cells[0], out var categoryName);
            if (!isCategoryNameValid)
            {
                category = null;
                return false;
            }

            category = new Category
            {
                CategoryName = categoryName,
            };
            return true;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBox1.Text, out int n) && n > 0)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Rows.Add(n);
            }
        }

        private static bool TryGetIds(DataGridView dataGridView, out List<int> ids)
        {
            ids = new List<int>();
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                var value = row.Cells[0].Value;
                if (value != null)
                {
                    if (!ids.Contains(Convert.ToInt32(value)))
                    {
                        ids.Add(Convert.ToInt32(value));
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
