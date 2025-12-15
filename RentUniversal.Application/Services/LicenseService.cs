using RentUniversal.Application.DTOs;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.Mappers;
using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Services
{
    /// <summary>
    /// Application service responsible for license-related operations.
    /// Wraps repository access, maps domain entities to DTOs, and contains license validation rules.
    /// </summary>
    public class LicenseService : ILicenseService
    {
        /// <summary>
        /// Repository used for retrieving and persisting the license.
        /// </summary>
        private readonly ILicenseRepository _licenseRepository;

        /// <summary>
        /// Creates a new <see cref="LicenseService"/> instance.
        /// </summary>
        /// <param name="licenseRepository">Repository implementation for license storage.</param>
        public LicenseService(ILicenseRepository licenseRepository)
        {
            _licenseRepository = licenseRepository;
        }

        /// <summary>
        /// Retrieves the current license and maps it to a DTO.
        /// Returns null if no license exists.
        /// </summary>
        /// <returns>The current <see cref="LicenseDTO"/> or null.</returns>
        public async Task<LicenseDTO?> GetLicenseAsync()
        {
            var license = await _licenseRepository.GetAsync();

            // Null means the system has no stored license record yet.
            return license == null ? null : DTOMapper.ToDTO(license);
        }

        /// <summary>
        /// Updates/creates the license and returns the updated license as a DTO.
        /// Uses an upsert operation to avoid separate "create vs update" logic.
        /// </summary>
        /// <param name="license">The license to store.</param>
        /// <returns>The stored license as <see cref="LicenseDTO"/>.</returns>
        public async Task<LicenseDTO> UpdateLicenseAsync(License license)
        {
            await _licenseRepository.UpsertAsync(license);

            // Assumes the passed in instance represents the stored license state.
            return DTOMapper.ToDTO(license);
        }

        /// <summary>
        /// Checks whether the provided license is currently valid.
        /// Current rule: the license is valid if it has not expired (expiry date is in the future, UTC).
        /// </summary>
        /// <param name="license">The license to validate.</param>
        /// <returns>True if valid; otherwise false.</returns>
        public bool IsLicenseValid(License license)
        {
            // Use UTC to avoid timezone issues when comparing timestamps.
            return license.ExpiryDate > DateTime.UtcNow;
        }
    }
}
