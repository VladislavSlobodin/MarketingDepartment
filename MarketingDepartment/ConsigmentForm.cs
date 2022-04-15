using MarketingDepartment.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MarketingDepartment
{
    public partial class ConsigmentForm : Form
    {
        public ConsigmentForm()
        {
            InitializeComponent();
        }

        private void AddConsignment_Click(object sender, EventArgs e)
        {
            ShowComponents();
            using (var context = new MarketingDepartmentDatabaseEntities1())
            {
                DataGridViewComboBoxColumn productsComboBoxColumn = new DataGridViewComboBoxColumn
                {
                    HeaderText = "Товар",
                    DisplayMember = nameof(NameAndId.Name),
                    ValueMember = nameof(NameAndId.Id)
                };

                productsComboBoxColumn.DataSource = context.Products.Select(p => new NameAndId { Id = p.ProductId, Name = p.ProductName }).ToList();

                var columns = new DataGridViewColumn[]
                {
                    productsComboBoxColumn,
                    new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Количество"
                    }
                };
                dataGridView1.Columns.AddRange(columns);
            }
        }

        private void ShowComponents()
        {
            panel1.Show();
            panel2.Show();
            dataGridView1.Show();
        }

        private void HideComponents()
        {
            panel1.Hide();
            panel2.Hide();
            dataGridView1.Hide();
            dataGridView1.Columns.Clear();
        }

        private void AddLine_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add();
        }

        private void DeleteLine_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int value) && value > 0)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Rows.Add(value);
            }
        }

        private void AddConsignmentButton_Click(object sender, EventArgs e)
        {
            var consigmentDataAsList = dataGridView1.Rows.Cast<DataGridViewRow>()
                .Select(r => TryGetData(r, out var data) ? data : null)
                .ToList();
            var destination = textBox2.Text;
            var isCustomerIdValid = TryGetCustomerId(comboBox1.SelectedItem, out var customerId);
            var isBankIdValid = TryGetBankId(comboBox2.SelectedItem, out var bankId);
            var isDataInvalid = consigmentDataAsList.Any(d => d == null);
            var date = monthCalendar1.SelectionStart;

            if (!isCustomerIdValid || !isBankIdValid || isDataInvalid || destination == string.Empty)
            {
                MessageBox.Show(this, "Неверно заполнена форма накладной", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //var normalizedDataList = Normalize(consigmentDataAsList);
            var normalizedDataList = consigmentDataAsList
                .GroupBy(p => p.ProductId)
                .Select(g => new ProductIdAndCount
            {
                ProductId = g.Key,
                Count = g.Sum(p => p.Count),
            }).ToList();

            var productIds = normalizedDataList.Select(p => p.ProductId).ToList();

            using (var context = new MarketingDepartmentDatabaseEntities1())
            {
                var products = context.Products.Where(p => productIds.Contains(p.ProductId)).ToList();
                var productCountsMap = normalizedDataList.ToDictionary(p => p.ProductId, p => p.Count);
                var isProductsNotAvailable = products.Any(p => p.Count < productCountsMap[p.ProductId]);

                if (isProductsNotAvailable)
                {
                    MessageBox.Show(this, "Недостаточно товаров на складе", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var price = products.Sum(p => p.Price * productCountsMap[p.ProductId]);

                var consignment = new Consignment()
                {
                    CustomerId = customerId,
                    BankId = bankId,
                    Destination = destination,
                    Date = date,
                    Price = price
                };

                var consignmentProducts = products.Select(p => new ConsignmentsProduct
                {
                    Consignment = consignment,
                    Product = p,
                    ProductsCount = productCountsMap[p.ProductId]
                }).ToList();

                //context.Consignments.Add(consignment);
                context.ConsignmentsProducts.AddRange(consignmentProducts);
                context.SaveChanges();
            }
        }

        private static List<double> GetPrices(List<ProductIdAndCount> list)
        {
            List<double> priceList = new List<double>();
            using (var context = new MarketingDepartmentDatabaseEntities1())
            {
                foreach (var item in list)
                {
                    var price = context.Products.First(d => d.ProductId == item.ProductId).Price;
                    if (context.Products.Any(d => d.ProductId == item.ProductId))
                    {
                        priceList.Add(context.Products.First(d => d.ProductId == item.ProductId).Price);
                    }
                }
            }
            return priceList;
        }

        private static bool DataCorrect(ProductIdAndCount data)
        {
            using (var context = new MarketingDepartmentDatabaseEntities1())
            {
                return context.Products.Any(p => p.ProductId == data.ProductId && p.Count >= data.Count);
            }
        }


        private static bool TryGetData(DataGridViewRow row, out ProductIdAndCount data)
        {
            data = null;
            var cells = row.Cells;
            var isProductNameValid = Parser.TryGetCellValueInt(cells[0], out var productId);
            var isCountValid = Parser.TryGetCellValueInt(cells[1], out var count);
            if (!isProductNameValid || !isCountValid)
            {
                return false;
            }

            data = new ProductIdAndCount
            {
                ProductId = productId,
                Count = count
            };

            return true;
        }

        private static bool TryGetCustomerId(object data, out int customerId)
        {
            customerId = 0;
            if (data != null)
            {
                customerId = ((Customer)data).CustomerId;
                return true;
            }
            return false;
        }

        private static bool TryGetBankId(object data, out int bankId)
        {
            bankId = 0;
            if (data != null)
            {
                bankId = ((Bank)data).BankId;
                return true;
            }
            return false;
        }

        private void ConsigmentForm_Load(object sender, EventArgs e)
        {
            using (var context = new MarketingDepartmentDatabaseEntities1())
            {
                comboBox1.Items.AddRange(context.Customers.ToArray());
                comboBox1.DisplayMember = nameof(Customer.Lastname);
                comboBox1.ValueMember = nameof(Customer.CustomerId);
                comboBox2.Items.AddRange(context.Banks.ToArray());
                comboBox2.DisplayMember = nameof(Bank.BankName);
                comboBox2.ValueMember = nameof(Bank.BankId);
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
