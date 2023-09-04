
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExpenSpend.Domain.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ExpenSpend.Repository.Account;

public class AccountRepository : IAccountRepository
{
    private readonly UserManager<Domain.Models.User> _userManager;
    private readonly SignInManager<Domain.Models.User> _signInManager;
    private readonly ExpenSpendDbContext _context;
    private readonly IConfiguration _configuration;

    public AccountRepository(
        UserManager<Domain.Models.User> userManager, 
        SignInManager<Domain.Models.User> signInManager, 
        ExpenSpendDbContext context,
        IConfiguration configuration
        )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
        _configuration = configuration;
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

    public async Task<Domain.Models.User?> FindByUserNameAsync(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }

    public async Task<Domain.Models.User?> FindByEmail(string email)
    {
        return  await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<IdentityResult> ResetPasswordAsync(Domain.Models.User user, string token, string newPassword)
    {
        return await _userManager.ResetPasswordAsync(user, token, newPassword);
    }

    public async Task<string> GenerateEmailConfirmationTokenAsync(Domain.Models.User user)
    {
        return await _userManager.GenerateEmailConfirmationTokenAsync(user);
    }
    
    public async Task<IdentityResult> ConfirmEmailAsync(Domain.Models.User user, string token)
    {
        return await _userManager.ConfirmEmailAsync(user, token);
    }
    public async Task<string> GenerateResetToken(Domain.Models.User user)
    {
        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }
    public async Task<JwtSecurityToken> LoginUserJwtAsync(string userName, string password, bool rememberMe)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
        {
            return null!;
        }

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id),
            new("FirstName",user.FirstName),
            new(ClaimTypes.Surname, user.LastName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        authClaims.AddRange((await _userManager.GetRolesAsync(user)).Select(role => new Claim(ClaimTypes.Role, role)));
        var expirationTime = rememberMe ? DateTime.Now.AddDays(30) : DateTime.Now.AddHours(8);
        return GenerateTokenOptions(authClaims, expirationTime);
    }

    private JwtSecurityToken GenerateTokenOptions(List<Claim> authClaims, DateTime expires)
    {
        var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!);
        var tokenOptions = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    claims: authClaims,
                    expires: expires,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
        return tokenOptions;
    }
}