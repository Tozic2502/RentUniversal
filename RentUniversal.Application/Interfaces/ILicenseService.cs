using RentUniversal.Application.DTOs;
using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Interfaces;

public interface ILicenseService
{
    Task<LicenseDTO?> GetLicenseAsync();
    Task<LicenseDTO> UpdateLicenseAsync(License license);
    bool IsLicenseValid(License license);
}
