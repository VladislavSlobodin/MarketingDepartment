using System.Windows.Forms;

namespace MarketingDepartment.ViewModels
{
    public class Parser
    {
        public static bool TryGetCellValueString(DataGridViewCell cell, out string value)
        {
            if (cell.Value != null)
            {
                value = cell.Value as string;
                return true;
            }
            else
            {
                value = string.Empty;
                return false;
            }
        }

        public static bool TryGetCellValueInt(DataGridViewCell cell, out int result)
        {
            if (cell.Value is int number)
            {
                result = number;
                return true;
            }
            return int.TryParse(cell.Value?.ToString() ?? "", out result);
        }

        public static bool TryGetCellValueFloat(DataGridViewCell cell, out float result)
        {
            if (cell.Value is float number)
            {
                result = number;
                return true;
            }
            return float.TryParse(cell.Value?.ToString() ?? "", out result);
        }

        public static bool TryGetCustomerType(DataGridViewCell cell, out bool type)
        {
            if (cell.Value != null)
            {
                type = cell.Value as string == "Юридическое лицо";
                return true;
            }
            else
            {
                type = false;
                return false;
            }
        }
    }
}
