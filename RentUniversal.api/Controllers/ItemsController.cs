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

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetAllItems()
        {
            var items = await _itemService.GetAllItemsAsync();
            return Ok(items);
        }

        // GET: api/Items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetItem(string id)
        {
            var item = await _itemService.GetItemAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // POST: api/Items
        [HttpPost]
        public async Task<ActionResult<ItemDTO>> CreateItem([FromBody] Item item)
        {
            if (item == null)
                return BadRequest();

            var created = await _itemService.AddItemAsync(item);

            // created.Id skal matche typen på id (string i dette tilfælde)
            return CreatedAtAction(
                nameof(GetItem),
                new { id = created.Id },
                created
            );
        }

        // PUT: api/Items/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem(string id, [FromBody] Item item)
        {
            if (item == null)
                return BadRequest();

            // sørg for at Id matcher route-id
            item.Id = id;

            var updated = await _itemService.UpdateItemAsync(item);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/Items/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            var deleted = await _itemService.DeleteItemAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}