using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Pharmacy
{
    class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NumberPhone { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}

