using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using DAL;
using Model;

namespace Service;

// Rafal (Individual functionality)
public class EmailService
{
    private User _user;
    private readonly EmailDao _emailDao;
    private readonly SmtpClient _smtpClient;
    private readonly UserService _userService;

    public EmailService()
    {
        _emailDao = new EmailDao();
        _smtpClient = GetSmtpClient();
        _userService = new UserService();
    }

    public void SendTemporaryPasswordByEmail(string emailAddress, string username, string password)
    {
        MailMessage mailMessage = GetMailMessage(emailAddress, username, password);

        try
        {
            // Send the email.
            _smtpClient.Send(mailMessage);
            Console.WriteLine("Email sent successfully!");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error sending email: {e.Message}");
        }
    }

    private MailMessage GetMailMessage(string recipientEmail, string username, string password)
    {
        // Create an email message.
        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress("thegardengroupinholland@gmail.com");
        mailMessage.To.Add(recipientEmail);
        mailMessage.Subject = "New Account's Password";
        mailMessage.Body =
            $"New user was created on The Garden Group application! This email stands as a reminder for your credentials:\n\nUsername: {username}\nPassword: {password}\n\nPlease note, this is temporary password and we ask you to change it as soon as possible using Password Reset Functionality in the login page of the app.\n\nIf you are not supposed to have account created on The Garden Group application, please ignore this email.";

        return mailMessage;
    }

    public void SendEmailConfirmation(string? username)
    {
        if (username != null) _user = GetUserByUsername(username);
        string token = GenerateResetToken();
        MailMessage mailMessage = GetMailMessage(_user.Email, token);

        try
        {
            // Send the email.
            _smtpClient.Send(mailMessage);
            Console.WriteLine("Email sent successfully!");

            // if email is sent successfully, add the token to the database
            _emailDao.AddPasswordResetToken(_user.Username, token);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error sending email: {e.Message}");
        }
    }

    private SmtpClient GetSmtpClient()
    {
        // Configure SMTP settings.
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
        smtpClient.Port = 587; // Specify the SMTP port you need.
        smtpClient.Credentials =
            new NetworkCredential("thegardengroupinholland@gmail.com",
                "hbts qzyg nyij pckr "); // the "App password" (secure connection)
        smtpClient.EnableSsl = true; // Use SSL for secure communication with the SMTP server.


        return smtpClient;
    }

    private MailMessage GetMailMessage(string recipientEmail, string token)
    {
        // Create an email message.
        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress("thegardengroupinholland@gmail.com");
        mailMessage.To.Add(recipientEmail);
        mailMessage.Subject = "Account Password Reset";
        mailMessage.Body =
            $"Please enter the provided token in the app to reset the password:\n\nToken: {token}\n\nIf you did not request a password reset, please ignore this email.";

        return mailMessage;
    }

    private static string GenerateResetToken()
    {
        byte[] randomBytes = new byte[8];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(randomBytes);
        }

        return Convert.ToBase64String(randomBytes);
    }

    private User GetUserByUsername(string username)
    {
        User user = _userService.GetUserByUsername(username);

        return user;
    }

    public bool ValidateUsername(string username)
    {
        List<User> userList = _userService.GetAllUsers();

        foreach (var user in userList)
        {
            if (user.Username == username)
            {
                return true;
            }
        }

        return false;
    }

    public bool ValidateResetToken(string inputToken)
    {
        string? dbToken = _emailDao.GetPasswordResetToken(_user.Username);

        if (inputToken == dbToken)
        {
            return true;
        }

        return false;
    }

    public void ChangePassword(string newPassword)
    {
        VerifyingLoginService verifyingLoginService = new VerifyingLoginService();
        string newPasswordHashed = verifyingLoginService.HashPassword(newPassword);

        _emailDao.ChangePassword(_user.Username, newPasswordHashed);
    }
}