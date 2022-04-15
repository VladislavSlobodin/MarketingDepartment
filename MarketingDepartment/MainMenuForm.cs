using MarketingDepartment.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarketingDepartment
{
    public partial class MainMenuForm : Form
    {
        private Button currentButton;
        private Form activeForm;

        private Color GetColor()
        {
            return Color.BlueViolet;
        }

        private void ActivateButton(object buttonSender)
        {
            if (currentButton!= (Button)buttonSender)
            {
                DisableButtons();
                currentButton = (Button)buttonSender;
                currentButton.BackColor = GetColor();
                currentButton.ForeColor = Color.White;
                currentButton.Font = new System.Drawing.Font("Segoe UI Light", 14F);
                LogoPanel.BackColor = TitleBarPanel.BackColor = currentButton.BackColor;
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

        private void OpenForm(Form newForm, object buttonSender)
        {
            if (activeForm!= null)
            {
                activeForm.Close();
            }

            ActivateButton(buttonSender);
            activeForm = newForm;
            newForm.TopLevel = false;
            newForm.FormBorderStyle = FormBorderStyle.None;
            newForm.Dock = DockStyle.Fill;
            FormPanel.Controls.Add(newForm);
            FormPanel.Tag = newForm;
            newForm.BringToFront();
            newForm.Show();
            TitleLabel.Text = newForm.Text;
        }

        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void ProductsButton_Click(object sender, EventArgs e)
        {
            OpenForm(new ProductsForm(), sender);
        }

        private void CategoriesButton_Click(object sender, EventArgs e)
        {
            OpenForm(new CategoriesForm(), sender);
        }

        private void CustomersButton_Click(object sender, EventArgs e)
        {
            OpenForm(new CustomersForm(), sender);
        }

        private void BanksButton_Click(object sender, EventArgs e)
        {
            OpenForm(new BanksForm(), sender);
        }

        private void ConsignmentsButton_Click(object sender, EventArgs e)
        {
            OpenForm(new ConsignmentsForm(), sender);
        }
    }
}
