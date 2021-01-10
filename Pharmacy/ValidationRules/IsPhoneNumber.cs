using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Controls;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Pharmacy
{
    class IsPhoneNumber : ValidationRule
    {
        static public bool IsValidPhone { get; private set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!Regex.Match(((string)value), @"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$").Success )
            {
                IsValidPhone = false;
                return new ValidationResult(false, $"Incorrect number phone");
            }
            else
            {
                IsValidPhone = true;
            }

            return ValidationResult.ValidResult;
        }
    }
}
