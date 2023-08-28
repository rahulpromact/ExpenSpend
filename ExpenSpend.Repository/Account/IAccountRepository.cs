using ExpenSpend.Core.User;
using Microsoft.AspNetCore.Identity;
namespace ExpenSpend.Repository.Account;

public interface IAccountRepository
{
    /// <summary>
    /// Finds a user by their email address asynchronously.
    /// </summary>
    /// <param name="email">The email address to search for.</param>
    /// <returns>A task that, upon completion, returns a User object if found; otherwise, null.</returns>
    Task<Domain.Models.User?> FindByEmail(string email);

    /// <summary>
    /// Resets a user's password asynchronously.
    /// </summary>
    /// <param name="user">The user for whom to reset the password.</param>
    /// <param name="token">The token associated with the password reset request.</param>
    /// <param name="newPassword">The new password to set.</param>
    /// <returns>A task that represents the asynchronous password reset operation. The result indicates whether the operation was successful.</returns>
    Task<IdentityResult> ResetPasswordAsync(Domain.Models.User user, string token, string newPassword);

    /// <summary>
    /// Generates a password reset token for a user asynchronously.
    /// </summary>
    /// <param name="user">The user for whom to generate the token.</param>
    /// <returns>A task that, upon completion, returns a generated password reset token as a string.</returns>
    Task<string> GenerateResetToken(Domain.Models.User user);

}