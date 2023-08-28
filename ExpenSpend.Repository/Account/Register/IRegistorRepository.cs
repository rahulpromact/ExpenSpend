using Microsoft.AspNetCore.Identity;

namespace ExpenSpend.Repository.Account.Register;

public interface IRegistorRepository
{
    /// <summary>
    /// Registers a user asynchronously with the provided user information and password.
    /// </summary>
    /// <param name="user">The user object containing registration details.</param>
    /// <param name="password">The password for the new user.</param>
    /// <returns>A task that represents the asynchronous registration operation. The result indicates whether the registration was successful.</returns>
    Task<IdentityResult> RegisterUserAsync(Domain.Models.User user, string password);

    /// <summary>
    /// Generates an email confirmation token for a user asynchronously.
    /// </summary>
    /// <param name="user">The user for whom to generate the email confirmation token.</param>
    /// <returns>A task that, upon completion, returns a generated email confirmation token as a string.</returns>
    Task<string> GenerateEmailTokenAsync(Domain.Models.User user);

    /// <summary>
    /// Confirms a user's email address asynchronously using a token.
    /// </summary>
    /// <param name="user">The user whose email address is to be confirmed.</param>
    /// <param name="token">The token associated with the email confirmation request.</param>
    /// <returns>A task that represents the asynchronous email confirmation operation. The result indicates whether the confirmation was successful.</returns>
    Task<IdentityResult> ConfirmEmailAsync(Domain.Models.User user, string token);

}