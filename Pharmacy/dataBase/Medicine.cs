using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy
{
    class Medicine
    {
        [Key]
        public int ID_Medicine { get; set; }
        public string Name_Medicine { get; set; }
        public decimal Price_Medicine { get; set; }
        public int Count_Medicine { get; set; }
        public string Location { get; set; }
    }
}
