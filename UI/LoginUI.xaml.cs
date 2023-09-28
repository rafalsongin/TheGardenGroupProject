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
            if (verifyingLoginService.IsCorrectPassword(TextBoxUsername.Text, TextBoxPassword.Text))
            {
                DashboardUI dashboardWindow = new DashboardUI();
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

        private void textBoxPassword_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void labelForgotLoginDetails_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Forgot details");
        }
    }
}