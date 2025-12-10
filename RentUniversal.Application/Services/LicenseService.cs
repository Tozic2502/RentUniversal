using RentUniversal.Application.DTOs;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.Mapper;
using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Services
{
    public class LicenseService : ILicenseService
    {
        private readonly ILicenseRepository _licenseRepository;

        public LicenseService(ILicenseRepository licenseRepository)
        {
            _licenseRepository = licenseRepository;
        }

        public async Task<LicenseDTO?> GetLicenseAsync()
        {
            var license = await _licenseRepository.GetAsync();
            return license == null ? null : DTOMapper.ToDTO(license);
        }

        public async Task<LicenseDTO> UpdateLicenseAsync(License license)
        {
            await _licenseRepository.UpsertAsync(license);
            return DTOMapper.ToDTO(license);
        }

        public bool IsLicenseValid(License license)
        {
            return license.ExpiryDate > DateTime.UtcNow;
        }
    }
}