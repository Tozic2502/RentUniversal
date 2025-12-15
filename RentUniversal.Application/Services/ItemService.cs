using RentUniversal.Application.DTOs;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.Mappers;
using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Services
{
    /// <summary>
    /// Application service responsible for item-related operations.
    /// This layer coordinates repository calls and maps domain entities to DTOs for the API/UI layer.
    /// </summary>
    public class ItemService : IItemService
    {
        /// <summary>
        /// Repository used for item persistence and retrieval.
        /// </summary>
        private readonly IItemRepository _itemRepository;

        /// <summary>
        /// Creates a new <see cref="ItemService"/> instance.
        /// </summary>
        /// <param name="itemRepository">Repository implementation for item storage.</param>
        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        /// <summary>
        /// Retrieves a single item by id and maps it to a DTO.
        /// Returns null if the item does not exist.
        /// </summary>
        /// <param name="id">The item identifier.</param>
        /// <returns>An <see cref="ItemDTO"/> or null.</returns>
        public async Task<ItemDTO?> GetItemAsync(string id)
        {
            var item = await _itemRepository.GetByIdAsync(id);

            // Avoids null reference and clearly communicates "not found" to callers.
            return item == null ? null : DTOMapper.ToDTO(item);
        }

        /// <summary>
        /// Retrieves all items and maps them to DTOs.
        /// </summary>
        /// <returns>A collection of <see cref="ItemDTO"/>.</returns>
        public async Task<IEnumerable<ItemDTO>> GetAllItemsAsync()
        {
            var items = await _itemRepository.GetAllAsync();

            // Mapping is done here to keep controllers thin and domain entities internal.
            return items.Select(DTOMapper.ToDTO);
        }

        /// <summary>
        /// Creates a new item in the repository and returns it as a DTO.
        /// </summary>
        /// <param name="item">The domain item to create.</param>
        /// <returns>The created item as <see cref="ItemDTO"/>.</returns>
        public async Task<ItemDTO> AddItemAsync(Item item)
        {
            await _itemRepository.CreateAsync(item);

            // Assumes the repository either persists the same instance or the instance already contains the correct id.
            return DTOMapper.ToDTO(item);
        }

        /// <summary>
        /// Updates an existing item.
        /// Returns false if the item does not exist; otherwise updates and returns true.
        /// </summary>
        /// <param name="item">The item with updated values.</param>
        /// <returns>True if the update succeeded; otherwise false.</returns>
        public async Task<bool> UpdateItemAsync(Item item)
        {
            // Simple existence check so we can return a clean boolean rather than throwing.
            var existing = await _itemRepository.GetByIdAsync(item.Id);
            if (existing == null)
                return false;

            await _itemRepository.UpdateAsync(item);
            return true;
        }

        /// <summary>
        /// Deletes an item by id.
        /// Returns false if the item does not exist; otherwise deletes and returns true.
        /// </summary>
        /// <param name="id">The item identifier.</param>
        /// <returns>True if delete succeeded; otherwise false.</returns>
        public async Task<bool> DeleteItemAsync(string id)
        {
            // Existence check to avoid attempting to delete an item that isn't there.
            var existing = await _itemRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            await _itemRepository.DeleteAsync(id);
            return true;
        }
    }
}
