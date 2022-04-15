using MarketingDepartment.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MarketingDepartment.Forms
{
    enum Function
    {
        Add,
        Delete,
        View,
        Edit
    }

    public partial class ProductsForm : Form
    {
        public ProductsForm()
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
            label1.Hide();
            textBox1.Hide();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            selectedFunction = Function.Add;
            ShowComponents();
            ActivateButton(sender);
            using (var context = new MarketingDepartmentDatabaseEntities1())
            {
                var categoriesComboBoxColumn = new DataGridViewComboBoxColumn
                {
                    Name = "1",
                    HeaderText = "Категория",
                    DisplayMember = nameof(NameAndId.Name),
                    ValueMember = nameof(NameAndId.Id)
                };
                var categories = context.Categories.Select(c => new NameAndId { Id = c.CategoryId, Name = c.CategoryName }).ToList();
                categoriesComboBoxColumn.DataSource = categories;
                var columns = new DataGridViewColumn[]
                {
                    new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Название"
                    },
                    new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Количество"
                    },
                    categoriesComboBoxColumn,
                    new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Цена"
                    }
                };
                dataGridView1.Columns.AddRange(columns);
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            selectedFunction = Function.Delete;
            ShowComponents();
            ActivateButton(sender);

            using (var context = new MarketingDepartmentDatabaseEntities1())
            {
                var products = context.Products.Select(p => new NameAndId { Name = p.ProductName, Id = p.ProductId }).ToList();
                var productsComboBoxColumn = new DataGridViewComboBoxColumn
                {
                    HeaderText = "Категория",
                    DisplayMember = nameof(NameAndId.Name),
                    ValueMember = nameof(NameAndId.Id)
                };
                productsComboBoxColumn.DataSource = products;
                dataGridView1.Columns.Add(productsComboBoxColumn);
            }

        }

        private void View_Click(object sender, EventArgs e)
        {
            selectedFunction = Function.View;
            ShowComponents();
            panel1.Hide();
            ActivateButton(sender);
            DataGridViewColumn[] columns = new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn
                {
                    HeaderText = "Название",
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    HeaderText = "Количество",
                    ReadOnly = true
                }
            };
            dataGridView1.Columns.AddRange(columns);

            using (var context = new MarketingDepartmentDatabaseEntities1())
            {
                var list = context.Products.ToList();
                dataGridView1.Rows.Add(list.Count < 10 ? list.Count : 10);
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = list[i].ProductName;
                    dataGridView1.Rows[i].Cells[1].Value = list[i].Count;
                }
            }
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            selectedFunction = Function.Edit;
            ShowComponents();
            ActivateButton(sender);
            var productNamesComboBoxColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = "Название",
                DisplayMember = nameof(NameAndId.Name),
                ValueMember = nameof(NameAndId.Id)
            };
            using (var context = new MarketingDepartmentDatabaseEntities1())
            {
                var products = context.Products.Select(p => new NameAndId { Id = p.ProductId, Name = p.ProductName }).ToList();
                productNamesComboBoxColumn.DataSource = products;
                DataGridViewColumn[] columns = new DataGridViewColumn[]
                {
                    productNamesComboBoxColumn,
                    new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Цена"
                    },
                    new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Количество"
                    }
                };
                dataGridView1.Columns.AddRange(columns);
            }
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
                    EditFunction();
                    break;
            }
        }

        void AddFunction()
        {
            var products = dataGridView1.Rows
                .Cast<DataGridViewRow>()
                .Select(r => TryGetProduct(r, out var p) ? p : null)
                .ToList();
            var isDataInvalid = products.Any(p => p == null);
            using (var context = new MarketingDepartmentDatabaseEntities1())
            {
                if (!isDataInvalid)
                {
                    var productNamesToAdd = products
                        .Select(p => p.ProductName)
                        .ToList();

                    var duplicateProductNames = context.Products
                        .Where(p => productNamesToAdd.Contains(p.ProductName))
                        .Select(p => p.ProductName)
                        .ToList();

                    if (duplicateProductNames.Any())
                    {
                        var productNames = string.Join(", ", duplicateProductNames);
                        MessageBox.Show(this, $"Продукт уже существует {productNames}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (isDataInvalid)
                {
                    var invalidRowsIds = products
                        .Select((product, rowNumber) => (product, rowNumber))
                        .Where(x => x.product == null)
                        .Select(x => x.rowNumber + 1)
                        .ToList();
                    var invalidRows = string.Join(", ", invalidRowsIds);
                    MessageBox.Show(this, $"Введена некорректная информация в строке {invalidRows}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                context.Products.AddRange(products);
                context.SaveChanges();
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
                context.Products.RemoveRange(context.Products.Select(p => p).Where(p => ids.Contains(p.ProductId)));
                context.SaveChanges();
            }
            HideComponents();
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

        void ViewFunction()
        {
            HideComponents();
        }

        void EditFunction()
        {
            if (!TryGetData(dataGridView1, out var dataList))
            {
                MessageBox.Show(this, "Не все ячейки заполнены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (var context = new MarketingDepartmentDatabaseEntities1())
            {
                var products = context.Products.Select(p => p).ToList();
                products = UpdateProducts(products, dataList, out var priceChanges);
                var priceHistories = GetPriceHistories(priceChanges);          
                var list = context.PriceHistories.AddRange(priceHistories);
                context.SaveChanges();
            }
        }

        private static bool TryGetData(DataGridView dataGridView, out List<(int, double, int)> data)
        {
            data = new List<(int, double, int)>();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                var item1 = row.Cells[0].Value;
                var item2 = row.Cells[1].Value;
                var item3 = row.Cells[2].Value;
                if (item1 != null && double.TryParse(item2 as string, out var price) && int.TryParse(item3 as string, out var count) && price > 0 && count > 0)
                {
                    data.Add((Convert.ToInt32(item1), price, count));
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private static List<Product> UpdateProducts(List<Product> products, List<(int, double, int)> dataList, out List<(int, double)> priceHistories)
        {
            priceHistories = new List<(int, double)>();
            var i = 0;
            while (i < products.Count)
            {
                for (int j = 0; j < dataList.Count; j++)
                {
                    if (dataList[j].Item1 == products[i].ProductId)
                    {
                        products[i].Price = dataList[j].Item2;
                        products[i].Count = dataList[j].Item3;
                        priceHistories.Add((dataList[j].Item1, dataList[j].Item2));
                        break;
                    }
                }
                i++;
            }
            return products;
        }

        private static List<PriceHistory> GetPriceHistories(List<(int, double)> priceChanges)
        {
            var priceHistories = new List<PriceHistory>();
            for (int i = 0; i < priceChanges.Count; i++)
            {
                var priceHistory = new PriceHistory
                {
                    Date = DateTime.Now,
                    ProductId = priceChanges[i].Item1,
                    Price = (float)priceChanges[i].Item2
                };
                priceHistories.Add(priceHistory);
            }
            return priceHistories;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBox1.Text, out int n) && n > 0)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Rows.Add(n);
            }
        }

        private static bool TryGetProduct(DataGridViewRow row, out Product product)
        {
            product = null;
            var cells = row.Cells;
            var isProductNameValid = Parser.TryGetCellValueString(cells[0], out var productName);
            var isCountValid = Parser.TryGetCellValueInt(cells[1], out var count);
            var isCategoryIdValid = Parser.TryGetCellValueInt(cells[2], out var categoryId);
            var isPriceValid = Parser.TryGetCellValueFloat(cells[3], out var price);
            if (!isProductNameValid || !isCountValid || !isCategoryIdValid || !isPriceValid)
            {
                return false;
            }

            var priceHistory = new PriceHistory
            {
                Date = DateTime.Now,
                Price = price
            };

            product = new Product
            {
                ProductName = productName,
                CategoryId = categoryId,
                Count = count,
                Price = price,
                PriceHistories = new List<PriceHistory> { priceHistory }
            };

            return true;
        }

        bool TryGetCellValueInt(DataGridViewCell cell, out int? value)
        {
            value = null;
            if (cell.Value != null)
            {
                value = Convert.ToInt32(cell.Value);
                return true;
            }
            return false;
        }
    }
}
