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
    /// Логика взаимодействия для UpdateDeleteWindow.xaml
    /// </summary>
    public partial class UpdateDeleteWindow : Window
    {
        Medicine product = new Medicine();

        public UpdateDeleteWindow(Medicine prod)
        {
            InitializeComponent();

            product = prod;
            SelectedProd.Text = prod.Name_Medicine;
            priceProduct.Text = prod.Price_Medicine.ToString();
        }

        private void countBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void priceProduct_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            product.Price_Medicine = decimal.Parse(priceProduct.Text);
            product.Count_Medicine = int.Parse(countBox.Text);

            using (ApplicationContext db = new ApplicationContext())
            {
                db.Medicines.Update(product);
                db.SaveChanges();
            }

            this.Close();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Medicines.Remove(product);
                db.SaveChanges();
            }
            this.Close();
        }
    }
}
