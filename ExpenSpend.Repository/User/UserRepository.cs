using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExpenSpend.Repository.User;

    
public class UserRepository : IUserRepository
{
    private readonly UserManager<Domain.Models.Users.User> _userManager;

    public UserRepository(UserManager<Domain.Models.Users.User> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<List<Domain.Models.Users.User>> GetAllUsersAsync()
    {
        return await _userManager.Users.ToListAsync();
    }
    
    public async Task<Domain.Models.Users.User?> GetUserByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<IdentityResult> UpdateUserAsync(Domain.Models.Users.User user)
    {
        
        return await _userManager.UpdateAsync(user);

    }
    
    public async Task<IdentityResult> DeleteUserAsync(Domain.Models.Users.User user)
    {
        return await _userManager.DeleteAsync(user);
    }
}