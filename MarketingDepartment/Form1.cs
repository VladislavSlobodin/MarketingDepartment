using MarketingDepartment.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MarketingDepartment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void AddProductCategory_Click(object sender, EventArgs e)
        {
            ShowComponents();
            dataGridView1.Columns.Add("Categories", "Категория");
            panel1.Show();
            AddCategoryButton.Show();
        }

        private void AddCategoryButton_Click(object sender, EventArgs e)
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

        private void AddGoods_Click(object sender, EventArgs e)
        {
            ShowComponents();
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
                AddProductButton.Show();
            }
        }

        private void AddProductButton_Click(object sender, EventArgs e)
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

        private void AddCustomer_Click(object sender, EventArgs e)
        {
            ShowComponents();
            var comboBoxColumn = new DataGridViewComboBoxColumn
            {
                Name = "1",
                HeaderText = "Тип покупателя",
                DataSource = new String[] { "Физическое лицо", "Юридическое лицо" }
            };
            var columns = new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn
                {
                    HeaderText = "Фамилия"
                },
                new DataGridViewTextBoxColumn
                {
                    HeaderText = "Имя"
                },
                new DataGridViewTextBoxColumn
                {
                    HeaderText = "Отчество"
                },
                new DataGridViewTextBoxColumn
                {
                    HeaderText = "Адрес"
                },
                comboBoxColumn
            };
            dataGridView1.Columns.AddRange(columns);
            AddCustomerButton.Show();
        }

        private void AddCustomerButton_Click(object sender, EventArgs e)
        {
            var customers = dataGridView1.Rows
                .Cast<DataGridViewRow>()
                .Select(r => TryGetCustomer(r, out var c) ? c : null)
                .ToList();
            var isDataInvalid = customers.Any(c => c == null);

            using (var context = new MarketingDepartmentDatabaseEntities1())
            {
                if (!isDataInvalid)
                {
                    var customerNamesToAdd = customers
                    .Select(c => new Fullname { Firstname = c.Firstname, Lastname = c.Lastname, Patronymic = c.Patronymic })
                    .ToList();

                    var duplicateCustomerNames = GetDuplicateNames(customerNamesToAdd);
                    if (duplicateCustomerNames.Any())
                    {
                        var customerNames = string.Join(",\n", customerNamesToAdd.Select(c => c.Lastname + " " + c.Firstname + " " + c.Patronymic).ToList());//TODO: сформировать строку уже добавленных покупателей
                        if (duplicateCustomerNames.Count == 1)
                        {
                            MessageBox.Show(this, $"Покупатель с таким ФИО уже существует:\n {customerNames}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show(this, $"Покупатели с такими ФИО уже существуют:\n {customerNames}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        return;

                    }
                }

                if (isDataInvalid)
                {
                    var invalidRowsIds = customers
                        .Select((customer, rowNumber) => (customer, rowNumber))
                        .Where(x => x.customer == null)
                        .Select(x => x.rowNumber + 1)
                        .ToList();
                    var invalidRows = string.Join(", ", invalidRowsIds);
                    MessageBox.Show(this, $"Введена некорректная информация в строке {invalidRows}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                context.Customers.AddRange(customers);
                context.SaveChanges();
                HideComponents();
            }
        }

        private void AddBank_Click(object sender, EventArgs e)
        {
            ShowComponents();
            dataGridView1.Columns.Add("BankName", "Название банка");
            AddBankButton.Show();
        }

        private void AddBankButton_Click(object sender, EventArgs e)
        {
            var banks = dataGridView1.Rows
                            .Cast<DataGridViewRow>()
                            .Select(r => TryGetBank(r, out var b) ? b : null)
                            .ToList();
            var isDataInvalid = banks.Any(b => b == null);

            using (var context = new MarketingDepartmentDatabaseEntities1())
            {
                if (!isDataInvalid)
                {
                    var bankNamesToAdd = banks
                    .Select(c => c.BankName)
                    .ToList();

                    var duplicateBankNames = context.Banks
                        .Where(b => bankNamesToAdd.Contains(b.BankName)).Select(b => b.BankName)
                        .ToList();

                    if (duplicateBankNames.Any())
                    {
                        var bankNames = string.Join("\n", duplicateBankNames);
                        if (duplicateBankNames.Count == 1)
                        {
                            MessageBox.Show(this, $"Банк с таким названием уже существует:\n {bankNames}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show(this, $"Банки с такими названиями уже существуют:\n {bankNames}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        return;
                    }
                }

                if (isDataInvalid)
                {
                    var invalidRowsIds = banks
                        .Select((bank, rowNumber) => (bank, rowNumber))
                        .Where(x => x.bank == null)
                        .Select(x => x.rowNumber + 1)
                        .ToList();
                    var invalidRows = string.Join(", ", invalidRowsIds);
                    MessageBox.Show(this, $"Введена некорректная информация в строке {invalidRows}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                context.Banks.AddRange(banks);
                context.SaveChanges();
                HideComponents();
            }
        }

        private void Consigments_Click(object sender, EventArgs e)
        {
            var consigmentForm = new ConsigmentForm();
            consigmentForm.ShowDialog();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBox1.Text, out int n) && n > 0)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Rows.Add(n);
            }
        }

        private void HideComponents()
        {
            foreach (Button button in panel2.Controls)
            {
                button.Visible = false;
            }
            dataGridView1.Columns.Clear();
            dataGridView1.Hide();
            panel1.Hide();
            textBox1.Text = String.Empty;
        }

        private void ShowComponents()
        {
            dataGridView1.Show();
            panel1.Show();
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

        private static bool TryGetCustomer(DataGridViewRow row, out Customer customer)
        {
            var isLastnameValid = Parser.TryGetCellValueString(row.Cells[0], out var lastname);
            var isFirstnameValid = Parser.TryGetCellValueString(row.Cells[1], out var firstname);
            var isPatronymicValid = Parser.TryGetCellValueString(row.Cells[2], out var patronymic);
            var isAddressValid = Parser.TryGetCellValueString(row.Cells[3], out var address);
            var isTypeValid = Parser.TryGetCustomerType(row.Cells[4], out var type);

            if (!isLastnameValid || !isFirstnameValid || !isPatronymicValid || !isAddressValid || !isTypeValid)
            {
                customer = null;
                return false;
            }

            customer = new Customer
            {
                Firstname = firstname,
                Lastname = lastname,
                Patronymic = patronymic,
                Address = address,
                Type = type
            };

            return true;
        }

        private static bool TryGetBank(DataGridViewRow row, out Bank bank)
        {
            var isBankNameValid = Parser.TryGetCellValueString(row.Cells[0], out var bankname);

            if (!isBankNameValid)
            {
                bank = null;
                return false;
            }

            bank = new Bank
            {
                BankName = bankname
            };

            return true;
        }

        private static List<Fullname> GetDuplicateNames(List<Fullname> customerNamesToAdd)
        {
            List<Fullname> duplicateNames = new List<Fullname>();
            using (var context = new MarketingDepartmentDatabaseEntities1())
            {
                foreach (Fullname customerName in customerNamesToAdd)
                {
                    var customer = context.Customers.First(c => c.Lastname == customerName.Lastname && c.Firstname == customerName.Firstname && c.Patronymic == customerName.Patronymic);
                    if (customer != null)
                    {
                        duplicateNames.Add(new Fullname { Firstname = customer.Firstname, Lastname = customer.Lastname, Patronymic = customer.Patronymic });
                    }
                }
            }
            return duplicateNames;
        }

        private void Prices_Click(object sender, EventArgs e)
        {
            var pricesForm = new PricesForm();
            pricesForm.ShowDialog();
        }
    }
}