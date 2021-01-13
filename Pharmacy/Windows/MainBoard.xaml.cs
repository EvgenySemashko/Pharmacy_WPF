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

        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
