using RentUniversal.Application.DTOs;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.Mappers;
using RentUniversal.Domain.Entities;

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

        // TODO: check password hash (Infrastructure should store PasswordHash)
        // For now just return DTO
        return DTOMapper.ToDTO(user);
    }

    public async Task<UserDTO> RegisterAsync(User user, string password)
    {
        // TODO: hash password and store password hash in domain or user entity
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
}
