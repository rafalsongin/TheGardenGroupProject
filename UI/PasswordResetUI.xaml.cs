using System.Windows;
using Service;
using UI;

namespace TheGardenGroupProject;

public partial class PasswordResetUI : Window
{
    private readonly PasswordResetService _passwordResetService;
    
    public PasswordResetUI()
    {
        InitializeComponent();
        _passwordResetService = new PasswordResetService();
    }

    private void buttonPasswordReset_Click(object sender, RoutedEventArgs e)
    {
        
        if (TextBoxPasswordResetUsername.Text == "")
        {
            LabelPasswordResetError.Visibility = Visibility.Visible;
            return;
        }
            
        if (!_passwordResetService.ValidateUsername(TextBoxPasswordResetUsername.Text))
        {
            LabelPasswordResetError.Visibility = Visibility.Visible;
            return;
        }

        ShowGridTokenValidation();
        _passwordResetService.SendEmailConfirmation(TextBoxPasswordResetUsername.Text);
    }

    private void buttonTokenSubmit_Click(object sender, RoutedEventArgs e)
    {
        string tokenInput = TextBoxTokenInput.Text;
        bool isTokenValid = _passwordResetService.ValidateResetToken(tokenInput);

        if (isTokenValid)
        {
            ShowGridPasswordChange();
        }
        else
        {
            LabelTokenValidationError.Visibility = Visibility.Visible;
        }
    }

    private void buttonTokenValidationBack_Click(object sender, RoutedEventArgs e)
    {
        ShowGridPasswordResetPage();
    }

    private void buttonPasswordChangeSubmit_Click(object sender, RoutedEventArgs e)
    {
        string newPassword = PasswordBoxNewPassword.Password;
        string newPasswordConfirmation = PasswordBoxNewPasswordRepeat.Password;

        if (newPassword != newPasswordConfirmation)
        {
            LabelPasswordsDontMatchError.Visibility = Visibility.Visible;
            return;
        }

        _passwordResetService.ChangePassword(newPassword);
        ShowInfoPasswordChangeSuccess();
    }

    private void ShowInfoPasswordChangeSuccess()
    {
        LabelPasswordResetSuccess.Visibility = Visibility.Visible;
        PasswordBoxNewPassword.IsEnabled = false;
        PasswordBoxNewPasswordRepeat.IsEnabled = false;
        ButtonGoToLoginPage.Visibility = Visibility.Visible;
        ButtonPasswordChangeGoBack.Visibility = Visibility.Hidden;
    }

    private void ButtonPasswordChangeGoBack_Click(object sender, RoutedEventArgs e)
    {
        ShowGridTokenValidation();
    }

    private void buttonGoToLoginPage_Click(object sender, RoutedEventArgs e)
    {
        LoginUI loginWindow = new LoginUI();
        loginWindow.Show();
        this.Close();
    }
    
    private void ShowGridPasswordResetPage()
    {
        GridTokenValidation.Visibility = Visibility.Hidden;
        GridPasswordChange.Visibility = Visibility.Hidden;
    }
    
    private void ShowGridTokenValidation()
    {
        GridTokenValidation.Visibility = Visibility.Visible;
        GridPasswordChange.Visibility = Visibility.Hidden;
    }
    
    private void ShowGridPasswordChange()
    {
        GridTokenValidation.Visibility = Visibility.Hidden;
        GridPasswordChange.Visibility = Visibility.Visible;
    }
}