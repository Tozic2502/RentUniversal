using BCrypt.Net;
using RentUniversal.Application.DTOs;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.Mapper;
using RentUniversal.Domain.Entities;
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
        if (!passwordMatches) return null;

        return DTOMapper.ToDTO(user);
    }



    public async Task<UserDTO> RegisterAsync(CreateUserRequestDTO request)
    {
        // Business-rule validation (domain protection)
        if (request.Name.Length < 2)
            throw new Exception("Name is too short.");

        if (!request.Email.Contains('@'))
            throw new Exception("Invalid email format.");

        if (request.Password.Length < 6)
            throw new Exception("Password must be at least 6 characters.");

        // Must be unique
        var existing = await _userRepository.GetByEmailAsync(request.Email);
        if (existing != null)
            throw new Exception("Email already registered");

        // Hash password
        string hash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        // Create domain entity
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Name = request.Name,
            Email = request.Email,
            PasswordHash = hash,
            Role = UserRole.Customer
        };

        await _userRepository.CreateAsync(user);

        return DTOMapper.ToDTO(user);
    }

    public async Task<bool> VerifyIdentificationAsync(string userId, string identificationId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) return false;
        user.IdentificationId = identificationId;
        await _userRepository.UpdateAsync(user);
        return true;
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



}
