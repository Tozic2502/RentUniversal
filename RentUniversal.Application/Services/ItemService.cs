using RentUniversal.Application.DTOs;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.Mappers;
using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Services;

public class ItemService : IItemService
{
    private readonly List<Item> _items = new();

    public Task<ItemDTO?> GetItemAsync(string id)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        return Task.FromResult(item is null ? null : DTOMapper.ToDTO(item));
    }

    public Task<IEnumerable<ItemDTO>> GetAllItemsAsync()
    {
        var dtos = _items.Select(DTOMapper.ToDTO);
        return Task.FromResult(dtos);
    }

    public Task<ItemDTO> AddItemAsync(Item item)
    {
        _items.Add(item);
        return Task.FromResult(DTOMapper.ToDTO(item));
    }

    public Task<bool> UpdateItemAsync(Item item)
    {
        var found = _items.FirstOrDefault(i => i.Id == item.Id);
        if (found == null) return Task.FromResult(false);

        found.Name = item.Name;
        found.Category = item.Category;
        found.Condition = item.Condition;
        found.Value = item.Value;
        found.IsAvailable = item.IsAvailable;

        return Task.FromResult(true);
    }
}
