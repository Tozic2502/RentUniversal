using RentUniversal.Application.DTOs;
using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Interfaces;

/// <summary>
/// Responsible for authentication, registration, profile updates, and security-related actions.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Authenticates a user using email and password.
    /// Returns null if the credentials are invalid or the user does not exist.
    /// </summary>
    /// <param name="email">The user's email address.</param>
    /// <param name="password">The plain-text password provided by the user.</param>
    /// <returns>The authenticated user as <see cref="UserDTO"/> or null.</returns>
    Task<UserDTO?> AuthenticateAsync(string email, string password);

    /// <summary>
    /// Registers a new user in the system.
    /// Stores the user and securely handles the provided password (hashing/salting).
    /// </summary>
    /// <param name="user">The domain user to register.</param>
    /// <param name="password">The initial plain-text password (should never be stored as-is).</param>
    /// <returns>The created user as <see cref="UserDTO"/>.</returns>
    Task<UserDTO> RegisterAsync(User user, string password);

    /// <summary>
    /// Links and verifies a user's identification record (KYC verification).
    /// Returns true if verification succeeded; otherwise false.
    /// </summary>
    /// <param name="userId">Identifier of the user to verify.</param>
    /// <param name="identificationId">Identifier of the identification record.</param>
    /// <returns>True if verification succeeded; otherwise false.</returns>
    Task<bool> VerifyIdentificationAsync(string userId, string identificationId);

    /// <summary>
    /// Retrieves a user by identifier as a DTO.
    /// Returns null if the user does not exist.
    /// </summary>
    /// <param name="id">The user identifier.</param>
    /// <returns>The matching <see cref="UserDTO"/> or null.</returns>
    Task<UserDTO?> GetUserByIdAsync(string id);

    /// <summary>
    /// Updates a user's profile information.
    /// Returns null if the user does not exist, otherwise returns the updated user DTO.
    /// </summary>
    /// <param name="id">The user identifier.</param>
    /// <param name="updatedUser">The updated user data.</param>
    /// <returns>The updated <see cref="UserDTO"/> or null.</returns>
    Task<UserDTO?> UpdateUserAsync(string id, UserDTO updatedUser);

    /// <summary>
    /// Changes a user's password after validating the current password.
    /// Returns true if the password was changed; otherwise false (invalid old password or user not found).
    /// </summary>
    /// <param name="id">The user identifier.</param>
    /// <param name="oldPassword">The user's current password for verification.</param>
    /// <param name="newPassword">The new password to set.</param>
    /// <returns>True if the password change succeeded; otherwise false.</returns>
    Task<bool> ChangePasswordAsync(string id, string oldPassword, string newPassword);
}
