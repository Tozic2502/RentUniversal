using RentUniversal.Application.DTOs;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.Mapper;
using RentUniversal.Domain.Entities;
using BCrypt.Net;
using RentUniversal.Domain.Enums;

namespace RentUniversal.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDTO?> AuthenticateAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null) return null;

        bool passwordMatches = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        user.LastLogin = DateTime.UtcNow;
        if (!passwordMatches) return null;
        
        return DTOMapper.ToDTO(user);
    }



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
    public async Task<UserDTO?> UpdateUserAsync(string id, UserDTO updatedUser)
    {
        var existingUser = await _userRepository.GetByIdAsync(id);
        if (existingUser == null) return null;

        existingUser.Name = updatedUser.Name;
        existingUser.Email = updatedUser.Email;

        await _userRepository.UpdateAsync(existingUser);

        return DTOMapper.ToDTO(existingUser);
    }
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