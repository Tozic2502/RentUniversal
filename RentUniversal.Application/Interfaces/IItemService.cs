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

        /// <summary>
        /// Retrieves an item entity by its identifier.
        /// This method is typically used internally for operations requiring the domain entity.
        /// </summary>
        /// <param name="id">The item identifier.</param>
        /// <returns>The matching <see cref="Item"/> or null if not found.</returns>
        Task<Item?> GetByIdAsync(string id);

        /// <summary>
        /// Updates the availability status of an item.
        /// </summary>
        /// <param name="itemId">The identifier of the item to update.</param>
        /// <param name="available">The new availability status.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAvailabilityAsync(string itemId, bool available);

        /// <summary>
        /// Adds an image URL to an item.
        /// Returns the added image URL if successful, or null if the operation failed.
        /// </summary>
        /// <param name="itemId">The identifier of the item to update.</param>
        /// <param name="imageUrl">The URL of the image to add.</param>
        /// <returns>The added image URL, or null if the operation failed.</returns>
        Task<string?> AddItemImageAsync(string itemId, string imageUrl);

        /// <summary>
        /// Removes an image URL from an item.
        /// Returns true if the image was successfully removed; otherwise false.
        /// </summary>
        /// <param name="itemId">The identifier of the item to update.</param>
        /// <param name="imageUrl">The URL of the image to remove.</param>
        /// <returns>True if the image was removed; otherwise false.</returns>
        Task<bool> RemoveItemImageAsync(string itemId, string imageUrl);
    }
}