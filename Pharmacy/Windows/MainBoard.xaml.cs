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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls.Primitives;

namespace Pharmacy
{
    /// <summary>
    /// Логика взаимодействия для MainBoard.xaml
    /// </summary>
    public partial class MainBoard : Window, INotifyPropertyChanged
    {
        Medicine med;
        private bool userRights;
        private List<Medicine> items = new List<Medicine>();

        public event PropertyChangedEventHandler PropertyChanged;

        public bool UserRights
        {
            get => userRights;
            set
            {
                userRights = value;
                NotifyPropertChanged();
            }
        }

        private void NotifyPropertChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public MainBoard(string userName, bool userRights)
        {
            InitializeComponent();
            this.userRights = userRights;
            nameTextBlock.Text = userName;
            using (MedicineContext db = new MedicineContext())
            {
                 items = db.Medicines.ToList();
            }
            productsView.ItemsSource = items;
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

        private void searchBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                for (int i = 0; i < items.Count; i++)
                    if (items[i].Name_Medicine == searchBar.Text)
                        (productsView.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow).Background = Brushes.Aqua;
            }
        }

        private void productsView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            med = items[productsView.SelectedIndex];
            
            Dialog dialog = new Dialog(med);

            dialog.ShowDialog();
        }
    }
}
