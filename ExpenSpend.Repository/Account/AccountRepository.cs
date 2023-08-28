
using ExpenSpend.Domain.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExpenSpend.Repository.Account;

public class AccountRepository : IAccountRepository
{
    private readonly UserManager<Domain.Models.User> _userManager;
    private readonly SignInManager<Domain.Models.User> _signInManager;
    private readonly ExpenSpendDbContext _context;

    public AccountRepository(UserManager<Domain.Models.User> userManager, SignInManager<Domain.Models.User> signInManager, ExpenSpendDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }
    public async Task<IdentityResult> RegisterUserAsync(Domain.Models.User user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<SignInResult> LoginUserAsync(string email, string password)
    {
        return await _signInManager.PasswordSignInAsync(email, password, false, false);
    }
        
    public async Task LogoutUserAsync()
    {
        await _signInManager.SignOutAsync();
    }
    
    public async Task<Domain.Models.User?> FindByEmail(string email)
    {
        return  await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }
}