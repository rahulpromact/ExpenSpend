using ExpenSpend.Core.User;
using Microsoft.AspNetCore.Identity;
namespace ExpenSpend.Repository.User;
public interface IUserRepository
{
    Task<IdentityResult> RegisterUserAsync(CreateUserDto user);
    Task<SignInResult> LoginUserAsync(string username, string password);
    Task LogoutUserAsync();
}