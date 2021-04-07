using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Pharmacy
{
    class validationRules
    {
        public static bool IsPhoneNumber(string numberPhone)
        {
            return Regex.Match(numberPhone, @"^(\+[0-9]{9})$").Success;
        }
    }
}
