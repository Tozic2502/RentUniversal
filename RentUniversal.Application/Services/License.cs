using RentUniversal.Application.DTOs;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.Mappers;
using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Services;

public class LicenseService : ILicenseService
{
    private License? _license;

    public Task<LicenseDTO?> GetLicenseAsync()
    {
        return Task.FromResult(_license is null ? null : DTOMapper.ToDTO(_license));
    }

    public Task<LicenseDTO> UpdateLicenseAsync(License license)
    {
        _license = license;
        return Task.FromResult(DTOMapper.ToDTO(_license));
    }

    public bool IsLicenseValid(License license)
    {
        return license.ExpiryDate > DateTime.UtcNow;
    }
}
