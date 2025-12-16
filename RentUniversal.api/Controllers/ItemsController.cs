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
        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// Constructor with dependency injection of the item service.
        /// </summary>
        public ItemsController(IItemService itemService , IWebHostEnvironment env)
        {
            _itemService = itemService;
            _env = env;
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
        
        [HttpPost("{itemId}/images")]
        public async Task<IActionResult> UploadImage(string itemId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var item = await _itemService.GetByIdAsync(itemId);
            if (item == null)
                return NotFound();

            // Brug WebRootPath (peger på projekt-rod/wwwroot)
            var webRoot = _env.WebRootPath 
                          ?? Path.Combine(_env.ContentRootPath, "wwwroot");

            var uploadsPath = Path.Combine(webRoot, "uploads", "items", itemId);
            Directory.CreateDirectory(uploadsPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // URL som klienten kan bruge
            var url = $"/uploads/items/{itemId}/{fileName}";

            item.ImageUrls ??= new List<string>();
            item.ImageUrls.Add(url);
            await _itemService.UpdateItemAsync(item);

            return Ok(new { url });
        }
    
        
        [HttpDelete("{itemId}/images")]
        public async Task<IActionResult> DeleteImage(string itemId, [FromBody] string fileName)
        {
            var item = await _itemService.GetByIdAsync(itemId);
            if (item == null)
                return NotFound();

            var path = Path.Combine("wwwroot", "uploads", "items", itemId, fileName);
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            item.ImageUrls.RemoveAll(x => x.EndsWith(fileName));
            await _itemService.UpdateItemAsync(item);


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