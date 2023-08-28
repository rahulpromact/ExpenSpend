using Microsoft.AspNetCore.Identity;

namespace ExpenSpend.Repository.Account.Login;

public class LoginRepository : ILoginRepository
{
    private readonly SignInManager<Domain.Models.User> _signInManager;
    public LoginRepository(SignInManager<Domain.Models.User> signInManager)
    {
        _signInManager = signInManager;
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