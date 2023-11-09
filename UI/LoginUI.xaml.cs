using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DAL;
using Model;
using Service;
using TheGardenGroupProject;

namespace UI
{
    public partial class LoginUI : Window
    {
        private UserService _userService;

        private VerifyingLoginService _verifyingLoginService;

        public LoginUI()
        {
            InitializeComponent();
        }

        private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _userService = new UserService();
            _verifyingLoginService = new VerifyingLoginService();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (_verifyingLoginService.IsCorrectPassword(TextBoxUsername.Text, PasswordBox.Password))
            {
                User user = _userService.GetUserByUsername(TextBoxUsername.Text);
                
                if (user.UserType == UserType.ServiceDeskEmployee)
                {
                    ServiceDeskWindow serviceDeskWindow = new ServiceDeskWindow(user);
                    serviceDeskWindow.Show();
                }
                else
                {
                    CompanyEmployeeWindow companyEmployeeWindow = new CompanyEmployeeWindow();
                    companyEmployeeWindow.Show();
                }
                
                this.Close();
            }
            else
            {
                LabelPasswordResetSuccess.Visibility = Visibility.Hidden;
                LabelIncorrectCredentials.Visibility = Visibility.Visible;
            }
        }

        private void labelForgotLoginDetails_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GridLoginPage.Visibility = Visibility.Hidden;
            
            PasswordResetWindow passwordResetWindow = new PasswordResetWindow();
            passwordResetWindow.Show();
            this.Close();
        }
    }
}