
using Microsoft.AspNetCore.Identity;

namespace ExpenSpend.Repository.Account;

public class AccountRepository : IAccountRepository
{
    private readonly UserManager<Domain.Models.User> _userManager;
    private readonly SignInManager<Domain.Models.User> _signInManager;

    public AccountRepository(UserManager<Domain.Models.User> userManager, SignInManager<Domain.Models.User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
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
}