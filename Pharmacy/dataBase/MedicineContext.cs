using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Pharmacy
{
    class MedicineContext : DbContext
    {
        public DbSet<Medicine> Medicines { get; set; }
        public MedicineContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-QN31D90;Initial Catalog=PharmacyDB;Integrated Security=True;Pooling=False");
        }
    }
}
