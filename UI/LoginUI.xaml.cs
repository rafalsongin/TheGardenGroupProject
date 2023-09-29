using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Service;
using TheGardenGroupProject;

namespace UI
{
    public partial class LoginUI : Window
    {
        public LoginUI()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            VerifyingLoginService verifyingLoginService = new VerifyingLoginService();
            if (verifyingLoginService.IsCorrectPassword(TextBoxUsername.Text, PasswordBox.Password))
            {
                DashboardUI dashboardWindow = new DashboardUI(TextBoxUsername.Text);
                dashboardWindow.Show();
            }
            else
            {
                LabelIncorrectCredentials.Visibility = Visibility.Visible;
            }
        }

        private void textBoxUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
 
        }

        private void labelForgotLoginDetails_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Forgot details");
        }
    }
}