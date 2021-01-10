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
using System.Globalization;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Pharmacy
{
    /// <summary>
    /// Логика взаимодействия для RegistrationControl.xaml
    /// </summary>

    
    public partial class RegistrationControl : UserControl, INotifyPropertyChanged
    {
        User user;
        private bool isSignedUp = false;
        private bool isJustStarted = true;
        private bool isPassword = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsSignedUp { get => isSignedUp;
            set 
            {
                isSignedUp = value;
                NotifyPropertChanged();
            } 
        }

        public bool IsJustStarted { get => isJustStarted;
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
        public RegistrationControl()
        {
            InitializeComponent();
            user = new User();
            this.DataContext = user;
        }
        private Task<bool> validCreds()
        {
           
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            if (NotEmptyValidationRule.IsValidName && IsPhoneNumber.IsValidPhone && IsEmail.IsValidEmail && !string.IsNullOrWhiteSpace(passwordBox.Password) && passwordBox.Password == repeatPassword.Password)
            {
                tcs.SetResult(true);
            }
            else
            {
                tcs.SetResult(false);
            }
            
            return tcs.Task;
        }

        private async void openCB(object sender, MaterialDesignThemes.Wpf.DialogOpenedEventArgs eventArgs)
        {
            await Task.Delay(2000);
            
            IsSignedUp = await validCreds();
            
            if (IsSignedUp)
            {
                eventArgs.Session.Close();

                //using (ApplicationContext db = new ApplicationContext())
                //{
                //    User userRegistration = new User();
                //    userRegistration.UserName = UserName.Text;
                //    userRegistration.Email = email.Text;
                //    userRegistration.NumberPhone = numberPhone.Text;
                //    userRegistration.Password = passwordBox.Password;

                //    await db.Users.AddAsync(userRegistration);
                //    await db.SaveChangesAsync();
                //}
            }
            else
            {
                eventArgs.Session.Close(false);
            }
        }
        
        private void closingCB(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter != null)
            {
                if (((bool)eventArgs.Parameter) == true)
                {
                    IsJustStarted = false;
                    // Sign up succes
                    IsSignedUp = true;
                }
                else if (((bool)eventArgs.Parameter) == false)
                {
                    IsJustStarted = false;
                    // Sign up failed
                    IsSignedUp = false;
                }
            }
          
        }

        private void repeatPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password != repeatPassword.Password)
            {
                MaterialDesignThemes.Wpf.HintAssist.SetHint(repeatPassword, "Password doesn't match");
                isPassword = false;
            }
            else
            {
                MaterialDesignThemes.Wpf.HintAssist.SetHint(repeatPassword, "Repeat Password");
                isPassword = true;
            }
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password.Length < 5)
            {
                MaterialDesignThemes.Wpf.HintAssist.SetHint(passwordBox, "Must be than 5 chars");
                isPassword = false;
            }
            else
            {
                MaterialDesignThemes.Wpf.HintAssist.SetHint(passwordBox, "Password");
                isPassword = true;
            }
        }
    }
}