using RentUniversal.Application.DTOs;
using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Interfaces
{
    public interface IItemService
    {
        Task<ItemDTO?> GetItemAsync(string id);
        Task<IEnumerable<ItemDTO>> GetAllItemsAsync();
        Task<ItemDTO> AddItemAsync(Item item);
        Task<bool> UpdateItemAsync(Item item);
        Task<bool> DeleteItemAsync(string id);
    }
}