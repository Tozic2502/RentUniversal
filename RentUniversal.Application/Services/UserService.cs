using RentUniversal.Application.DTOs;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.Mappers;
using RentUniversal.Domain.Entities;
using BCrypt.Net;

namespace RentUniversal.Application.Services;

/// <summary>
/// Application service responsible for user-related operations.
/// Handles authentication, registration, profile updates, and password changes.
/// </summary>
public class UserService : IUserService
{
    /// <summary>
    /// Repository used for user persistence and retrieval.
    /// </summary>
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Creates a new <see cref="UserService"/> instance.
    /// </summary>
    /// <param name="userRepository">Repository implementation for user storage.</param>
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Authenticates a user by email and password.
    /// Returns null if the user does not exist or the password is invalid.
    /// </summary>
    /// <param name="email">The email used to look up the user.</param>
    /// <param name="password">The plain-text password provided by the user.</param>
    /// <returns>The authenticated user as <see cref="UserDTO"/> or null.</returns>
    public async Task<UserDTO?> AuthenticateAsync(string email, string password)
    {
        // Look up the user by email.
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null)
            return null;

        // Verify the supplied password against the stored BCrypt hash.
        bool passwordMatches = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        if (!passwordMatches)
            return null;

        // Return a DTO to avoid exposing domain entity internals.
        return DTOMapper.ToDTO(user);
    }

    /// <summary>
    /// Registers a new user by hashing the provided password and saving the user.
    /// </summary>
    /// <param name="user">The domain user to create.</param>
    /// <param name="password">The initial plain-text password (will be hashed before storage).</param>
    /// <returns>The created user as <see cref="UserDTO"/>.</returns>
    public async Task<UserDTO> RegisterAsync(User user, string password)
    {
        // Hash the password before persisting the user (never store plain-text passwords).
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);

        await _userRepository.CreateAsync(user);

        // Return the created user as a DTO (without sensitive fields).
        return DTOMapper.ToDTO(user);
    }

    /// <summary>
    /// Stores a reference to an identification record for a user (verification step).
    /// Returns false if the user does not exist.
    /// </summary>
    /// <param name="userId">Identifier of the user to update.</param>
    /// <param name="identificationId">Identifier of the identification record to link.</param>
    /// <returns>True if the update succeeded; otherwise false.</returns>
    public async Task<bool> VerifyIdentificationAsync(string userId, string identificationId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            return false;

        // Link the identification record to the user.
        user.IdentificationId = identificationId;

        await _userRepository.UpdateAsync(user);
        return true;
    }

    /// <summary>
    /// Retrieves a user by id and maps it to a DTO.
    /// Returns null if the user does not exist.
    /// </summary>
    /// <param name="id">The user identifier.</param>
    /// <returns>A <see cref="UserDTO"/> or null.</returns>
    public async Task<UserDTO?> GetUserByIdAsync(string id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user == null ? null : DTOMapper.ToDTO(user);
    }

    /// <summary>
    /// Updates a user's basic profile fields (name and email).
    /// Returns null if the user does not exist.
    /// </summary>
    /// <param name="id">Identifier of the user to update.</param>
    /// <param name="updatedUser">DTO containing the updated values.</param>
    /// <returns>The updated <see cref="UserDTO"/> or null.</returns>
    public async Task<UserDTO?> UpdateUserAsync(string id, UserDTO updatedUser)
    {
        var existingUser = await _userRepository.GetByIdAsync(id);
        if (existingUser == null)
            return null;

        // Only update allowed fields (avoid blindly copying role/security fields from client input).
        existingUser.Name = updatedUser.Name;
        existingUser.Email = updatedUser.Email;

        await _userRepository.UpdateAsync(existingUser);

        return DTOMapper.ToDTO(existingUser);
    }

    /// <summary>
    /// Changes the user's password after verifying the old password.
    /// Returns false if the user does not exist or the old password is incorrect.
    /// </summary>
    /// <param name="id">Identifier of the user whose password should be changed.</param>
    /// <param name="oldPassword">The current password (must match the stored hash).</param>
    /// <param name="newPassword">The new password to set (will be hashed).</param>
    /// <returns>True if the password was changed; otherwise false.</returns>
    public async Task<bool> ChangePasswordAsync(string id, string oldPassword, string newPassword)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return false;

        // Verify the old password to prevent unauthorized password changes.
        if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.PasswordHash))
            return false;

        // Hash and store the new password.
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        await _userRepository.UpdateAsync(user);

        return true;
    }
}
