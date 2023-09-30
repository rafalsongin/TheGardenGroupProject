using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using DAL;
using Model;

namespace Service;

public class PasswordResetService
{
    private User _user;
    private readonly PasswordResetDao _passwordResetDao;
    
    // TODO: Add password reset by user functionality.
    // TODO: Add expiration date to password reset link. (use tokens?)
    public void ResetPassword(string? username)
    {
        _user = GetUserByUsername(username);
        SmtpClient smtpClient = GetSmtpClient();
        String token = GenerateResetToken();
        MailMessage mailMessage = GetMailMessage(_user.Email, token);
        
        try
        {
            // Send the email.
            smtpClient.Send(mailMessage);
            Console.WriteLine("Email sent successfully!");
            
            // if email is sent successfully, add the token to the database
            _passwordResetDao.AddPasswordResetToken(_user.Username, token);
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
        smtpClient.Credentials = new NetworkCredential("thegardengroupinholland@gmail.com", "hbts qzyg nyij pckr "); // the "App password" (secure connection)
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
        mailMessage.Body = $"Please enter the provided token in the app to reset the password:\n\nToken: {token}\n\nIf you did not request a password reset, please ignore this email.";

        return mailMessage;
    }

    private static string GenerateResetToken()
    {
        byte[] randomBytes = new byte[32];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(randomBytes);
        }
        return Convert.ToBase64String(randomBytes);
    }

    private User GetUserByUsername(string username)
    {
        UserService userService = new UserService();
        User user = userService.GetUserByUsername(username);
        
        return user;
    }

    public bool ValidateUsername(string username)
    {
        UserService userService = new UserService();
        List<User> userList = userService.GetAllUsers();

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
        string? dbToken = _passwordResetDao.GetPasswordResetToken(_user.Username);
        
        if (inputToken == dbToken)
        {
            return true;
        }
        
        return false;
    }
}