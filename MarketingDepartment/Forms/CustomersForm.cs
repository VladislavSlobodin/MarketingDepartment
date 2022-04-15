using MarketingDepartment.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MarketingDepartment.Forms
{
    public partial class CustomersForm : Form
    {
        public CustomersForm()
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
            label1.Show();
            textBox1.Show();
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
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            selectedFunction = Function.Delete;
            ShowComponents();
            ActivateButton(sender);

            using (var context = new MarketingDepartmentDatabaseEntities1())
            {
                var products = context.Customers.Select(p => new NameAndId { Name = p.Lastname + " " + p.Firstname + p.Patronymic, Id = p.CustomerId }).ToList();
                var productsComboBoxColumn = new DataGridViewComboBoxColumn
                {
                    HeaderText = "Покупатель",
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
            ActivateButton(sender);
            DataGridViewColumn[] columns = new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn
                {
                    HeaderText = "Фамилия",
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    HeaderText = "Имя",
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    HeaderText = "Отчество",
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    HeaderText = "Тип",
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    HeaderText = "Адрес",
                    ReadOnly = true
                }
            };
            dataGridView1.Columns.AddRange(columns);

            using (var context = new MarketingDepartmentDatabaseEntities1())
            {
                var list = context.Customers.ToList();
                dataGridView1.Rows.Add(list.Count < 10 ? list.Count : 10);
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = list[i].Lastname;
                    dataGridView1.Rows[i].Cells[1].Value = list[i].Firstname;
                    dataGridView1.Rows[i].Cells[2].Value = list[i].Patronymic;
                    dataGridView1.Rows[i].Cells[3].Value = list[i].Type == true ? "Юридическое лицо" : "Физическое лицо";
                    dataGridView1.Rows[i].Cells[4].Value = list[i].Address;
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

        void DeleteFunction()
        {
            if (!Parser.TryGetIds(dataGridView1, out var ids))
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

        void ViewFunction()
        {
            HideComponents();
        }

        void ChangeFunction()
        {

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

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBox1.Text, out int n) && n > 0)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Rows.Add(n);
            }
        }
    }
}
