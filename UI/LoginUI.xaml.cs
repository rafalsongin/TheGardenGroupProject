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
                this.Close();
            }
            else
            {
                LabelPasswordResetSuccess.Visibility = Visibility.Hidden;
                LabelIncorrectCredentials.Visibility = Visibility.Visible;
            }
        }

        private void textBoxUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
 
        }

        private void labelForgotLoginDetails_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // GridLoginPage.Visibility = Visibility.Hidden;
            GridPasswordResetPage.Visibility = Visibility.Visible;
        }

        private void buttonPasswordResetBack_Click(object sender, RoutedEventArgs e)
        {
            GridPasswordResetPage.Visibility = Visibility.Hidden;
        }

        private void buttonPasswordReset_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxPasswordResetUsername.Text == "")
            {
                LabelPasswordResetError.Visibility = Visibility.Visible;
                return;
            }
            
            PasswordResetService passwordResetService = new PasswordResetService();
            if (!passwordResetService.ValidateUsername(TextBoxPasswordResetUsername.Text))
            {
                LabelPasswordResetError.Visibility = Visibility.Visible;
                return;
            }
            passwordResetService.ResetPassword(TextBoxPasswordResetUsername.Text);

            GridPasswordResetPage.Visibility = Visibility.Hidden;
            LabelPasswordResetSuccess.Visibility = Visibility.Visible;
        }
    }
}