using ExpenSpend.Core.User;

namespace ExpenSpend.Repository.User;
using Microsoft.AspNetCore.Identity;

public interface IUserRepository
{
    Task<IdentityResult> RegisterUserAsync(CreateUserDto user);
    Task<SignInResult> LoginUserAsync(string username, string password);
    Task LogoutUserAsync();
}