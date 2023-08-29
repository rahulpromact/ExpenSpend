using System.ComponentModel.DataAnnotations;

namespace ExpenSpend.Core.Account;

public class ResetPasswordDto
{
    public string Password { get; set; } = null!;

    [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
    public string ConfirmPassword { get; set; } = null!;
    
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
}