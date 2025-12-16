using RentUniversal.Application.DTOs;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.Mapper;
using RentUniversal.Domain.Entities;
using BCrypt.Net;
using RentUniversal.Domain.Enums;

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
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null) return null;

        bool passwordMatches = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        if (!passwordMatches) return null;

        user.LastLogin = DateTime.UtcNow;
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
        if (user.Name.Length < 2)
            throw new Exception("Name is too short.");

        if (!user.Email.Contains('@'))
            throw new Exception("Invalid email format.");

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        user.RegisteredDate = DateTime.UtcNow;

        if (user.PasswordHash.Length < 6)
            throw new Exception("Password must be at least 6 characters.");

        // Must be unique
        var existing = await _userRepository.GetByEmailAsync(user.Email);
        if (existing != null)
            throw new Exception("Email already registered");

        // Must be unique for IdentificationId if provided
        if (user.IdentificationId != 0)
        {
            var existingById = await _userRepository.GetByIdentificationIdAsync(user.IdentificationId);
            if (existingById != null)
                throw new Exception("Identification ID already registered");
        }

        await _userRepository.CreateAsync(user);

        return DTOMapper.ToDTO(user);
    }

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
        if (existingUser == null) return null;

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
        if (user == null) return false;

        // Check old password matches
        if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.PasswordHash))
            return false;

        // Hash new password & save
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        await _userRepository.UpdateAsync(user);

        return true;
    }
    
    public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(DTOMapper.ToDTO);
    }

    public async Task<bool> UpdateUserRoleAsync(string id, UserRole role)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return false;

        user.Role = role;

        await _userRepository.UpdateAsync(user);
        return true;
    }



}
