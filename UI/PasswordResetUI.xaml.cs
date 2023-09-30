using System.Windows;
using Service;

namespace TheGardenGroupProject;

public partial class PasswordResetUI : Window
{
    public PasswordResetUI()
    {
        InitializeComponent();
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
        // LabelPasswordResetSuccess.Visibility = Visibility.Visible;
    }
    private void buttonTokenSubmit_Click(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void buttonTokenValidationBack_Click(object sender, RoutedEventArgs e)
    {

    }

    private void buttonPasswordChangeSubmit_Click(object sender, RoutedEventArgs e)
    {

    }

    private void buttonGoToLogin_Click(object sender, RoutedEventArgs e)
    {

    }

}