using RentUniversal.Application.DTOs;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.Mapper;
using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Services
{
    public class ItemService : IItemService
    {
        const double DEPOSIT_RATE = 0.30;
        const double PRICE_RATE = 0.10;
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<ItemDTO?> GetItemAsync(string id)
        {
            var item = await _itemRepository.GetByIdAsync(id);
            return item == null ? null : DTOMapper.ToDTO(item);
        }

        public async Task<IEnumerable<ItemDTO>> GetAllItemsAsync()
        {
            var items = await _itemRepository.GetAllAsync();
            return items.Select(DTOMapper.ToDTO);
        }

        public async Task<ItemDTO> AddItemAsync(Item item)
        {
            item.Deposit = Math.Round(item.Value * DEPOSIT_RATE, 2);
            item.PricePerDay = Math.Round(item.Value * PRICE_RATE, 2);
            await _itemRepository.CreateAsync(item);
            
            return DTOMapper.ToDTO(item);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var existing = await _itemRepository.GetByIdAsync(item.Id);
            if (existing == null)
                return false;

            await _itemRepository.UpdateAsync(item);
            return true;
        }
        public async Task UpdateAvailabilityAsync(string itemId, bool available)
        {
            var item = await _itemRepository.GetByIdAsync(itemId);
            if (item == null) return;

            item.IsAvailable = available;
            await _itemRepository.UpdateAsync(item);
        }
        public async Task<Item?> GetByIdAsync(string id)
        {
            return await _itemRepository.GetByIdAsync(id);
        }


        public async Task<bool> DeleteItemAsync(string id)
        {
            var existing = await _itemRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            await _itemRepository.DeleteAsync(id);
            return true;
        }
    }
}