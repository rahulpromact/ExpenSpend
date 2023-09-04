using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ExpenSpend.Repository.User;
public interface IUserRepository
{
    Task<List<Domain.Models.User>> GetAllUsersAsync();
    Task<Domain.Models.User?> GetUserByIdAsync(string id);
    Task<IdentityResult> UpdateUserAsync(Domain.Models.User user);
    Task<IdentityResult> DeleteUserAsync(Domain.Models.User user);
}