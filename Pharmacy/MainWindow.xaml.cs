using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Threading;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pharmacy
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool isLogedIn = false;
        private bool isJustStarted = true;
        private bool isPassword = false;
        User user;

        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsLogedIn
        {
            get => isLogedIn;
            set
            {
                isLogedIn = value;
                NotifyPropertChanged();
            }
        }

        public bool IsJustStarted
        {
            get => isJustStarted;
            set
            {
                isJustStarted = value;
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
        public MainWindow()
        {
            InitializeComponent();
            user = new User();
            this.DataContext = user;
        }

        private Task<bool> validCreds()
        {

            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            if (NotEmptyValidationRule.IsValidName && isPassword)
            {
                tcs.SetResult(true);
            }
            else
            {
                tcs.SetResult(false);
            }

            return tcs.Task;
        }

        private Task<bool> UserIsExist(out bool userRights)
        {
            bool res = false;
            userRights = false;

            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
     
            using (ApplicationContext db = new ApplicationContext())
            {
                var checkUsers = db.Users.ToList();

                foreach (User user in checkUsers)
                {
                    if (user.UserName == NameTextBox.Text && user.Password == passwordBox.Password)
                    {
                        userRights = user.IsAdmin;
                        res = true;
                        break;
                    }
                    else
                    { 
                        res = false;
                    }
                }
            }

            tcs.SetResult(res);
            return tcs.Task;
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
        
        private async void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TranslateTransform translateTransform = new TranslateTransform();
            

            imgForTrans.RenderTransform = translateTransform;

            DoubleAnimation animX = new DoubleAnimation(0, 400, TimeSpan.FromSeconds(1));
            translateTransform.BeginAnimation(TranslateTransform.XProperty, animX);

            imageButt.Visibility = Visibility.Hidden;

            await Task.Delay(TimeSpan.FromSeconds(1));
            AddRegControl();          
        }

        private void AddRegControl()
        {
            RegistrationControl registrationControl = new RegistrationControl();

            GridFirst.Children.Add(registrationControl);
            Grid.SetColumn(registrationControl, 1);
            Grid.SetRow(registrationControl, 0);
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                MaterialDesignThemes.Wpf.HintAssist.SetHint(passwordBox, "Field is required");
                isPassword = false;
            }
            else
            {
                MaterialDesignThemes.Wpf.HintAssist.SetHint(passwordBox, "Password");
                isPassword = true;
            }
        }
       
        private async void openLB(object sender, MaterialDesignThemes.Wpf.DialogOpenedEventArgs eventArgs)
        {
            await Task.Delay(2000);
            bool isAdmin = false;

            IsLogedIn = await validCreds();
            IsLogedIn = await UserIsExist(out isAdmin);

            if (IsLogedIn)
            {
                eventArgs.Session.Close();
                MainBoard board = new MainBoard(NameTextBox.Text, isAdmin);
                this.Hide();
                board.Show();
            }
            else
            {
                if (!eventArgs.Session.IsEnded)
                {
                    eventArgs.Session.Close(false);
                }
            }
        }
        
        private void closingLB(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter != null)
            {
                if (((bool)eventArgs.Parameter) == true)
                {
                    IsJustStarted = false;
                    // Log in succes
                    IsLogedIn = true;
                }
                else if (((bool)eventArgs.Parameter) == false)
                {
                    IsJustStarted = false;
                    // Log In failed
                    IsLogedIn = false;
                }
            }
        }
    }
}
