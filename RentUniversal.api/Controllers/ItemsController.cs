using Microsoft.AspNetCore.Mvc;
using RentUniversal.Application.Interfaces;
using RentUniversal.Domain.Entities;
using RentUniversal.Application.DTOs;
using RentUniversal.Application.Mapper;

namespace RentUniversal.api.Controllers
{
    /// <summary>
    /// API controller responsible for CRUD operations on rentable items.
    /// </summary>
    /// <remarks>
    /// Exposes endpoints for retrieving, creating, updating and deleting items.
    /// Uses the IItemService abstraction to keep controller logic thin.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        /// <summary>
        /// Constructor with dependency injection of the item service.
        /// </summary>
        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        /// <summary>
        /// Retrieves all items in the system.
        /// </summary>
        /// <returns>List of ItemDTO objects</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetAllItems()
        {
            var items = await _itemService.GetAllItemsAsync();
            return Ok(items);
        }

        /// <summary>
        /// Retrieves a single item by its ID.
        /// </summary>
        /// <param name="id">Item identifier</param>
        /// <returns>ItemDTO or 404 if not found</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetItem(string id)
        {
            var item = await _itemService.GetItemAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        /// <summary>
        /// Creates a new item.
        /// </summary>
        /// <remarks>
        /// Accepts a domain Item entity. In a production system,
        /// this would usually be a CreateItemDTO.
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<ItemDTO>> CreateItem([FromBody] Item item)
        {
            if (item == null)
                return BadRequest();

            
            var created = await _itemService.AddItemAsync(item);

            return CreatedAtAction(
                nameof(GetItem),
                new { id = created.Id },
                created
            );
        }

        /// <summary>
        /// Updates an existing item.
        /// </summary>
        /// <param name="id">Item ID from route</param>
        /// <param name="item">Updated item data</param>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem(string id, [FromBody] Item item)
        {
            if (item == null)
                return BadRequest();

            // Ensure route ID and body ID match
            item.Id = id;

            var updated = await _itemService.UpdateItemAsync(item);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deletes an item by ID.
        /// </summary>
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