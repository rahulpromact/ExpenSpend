using ExpenSpend.Util.Models;

namespace ExpenSpend.Util.Services;

public interface IEmailService
{
    void SendEmail(Message email);
}
