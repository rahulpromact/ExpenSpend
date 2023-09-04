using ExpenSpend.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExpenSpend.Repository.User;

    
public class UserRepository : IUserRepository
{
    private readonly UserManager<Domain.Models.User> _userManager;

    public UserRepository(UserManager<Domain.Models.User> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<Domain.Models.User?> GetLoggedInUser(ClaimsPrincipal userPrincipal)
    {
        if (userPrincipal == null)
        {
            throw new ArgumentNullException(nameof(userPrincipal));
        }

        return await _userManager.GetUserAsync(userPrincipal);
    }
    public async Task<List<Domain.Models.User>> GetAllUsersAsync()
    {
        return await _userManager.Users.ToListAsync();
    }
    
    public async Task<Domain.Models.User?> GetUserByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }
    
    public async Task<IdentityResult> UpdateUserAsync(Domain.Models.User user)
    {
        
        return await _userManager.UpdateAsync(user);

    }
    
    public async Task<IdentityResult> DeleteUserAsync(Domain.Models.User user)
    {
        return await _userManager.DeleteAsync(user);
    }
}