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
    /// Логика взаимодействия для MainBoard.xaml
    /// </summary>
    public partial class MainBoard : Window
    {
        private string userName;
        public MainBoard(string userName)
        {
            InitializeComponent();
            this.userName = userName;
          
            topBarControl.nameTextBlock.Text = userName;
        }

        public string UserName { get => userName; private set => userName = value; }

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
    }
}
