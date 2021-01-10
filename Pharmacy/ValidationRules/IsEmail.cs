using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Pharmacy
{
    class IsEmail : ValidationRule
    {
        static public bool IsValidEmail { get; private set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!Regex.Match(((string)value), @"[^@ \t\r\n]+@[^@ \t\r\n]+\.[^@ \t\r\n]+").Success)
            {
                IsValidEmail = false;
                return new ValidationResult(false, $"Incorrect email");
            }
            else
            {
                IsValidEmail = true;
            }

            return ValidationResult.ValidResult;
        }
    }
}
