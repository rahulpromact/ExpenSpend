using Microsoft.AspNetCore.Identity;
namespace ExpenSpend.Repository.User;
public interface IUserRepository
{
    /// <summary>
    /// Retrieves a list of all users.
    /// </summary>
    /// <returns>Returns a list of all users.</returns>
    Task<List<Domain.Models.Users.User>> GetAllUsersAsync();

    /// <summary>
    /// Finds and retrieves a user based on the provided user ID.
    /// </summary>
    /// <param name="id">User's ID.</param>
    /// <returns>Returns the user if found, otherwise null.</returns>
    Task<Domain.Models.Users.User?> GetUserByIdAsync(string id);

    /// <summary>
    /// Updates the given user's details.
    /// </summary>
    /// <param name="user">The user model with updated details.</param>
    /// <returns>Returns an IdentityResult indicating the outcome of the update process.</returns>
    Task<IdentityResult> UpdateUserAsync(Domain.Models.Users.User user);

    /// <summary>
    /// Deletes the given user.
    /// </summary>
    /// <param name="user">The user model to be deleted.</param>
    /// <returns>Returns an IdentityResult indicating the outcome of the deletion process.</returns>
    Task<IdentityResult> DeleteUserAsync(Domain.Models.Users.User user);
}