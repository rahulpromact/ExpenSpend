using ExpenSpend.Core.User;
using Microsoft.AspNetCore.Identity;
namespace ExpenSpend.Repository.Account;

public interface IAccountRepository
{
    Task<IdentityResult> RegisterUserAsync(Domain.Models.User user, string password);
    Task<SignInResult> LoginUserAsync(string username, string password);
    Task LogoutUserAsync();
    Task<Domain.Models.User?> FindByEmail(string email);
}