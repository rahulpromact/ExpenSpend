using Microsoft.AspNetCore.Identity;

namespace ExpenSpend.Domain.Models.Users;

public class User : IdentityUser
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
}