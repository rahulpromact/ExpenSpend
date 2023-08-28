using ExpenSpend.Core.User;
using Microsoft.AspNetCore.Identity;
namespace ExpenSpend.Repository.User;
public interface IUserRepository
{
    /// <summary>
    /// Retrieves a list of all users asynchronously.
    /// </summary>
    /// <returns>A task that, upon completion, returns a list of User objects.</returns>
    Task<List<Domain.Models.User>> GetAllUsersAsync();

    /// <summary>
    /// Retrieves a user by their ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>A task that, upon completion, returns a User object if found; otherwise, null.</returns>
    Task<Domain.Models.User?> GetUserByIdAsync(string id);

    /// <summary>
    /// Updates a user's information asynchronously.
    /// </summary>
    /// <param name="user">The user object containing updated information.</param>
    /// <returns>A task that represents the asynchronous update operation. The result indicates whether the update was successful.</returns>
    Task<IdentityResult> UpdateUserAsync(Domain.Models.User user);

    /// <summary>
    /// Deletes a user asynchronously.
    /// </summary>
    /// <param name="user">The user to be deleted.</param>
    /// <returns>A task that represents the asynchronous delete operation. The result indicates whether the deletion was successful.</returns>
    Task<IdentityResult> DeleteUserAsync(Domain.Models.User user);

}