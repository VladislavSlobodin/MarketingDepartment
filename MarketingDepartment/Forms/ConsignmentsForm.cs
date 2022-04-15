using MarketingDepartment.ViewModels;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MarketingDepartment.Forms
{
    public partial class ConsignmentsForm : Form
    {
        public ConsignmentsForm()
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
            panel2.Hide();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            selectedFunction = Function.Add;
            ShowComponents();
            panel2.Show();
            ActivateButton(sender);
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

        private void Delete_Click(object sender, EventArgs e)
        {
            selectedFunction = Function.Delete;
            ShowComponents();
            panel2.Hide();
            ActivateButton(sender);

            using (var context = new MarketingDepartmentDatabaseEntities1())
            {
                var consignments = context.Consignments.Select(p => p.ConsignmentId).ToList();
                var productsComboBoxColumn = new DataGridViewComboBoxColumn
                {
                    Name = string.Empty,
                    HeaderText = "Номер накладной"
                };
                productsComboBoxColumn.DataSource = consignments;
                dataGridView1.Columns.Add(productsComboBoxColumn);
            }
        }

        private void View_Click(object sender, EventArgs e)
        {
            selectedFunction = Function.View;
            ShowComponents();
            panel2.Hide();
            ActivateButton(sender);

            DataGridViewColumn[] columns = new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn
                {
                    HeaderText = "Покупатель",
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    HeaderText = "Банк",
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    HeaderText = "Адрес доставки",
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    HeaderText = "Дата",
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    HeaderText = "Стоимость",
                    ReadOnly = true
                }

            };
            dataGridView1.Columns.AddRange(columns);

            using (var context = new MarketingDepartmentDatabaseEntities1())
            {
                var list = context.Consignments.ToList();
                dataGridView1.Rows.Add(list.Count < 10 ? list.Count : 10);
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = list[i].Customer.Lastname + " " + list[i].Customer.Firstname.Substring(0, 1) + "." + list[i].Customer.Patronymic.Substring(0, 1) + ".";
                    dataGridView1.Rows[i].Cells[1].Value = list[i].Bank.BankName;
                    dataGridView1.Rows[i].Cells[2].Value = list[i].Destination;
                    dataGridView1.Rows[i].Cells[3].Value = list[i].Date;
                    dataGridView1.Rows[i].Cells[4].Value = list[i].Price;
                }
            }
        }

        private void Change_Click(object sender, EventArgs e)
        {
            selectedFunction = Function.Edit;
            ShowComponents();
            panel2.Hide();
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
                for (int i = 0; i < products.Count; i++)
                {
                    products[i].Count -= productCountsMap[products[i].ProductId];
                }
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

        void DeleteFunction()
        {
            if (!Parser.TryGetIds(dataGridView1, out var ids))
            {
                MessageBox.Show(this, "Не все ячейки заполнены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var context = new MarketingDepartmentDatabaseEntities1())
            {
                context.Consignments.RemoveRange(context.Consignments.Select(p => p).Where(p => ids.Contains(p.ConsignmentId)));
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

        private static bool TryGetData(DataGridViewRow row, out ProductIdAndCount data)
        {
            data = null;
            var cells = row.Cells;
            var isProductNameValid = Parser.TryGetCellValueInt(cells[0], out var productId);
            var isCountValid = Parser.TryGetCellValueInt(cells[1], out var count);
            if (!isProductNameValid || !isCountValid || count <= 0)
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

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBox1.Text, out int n) && n > 0)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Rows.Add(n);
            }
        }

        private void ConsignmentsForm_Load(object sender, EventArgs e)
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
    }
}
