using RentUniversal.Application.DTOs;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.Mappers;
using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Services
{
    public class ItemService : IItemService
    {
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
            await _itemRepository.CreateAsync(item);
            return DTOMapper.ToDTO(item);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var existing = await _itemRepository.GetByIdAsync(item.Id);
            if (existing == null) return false;

            await _itemRepository.UpdateAsync(item);
            return true;
        }
    }
}
