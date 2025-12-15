using RentUniversal.Application.DTOs;
using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Interfaces;

/// <summary>
/// Service abstraction for license-related business logic.
/// Handles retrieving, updating, and validating the system's license.
/// </summary>
public interface ILicenseService
{
    /// <summary>
    /// Retrieves the current license as a DTO.
    /// Returns null if no license exists.
    /// </summary>
    /// <returns>The current <see cref="LicenseDTO"/> or null.</returns>
    Task<LicenseDTO?> GetLicenseAsync();

    /// <summary>
    /// Updates/creates the license and returns the updated license as a DTO.
    /// This method accepts a domain <see cref="License"/>. In some architectures,
    /// a dedicated DTO "LicenseUpdateDTO" is used to avoid passing domain entities across boundaries.
    /// </summary>
    /// <param name="license">The license to update/store.</param>
    /// <returns>The updated license as <see cref="LicenseDTO"/>.</returns>
    Task<LicenseDTO> UpdateLicenseAsync(License license);

    /// <summary>
    /// Validates whether a license is valid according to business rules
    /// </summary>
    /// <param name="license">The license to validate.</param>
    /// <returns>True if the license is valid; otherwise false.</returns>
    bool IsLicenseValid(License license);
}