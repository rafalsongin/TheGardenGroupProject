using System.Windows;
using Service;
using UI;

namespace TheGardenGroupProject;

// Rafal
public partial class PasswordResetWindow : Window
{
    private readonly EmailService _emailService;

    public PasswordResetWindow()
    {
        InitializeComponent();
        _emailService = new EmailService();
    }

    private void buttonPasswordReset_Click(object sender, RoutedEventArgs e)
    {
        if (TextBoxPasswordResetUsername.Text == "")
        {
            LabelPasswordResetError.Visibility = Visibility.Visible;
            return;
        }

        if (!_emailService.ValidateUsername(TextBoxPasswordResetUsername.Text))
        {
            LabelPasswordResetError.Visibility = Visibility.Visible;
            return;
        }

        ShowGridTokenValidation();
        _emailService.SendEmailConfirmation(TextBoxPasswordResetUsername.Text);
    }

    private void buttonTokenSubmit_Click(object sender, RoutedEventArgs e)
    {
        string tokenInput = TextBoxTokenInput.Text;
        bool isTokenValid = _emailService.ValidateResetToken(tokenInput);

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

        _emailService.ChangePassword(newPassword);
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