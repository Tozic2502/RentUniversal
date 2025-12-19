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
        // Retrieve the user by email
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null) return null;

        // Verify the provided password against the stored hash
        bool passwordMatches = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        user.LastLogin = DateTime.UtcNow;
        if (!passwordMatches) return null;
        
        // Map the domain user to a DTO and return
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
        // Validate user name length
        if (user.Name.Length < 2)
            throw new Exception("Name is too short.");

        // Validate email format
        if (!user.Email.Contains('@'))
            throw new Exception("Invalid email format.");

        // Hash the password and set registration date
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        user.RegisteredDate = DateTime.UtcNow;

        // Ensure password hash length is valid
        if (user.PasswordHash.Length < 6)
            throw new Exception("Password must be at least 6 characters.");

        // Check if email is already registered
        var existing = await _userRepository.GetByEmailAsync(user.Email);
        if (existing != null)
            throw new Exception("Email already registered");

        // Check if IdentificationId is unique (if provided)
        if (user.IdentificationId != 0)
        {
            var existingById = await _userRepository.GetByIdentificationIdAsync(user.IdentificationId);
            if (existingById != null)
                throw new Exception("Identification ID already registered");
        }

        // Save the new user to the repository
        await _userRepository.CreateAsync(user);

        // Map the domain user to a DTO and return
        return DTOMapper.ToDTO(user);
    }

    /// <summary>
    /// Retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>The user as <see cref="UserDTO"/> or null if not found.</returns>
    public async Task<UserDTO?> GetUserByIdAsync(string id)
    {
        // Retrieve the user by ID
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
        // Retrieve the existing user by ID
        var existingUser = await _userRepository.GetByIdAsync(id);
        if (existingUser == null) return null;

        // Update the user's name and email
        existingUser.Name = updatedUser.Name;
        existingUser.Email = updatedUser.Email;

        // Save the updated user to the repository
        await _userRepository.UpdateAsync(existingUser);

        // Map the updated user to a DTO and return
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
        // Retrieve the user by ID
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return false;

        // Verify the old password matches the stored hash
        if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.PasswordHash))
            return false;

        // Hash the new password and save it
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        await _userRepository.UpdateAsync(user);

        return true;
    }
    
    /// <summary>
    /// Retrieves all users from the repository.
    /// </summary>
    /// <returns>A collection of users as <see cref="UserDTO"/>.</returns>
    public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
    {
        // Retrieve all users and map them to DTOs
        var users = await _userRepository.GetAllAsync();
        return users.Select(DTOMapper.ToDTO);
    }

    /// <summary>
    /// Updates the role of a user.
    /// Returns false if the user does not exist.
    /// </summary>
    /// <param name="id">Identifier of the user whose role should be updated.</param>
    /// <param name="role">The new role to assign to the user.</param>
    /// <returns>True if the role was updated; otherwise false.</returns>
    public async Task<bool> UpdateUserRoleAsync(string id, UserRole role)
    {
        // Retrieve the user by ID
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return false;

        // Update the user's role
        user.Role = role;

        // Save the updated user to the repository
        await _userRepository.UpdateAsync(user);
        return true;
    }
}
