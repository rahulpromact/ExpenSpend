using ExpenSpend.Util.Models;

namespace ExpenSpend.Util.Services;

public interface IEmailService
{
    void SendEmail(Message email);
    Task<Message> CreateEmailValidationTemplateMessage(string email, string confirmationCode);
    Task<string> EmailConfirmationPageTemplate();
}
