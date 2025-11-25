using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentUniversal.Application.DTOs;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.Mappers;
using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Services;

public class UserService : IUserService
{
    private readonly List<User> _users = new();

    public Task<UserDTO?> AuthenticateAsync(string email, string password)
    {
        var user = _users.FirstOrDefault(u => u.Email == email);
        return Task.FromResult(user is null ? null : DTOMapper.ToDTO(user));
    }

    public Task<UserDTO> RegisterAsync(User user, string password)
    {
        _users.Add(user);
        return Task.FromResult(DTOMapper.ToDTO(user));
    }

    public Task<bool> VerifyIdentificationAsync(string userId, string identificationId)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId);
        if (user == null) return Task.FromResult(false);

        user.IdentificationId = identificationId;
        return Task.FromResult(true);
    }

    public Task<UserDTO?> GetUserByIdAsync(string id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        return Task.FromResult(user is null ? null : DTOMapper.ToDTO(user));
    }
}

