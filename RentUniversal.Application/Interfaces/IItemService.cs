using RentUniversal.Application.DTOs;
using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Interfaces
{
    /// <summary>
    /// Service abstraction for item-related business logic.
    /// Typically called by controllers/endpoints and implemented in the application layer.
    /// </summary>
    public interface IItemService
    {
        /// <summary>
        /// Retrieves a single item as a DTO by its identifier.
        /// Returns null if no item exists with the provided id.
        /// </summary>
        /// <param name="id">The item identifier.</param>
        /// <returns>The matching <see cref="ItemDTO"/> or null.</returns>
        Task<ItemDTO?> GetItemAsync(string id);

        /// <summary>
        /// Retrieves all items as DTOs.
        /// </summary>
        /// <returns>A collection of <see cref="ItemDTO"/>.</returns>
        Task<IEnumerable<ItemDTO>> GetAllItemsAsync();

        /// <summary>
        /// Creates a new item.
        /// Note: This method currently accepts a domain <see cref="Item"/> and returns an <see cref="ItemDTO"/>.
        /// Some architectures prefer accepting an ItemCreate DTO instead, to avoid exposing domain types to the API.
        /// </summary>
        /// <param name="item">The item to create.</param>
        /// <returns>The created item as <see cref="ItemDTO"/>.</returns>
        Task<ItemDTO> AddItemAsync(Item item);

        /// <summary>
        /// Updates an existing item.
        /// Returns true if the update succeeded; otherwise false (item not found).
        /// </summary>
        /// <param name="item">The item with updated values.</param>
        /// <returns>True if the item was updated; otherwise false.</returns>
        Task<bool> UpdateItemAsync(Item item);

        /// <summary>
        /// Deletes an item by its identifier.
        /// Returns true if the deletion succeeded; otherwise false (item not found).
        /// </summary>
        /// <param name="id">The item identifier.</param>
        /// <returns>True if the item was deleted; otherwise false.</returns>
        Task<bool> DeleteItemAsync(string id);
    }
}
