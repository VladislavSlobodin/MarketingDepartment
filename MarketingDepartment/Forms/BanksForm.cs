using MarketingDepartment.ViewModels;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MarketingDepartment.Forms
{
    public partial class BanksForm : Form
    {
        public BanksForm()
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
            dataGridView1.Columns.Add("BankName", "Название банка");
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            selectedFunction = Function.Delete;
            ShowComponents();
            ActivateButton(sender);

            using (var context = new MarketingDepartmentDatabaseEntities1())
            {
                var products = context.Banks.Select(p => new NameAndId { Name = p.BankName, Id = p.BankId }).ToList();
                var productsComboBoxColumn = new DataGridViewComboBoxColumn
                {
                    HeaderText = "Банк",
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
                    HeaderText = "Название",
                    ReadOnly = true
                }
            };
            dataGridView1.Columns.AddRange(columns);

            using (var context = new MarketingDepartmentDatabaseEntities1())
            {
                var list = context.Banks.ToList();
                dataGridView1.Rows.Add(list.Count < 10 ? list.Count : 10);
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = list[i].BankName;
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

        private void AddFunction()
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

        private void DeleteFunction()
        {
            if (!Parser.TryGetIds(dataGridView1, out var ids))
            {
                MessageBox.Show(this, "Не все ячейки заполнены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var context = new MarketingDepartmentDatabaseEntities1())
            {
                context.Banks.RemoveRange(context.Banks.Select(p => p).Where(p => ids.Contains(p.BankId)));
                context.SaveChanges();
            }
            HideComponents();
        }

        private void ViewFunction()
        {
            HideComponents();
        }

        private void ChangeFunction()
        {

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
