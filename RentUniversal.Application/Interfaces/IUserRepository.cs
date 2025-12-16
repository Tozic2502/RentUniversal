using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Interfaces
{
    /// <summary>
    /// Repository abstraction for working with <see cref="User"/> entities.
    /// Provides data access methods for creating, reading, and updating users.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves a user by its identifier.
        /// Returns null if the user does not exist.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>The matching <see cref="User"/> or null.</returns>
        Task<User?> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves a user by email address.
        /// Returns null if no user exists with the provided email.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <returns>The matching <see cref="User"/> or null.</returns>
        Task<User?> GetByEmailAsync(string email);

        /// <summary>
        /// Creates a new user in the data store.
        /// </summary>
        /// <param name="user">The user to create.</param>
        Task CreateAsync(User user);

        /// <summary>
        /// Updates an existing user in the data store.
        /// </summary>
        /// <param name="user">The user with updated values.</param>
        Task UpdateAsync(User user);

        /// <summary>
        /// Retrieves all users in the system.
        /// </summary>
        /// <returns>A collection of all <see cref="User"/> entities.</returns>
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdentificationIdAsync(int identificationId);
    }
}