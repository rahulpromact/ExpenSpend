using Microsoft.AspNetCore.Identity;

namespace ExpenSpend.Repository.Account.Login;

public interface ILoginRepository
{
    /// <summary>
    /// Asynchronously attempts to log in a user with the provided username and password.
    /// </summary>
    /// <param name="username">The username of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a SignInResult indicating the outcome of the login attempt.</returns>
    Task<SignInResult> LoginUserAsync(string username, string password);

    /// <summary>
    /// Asynchronously logs out the currently authenticated user.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task LogoutUserAsync();
}