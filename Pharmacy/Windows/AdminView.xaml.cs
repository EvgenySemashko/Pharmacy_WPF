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
    /// Логика взаимодействия для AdminView.xaml
    /// </summary>
    public partial class AdminView : Window
    {
        List<Medicine> products = new List<Medicine>();
        public AdminView()
        {
            InitializeComponent();

            using (ApplicationContext db = new ApplicationContext())
            {
                productsView.ItemsSource = db.Medicines.ToList();
                products = db.Medicines.ToList();
            }
        }

        private void dragMe(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void productsView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Medicine product = new Medicine();

            product = products[productsView.SelectedIndex];

            UpdateDeleteWindow updateDelete = new UpdateDeleteWindow(product);
            updateDelete.Show();
            updateDelete.Closed += UpdateDelete_Closed;
        }

        private void UpdateDelete_Closed(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                productsView.ItemsSource = db.Medicines.ToList();
            }
        }


        private void insertButton_Click(object sender, RoutedEventArgs e)
        {
            InsertWindow window = new InsertWindow();

            window.Show();

            window.Closed += Window_Closed;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                productsView.ItemsSource = db.Medicines.ToList();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
