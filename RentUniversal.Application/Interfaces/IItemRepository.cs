using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Interfaces
{
    /// <summary>
    /// Repository abstraction for working with "Item" entities.
    /// The application layer depends on this interface, while the infrastructure layer provides the implementation.
    /// </summary>
    public interface IItemRepository
    {
        /// <summary>
        /// Retrieves a single item by its identifier.
        /// Returns null if the item does not exist.
        /// </summary>
        Task<Item?> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all items in the system.
        /// </summary>
        /// <returns>A collection of all <see cref="Item"/> entities.</returns>
        Task<IEnumerable<Item>> GetAllAsync();

        /// <summary>
        /// Creates a new item in the data store.
        /// </summary>
        /// <param name="item">The item to create.</param>
        Task CreateAsync(Item item);

        /// <summary>
        /// Updates an existing item in the data store.
        /// </summary>
        /// <param name="item">The item to update.</param>
        Task UpdateAsync(Item item);

        /// <summary>
        /// Deletes an item from the data store by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the item to delete.</param>
        Task DeleteAsync(string id);
    }
}