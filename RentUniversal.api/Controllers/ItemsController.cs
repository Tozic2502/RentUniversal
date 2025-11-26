using Microsoft.AspNetCore.Mvc;
using RentUniversal.Application.Interfaces;
using RentUniversal.Domain.Entities;
using RentUniversal.Application.DTOs;

namespace RentUniversal.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetAllItems()
        {
            return Ok(await _itemService.GetAllItemsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetItem(string id)
        {
            var item = await _itemService.GetItemAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<ItemDTO>> CreateItem([FromBody] Item item)
        {
            var created = await _itemService.AddItemAsync(item);
            return CreatedAtAction(nameof(GetItem), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem(string id, [FromBody] Item item)
        {
            item.Id = id;
            bool updated = await _itemService.UpdateItemAsync(item);
            if (!updated) return NotFound();
            return NoContent();
        }
    }
}
