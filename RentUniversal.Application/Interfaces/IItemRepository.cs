using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Interfaces
{
    public interface IItemRepository
    {
        Task<Item?> GetByIdAsync(string id);
        Task<IEnumerable<Item>> GetAllAsync();
        Task CreateAsync(Item item);
        Task UpdateAsync(Item item);
    }
}

