using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pharmacy
{
    /// <summary>
    /// Логика взаимодействия для InsertWindow.xaml
    /// </summary>
    public partial class InsertWindow : Window
    {
        Medicine prod = new Medicine();
        List<Medicine> items = new List<Medicine>();

        public InsertWindow()
        {
            InitializeComponent();
        }

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {

            if (items.Count == 0)
            {
                prod.ID_Medicine = items.Count;
            }
            else
            {
                prod.ID_Medicine = items.Count + 1;
            }

            prod.Name_Medicine = nameBox.Text;

            if (nameBox.Text.Length == 0)
            {
               prod.Name_Medicine = "";
            }
            else
            {
               prod.Name_Medicine = nameBox.Text;
            }

            if (priceProduct.Text.Length == 0)
            {
                prod.Price_Medicine = 0;
            }
            else
            {
                prod.Price_Medicine = decimal.Parse(priceProduct.Text);
            }

            if (countBox.Text.Length == 0)
            {
                prod.Count_Medicine = 0;
            }
            else
            {
                prod.Count_Medicine = int.Parse(countBox.Text);
            }

            using (ApplicationContext db = new ApplicationContext())
            {
                db.Medicines.Update(prod);
                db.SaveChanges();
            }
            this.Close();
        }

        private void priceProduct_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void countBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
