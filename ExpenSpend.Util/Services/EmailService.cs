using ExpenSpend.Util.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace ExpenSpend.Util.Services;

public class EmailService : IEmailService
{
    private readonly EmailConfiguration _emailConfig;
    public EmailService(EmailConfiguration emailConfig)
    {
        _emailConfig = emailConfig;
    }
    public void SendEmail(Message email)
    {
        var emailMessage = CreateEmailMessage(email);
        MailSend(emailMessage);
    }

    private MimeMessage CreateEmailMessage(Message message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
        emailMessage.To.AddRange(message.To);
        emailMessage.Subject = message.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
        return emailMessage;
    }
    private async void MailSend(MimeMessage mailMessage)
    {
        using var client = new SmtpClient();
        try
        {
            client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(_emailConfig.UserName, _emailConfig.UserPassword);
            await client.SendAsync(mailMessage);
        }
        catch{
            // log here
            throw;
        }
        finally {
            client.Disconnect(true);
            client.Dispose(); 
        }
    }
}
