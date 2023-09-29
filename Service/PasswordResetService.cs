using System.Net;
using System.Net.Mail;
using Model;

namespace Service;

public class PasswordResetService
{
    // TODO: Add password reset by user functionality.
    // TODO: Add expiration date to password reset link. (use tokens?)
    public void ResetPassword(string? username)
    {
        User user = GetUserByUsername(username);
        SmtpClient smtpClient = GetSmtpClient();
        MailMessage mailMessage = GetMailMessage(user.Email);
        
        try
        {
            // Send the email.
            smtpClient.Send(mailMessage);
            Console.WriteLine("Email sent successfully!");
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

    private MailMessage GetMailMessage(string recipientEmail)
    {
        // Create an email message.
        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress("thegardengroupinholland@gmail.com");
        mailMessage.To.Add(recipientEmail);
        mailMessage.Subject = "Test Email";
        mailMessage.Body = "This is a test email sent from C#.";

        return mailMessage;
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
}