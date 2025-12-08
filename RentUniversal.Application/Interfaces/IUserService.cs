using RentUniversal.Application.DTOs;
using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Interfaces;

public interface IUserService
{
    Task<UserDTO?> AuthenticateAsync(string email, string password);
    Task<UserDTO> RegisterAsync(CreateUserRequestDTO request);
    Task<bool> VerifyIdentificationAsync(string userId, string identificationId);
    Task<UserDTO?> GetUserByIdAsync(string id);
    Task<UserDTO?> UpdateUserAsync(string id, UserDTO updatedUser);
    Task<bool> ChangePasswordAsync(string id, string oldPassword, string newPassword);
    Task<IEnumerable<UserDTO>> GetAllUsersAsync();

}
