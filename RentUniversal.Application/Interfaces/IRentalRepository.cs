using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Interfaces
{
    /// <summary>
    /// Repository abstraction for working with <see cref="Rental"/> entities.
    /// Provides data access methods for creating, reading, and updating rentals.
    /// </summary>
    public interface IRentalRepository
    {
        /// <summary>
        /// Retrieves a rental by its identifier.
        /// Returns null if the rental does not exist.
        /// </summary>
        /// <param name="id">The rental identifier.</param>
        /// <returns>The matching <see cref="Rental"/> or null.</returns>
        Task<Rental?> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all rentals for a specific user.
        /// </summary>
        /// <param name="userId">The user's identifier.</param>
        /// <returns>A collection of <see cref="Rental"/> records for the user.</returns>
        Task<IEnumerable<Rental>> GetByUserIdAsync(string userId);

        /// <summary>
        /// Creates a new rental in the data store.
        /// </summary>
        /// <param name="rental">The rental to create.</param>
        Task CreateAsync(Rental rental);

        /// <summary>
        /// Updates an existing rental in the data store.
        /// </summary>
        /// <param name="rental">The rental with updated values.</param>
        Task UpdateAsync(Rental rental);
    }
}