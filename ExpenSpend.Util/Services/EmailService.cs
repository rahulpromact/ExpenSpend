using ExpenSpend.Util.Models;
using MailKit.Net.Smtp;
using MimeKit;
using Org.BouncyCastle.Tls;

namespace ExpenSpend.Util.Services;

public class EmailService : IEmailService
{
    private readonly EmailConfiguration _emailConfig;
    public EmailService(EmailConfiguration emailConfig)
    {
        _emailConfig = emailConfig;
    }
    public async void SendEmail(Message email)
    {
        var emailMessage = CreateEmailMessage(email);
        MailSend(emailMessage);
    }
    
    public async Task<Message> CreateEmailValidationTemplateMessage(string email, string confirmationCode)
    {
        string emailTemplateFileName = "..\\ExpenSpend.Domain.Shared\\Account\\EmailFormat.html";
        string emailBody;

        using (StreamReader reader = new StreamReader(emailTemplateFileName))
        {
            emailBody = await reader.ReadToEndAsync();
        }
        emailBody = emailBody.Replace("{confirmationCode}", confirmationCode);
        var subject = "ExpenSpend Account Confirmation";
        var emailMessage = new Message(new[] { email! }, subject, emailBody);
        return emailMessage;
    }
    
    public async Task<string> EmailConfirmationPageTemplate()
    {
        string emailTemplateFileName = "..\\ExpenSpend.Domain.Shared\\Account\\EmailConfResponse.html";
        string htmlBody;

        using (StreamReader reader = new StreamReader(emailTemplateFileName))
        {
            htmlBody = await reader.ReadToEndAsync();
        }
        return htmlBody;
    }
    
    private MimeMessage CreateEmailMessage(Message message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
        emailMessage.To.AddRange(message.To);
        emailMessage.Subject = message.Subject;

        // Create the HTML part of the email
        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = message.Content; // Set the HTML content here

        // Attach the HTML part to the email message
        emailMessage.Body = bodyBuilder.ToMessageBody();

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
