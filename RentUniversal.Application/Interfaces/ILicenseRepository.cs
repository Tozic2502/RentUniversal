using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Interfaces
{
    /// <summary>
    /// Repository abstraction for working with the system's <see cref="License"/> entity.
    /// Provides access to the current license and allows creating/updating it.
    /// </summary>
    public interface ILicenseRepository
    {
        /// <summary>
        /// Retrieves the current license record.
        /// Returns null if no license is stored yet.
        /// </summary>
        /// <returns>The current <see cref="License"/> or null.</returns>
        Task<License?> GetAsync();

        /// <summary>
        /// Inserts the license if it does not exist, otherwise updates the existing license.
        /// </summary>
        /// <param name="license">The license to insert or update.</param>
        Task UpsertAsync(License license);
    }
}