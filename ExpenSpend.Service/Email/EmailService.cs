using ExpenSpend.Core.Email;
using ExpenSpend.Service.Email.Interface;
using MimeKit;
using MailKit.Net.Smtp;


namespace ExpenSpend.Service.Email;

public class EmailService : IEmailService
{
    private readonly EmailConfigurationDto _emailConfig;
    public EmailService(EmailConfigurationDto emailConfig)
    {
        _emailConfig = emailConfig;
    }
    public async void SendEmail(MessageDto email)
    {
        var emailMessage = CreateEmailMessage(email);
        MailSend(emailMessage);
    }

    public async Task<MessageDto> CreateEmailValidationTemplateMessage(string email, string confirmationCode)
    {
        string emailTemplateFileName = "..\\ExpenSpend.Domain.Shared\\Account\\EmailFormat.html";
        string emailBody;

        using (StreamReader reader = new StreamReader(emailTemplateFileName))
        {
            emailBody = await reader.ReadToEndAsync();
        }
        emailBody = emailBody.Replace("{confirmationCode}", confirmationCode);
        var subject = "ExpenSpend Account Confirmation";
        var emailMessage = new MessageDto(new[] { email! }, subject, emailBody);
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

    private MimeMessage CreateEmailMessage(MessageDto message)
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
        finally
        {
            client.Disconnect(true);
            client.Dispose();
        }
    }
    public void SendPasswordResetEmail(string recipientEmail, string resetLink)
    {
        var emailMessage = new MessageDto(new[] { recipientEmail }, "Forgot Password Link", resetLink);
        SendEmail(emailMessage);
    }
    public async void SandPasswordChangeNotification(string email, string userName)
    {
        string emailTemplateFileName = "..\\ExpenSpend.Domain.Shared\\Account\\PasswordChangeNotification.html";
        string emailBody;

        using (StreamReader reader = new StreamReader(emailTemplateFileName))
        {
            emailBody = await reader.ReadToEndAsync();
        }
        emailBody = emailBody.Replace("{userName}", userName);
        var subject = "Password Change Notification";
        var emailMessage = new MessageDto(new[] { email! }, subject, emailBody);
        SendEmail(emailMessage);
    }
}
