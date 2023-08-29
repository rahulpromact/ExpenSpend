using ExpenSpend.Core.User;
using Microsoft.AspNetCore.Identity;
namespace ExpenSpend.Repository.Account;

public interface IAccountRepository
{
    Task<IdentityResult> RegisterUserAsync(Domain.Models.User user, string password);
    Task<SignInResult> LoginUserAsync(string username, string password);
    Task LogoutUserAsync();
    Task<Domain.Models.User?> FindByEmail(string email);
    Task<IdentityResult> ResetPasswordAsync(Domain.Models.User user, string token, string newPassword);
    Task<string> GenerateEmailConfirmationTokenAsync(Domain.Models.User user);
    Task<IdentityResult> ConfirmEmailAsync(Domain.Models.User user, string token);
    Task<string> GenerateResetToken(Domain.Models.User user);
}