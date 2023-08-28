using ExpenSpend.Domain.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExpenSpend.Repository.Account;

public class AccountRepository : IAccountRepository
{
    private readonly UserManager<Domain.Models.User> _userManager;
    private readonly ExpenSpendDbContext _context;

    public AccountRepository(UserManager<Domain.Models.User> userManager, ExpenSpendDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }
    
    
    public async Task<Domain.Models.User?> FindByEmail(string email)
    {
        return  await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<IdentityResult> ResetPasswordAsync(Domain.Models.User user, string token, string newPassword)
    {
        return await _userManager.ResetPasswordAsync(user, token, newPassword);
    }

    public async Task<string> GenerateResetToken(Domain.Models.User user)
    {
        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }
}