using RentUniversal.Application.DTOs;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.Mapper;
using RentUniversal.Domain.Entities;
using BCrypt.Net;

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



    public async Task<UserDTO> RegisterAsync(User user, string password)
    {
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);

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
