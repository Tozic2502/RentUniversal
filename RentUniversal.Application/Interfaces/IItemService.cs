using RentUniversal.Application.DTOs;
using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Interfaces;

public interface IItemService
{
    Task<ItemDTO?> GetItemAsync(string id);
    Task<IEnumerable<ItemDTO>> GetAllItemsAsync();
    Task<ItemDTO> AddItemAsync(Item item);
    Task<bool> UpdateItemAsync(Item item);
    Task UpdateAvailabilityAsync(string itemId, bool available);
    Task<Item?> GetByIdAsync(string id);
    Task<bool> DeleteItemAsync(string id);
    
    Task<string?> AddItemImageAsync(string itemId, string imageUrl);
    Task<bool> RemoveItemImageAsync(string itemId, string imageUrl);
    
}