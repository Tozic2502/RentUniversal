using RentUniversal.Application.DTOs;
using RentUniversal.Domain.Entities;
using RentUniversal.Domain.Enums;

namespace RentUniversal.Application.Interfaces;

/// <summary>
/// Defines the contract for user-related operations such as authentication, registration, profile management, 
/// and security actions within the application.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Authenticates a user using their email and password.
    /// </summary>
    /// <param name="email">The email address of the user attempting to authenticate.</param>
    /// <param name="password">The plain-text password provided by the user.</param>
    /// <returns>
    /// A <see cref="UserDTO"/> representing the authenticated user if the credentials are valid; otherwise, null.
    /// </returns>
    Task<UserDTO?> AuthenticateAsync(string email, string password);

    /// <summary>
    /// Registers a new user in the system with the provided details and password.
    /// </summary>
    /// <param name="user">The <see cref="User"/> entity containing the user's details.</param>
    /// <param name="password">The plain-text password to be securely stored (hashed and salted).</param>
    /// <returns>
    /// A <see cref="UserDTO"/> representing the newly created user.
    /// </returns>
    Task<UserDTO> RegisterAsync(User user, string password);

    /// <summary>
    /// Retrieves a user's details by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>
    /// A <see cref="UserDTO"/> representing the user if found; otherwise, null.
    /// </returns>
    Task<UserDTO?> GetUserByIdAsync(string id);

    /// <summary>
    /// Updates the profile information of an existing user.
    /// </summary>
    /// <param name="id">The unique identifier of the user to update.</param>
    /// <param name="updatedUser">A <see cref="UserDTO"/> containing the updated user details.</param>
    /// <returns>
    /// A <see cref="UserDTO"/> representing the updated user if the operation succeeds; otherwise, null.
    /// </returns>
    Task<UserDTO?> UpdateUserAsync(string id, UserDTO updatedUser);

    /// <summary>
    /// Changes the password of an existing user after validating their current password.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="oldPassword">The user's current password for verification.</param>
    /// <param name="newPassword">The new password to set for the user.</param>
    /// <returns>
    /// True if the password change is successful; otherwise, false (e.g., invalid old password or user not found).
    /// </returns>
    Task<bool> ChangePasswordAsync(string id, string oldPassword, string newPassword);

    /// <summary>
    /// Retrieves a list of all users in the system.
    /// </summary>
    /// <returns>
    /// An enumerable collection of <see cref="UserDTO"/> representing all users.
    /// </returns>
    Task<IEnumerable<UserDTO>> GetAllUsersAsync();

    /// <summary>
    /// Updates the role of a user in the system.
    /// </summary>
    /// <param name="id">The unique identifier of the user whose role is to be updated.</param>
    /// <param name="role">The new <see cref="UserRole"/> to assign to the user.</param>
    /// <returns>
    /// True if the role update is successful; otherwise, false.
    /// </returns>
    Task<bool> UpdateUserRoleAsync(string id, UserRole role);
}
