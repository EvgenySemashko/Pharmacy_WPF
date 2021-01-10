using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.ComponentModel;
using System.Globalization;

namespace Pharmacy
{
    public class NotEmptyValidationRule : ValidationRule
    {
        static public bool IsValidName { get; private set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace((((string)value) ?? "")))
            {
                IsValidName = false;
                return new ValidationResult(false, "Field is required");
            }
            else
            {
                IsValidName = true;
            }

            return ValidationResult.ValidResult;
        }
    }
}
