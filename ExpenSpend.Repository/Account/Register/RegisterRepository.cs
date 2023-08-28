using Microsoft.AspNetCore.Identity;

namespace ExpenSpend.Repository.Account.Register;

public class RegisterRepository : IRegistorRepository
{
    private readonly UserManager<Domain.Models.User> _userManager;

    public RegisterRepository(UserManager<Domain.Models.User> userManager)
    {
        _userManager = userManager;
    }
    public async Task<IdentityResult> RegisterUserAsync(Domain.Models.User user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<string> GenerateEmailTokenAsync(Domain.Models.User user)
    {
        return await _userManager.GenerateEmailConfirmationTokenAsync(user);
    }

    public async Task<IdentityResult> ConfirmEmailAsync(Domain.Models.User user, string token)
    {
        return await _userManager.ConfirmEmailAsync(user, token);
    }
}