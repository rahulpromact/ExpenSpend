using ExpenSpend.Core.User;
using Microsoft.AspNetCore.Identity;

namespace ExpenSpend.Repository.Account;

public interface IAccountRepository
{
    Task<IdentityResult> RegisterUserAsync(CreateUserDto user);
    Task<SignInResult> LoginUserAsync(string username, string password);
    Task LogoutUserAsync();
}