using System.Diagnostics;
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
        private readonly IItemService _itemService; // Service layer abstraction for item operations
        private readonly IWebHostEnvironment _env; // Provides information about the web hosting environment

        /// <summary>
        /// Constructor with dependency injection of the item service and hosting environment.
        /// </summary>
        /// <param name="itemService">Service for item-related operations</param>
        /// <param name="env">Web hosting environment</param>
        public ItemsController(IItemService itemService, IWebHostEnvironment env)
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
            // Fetch all items from the service layer
            var items = await _itemService.GetAllItemsAsync();
            return Ok(items); // Return the items as an HTTP 200 response
        }

        /// <summary>
        /// Retrieves a single item by its ID.
        /// </summary>
        /// <param name="id">Item identifier</param>
        /// <returns>ItemDTO or 404 if not found</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetItem(string id)
        {
            // Fetch the item by ID
            var item = await _itemService.GetItemAsync(id);
            if (item == null)
                return NotFound(); // Return 404 if the item does not exist

            return Ok(item); // Return the item as an HTTP 200 response
        }

        /// <summary>
        /// Creates a new item.
        /// </summary>
        /// <remarks>
        /// Accepts a domain Item entity. In a production system,
        /// this would usually be a CreateItemDTO.
        /// </remarks>
        /// <param name="itemDTO">Data transfer object representing the item to create</param>
        /// <returns>The created item</returns>
        [HttpPost]
        public async Task<ActionResult<ItemDTO>> CreateItem([FromBody] ItemDTO itemDTO)
        {
            if (itemDTO == null)
                return BadRequest(); // Return 400 if the request body is null

            // Map the DTO to a domain entity
            var item = new Item
            {
                Name = itemDTO.Name,
                Category = itemDTO.Category,
                Condition = itemDTO.Condition,
                Value = itemDTO.Value,
                IsAvailable = itemDTO.IsAvailable,
                Deposit = itemDTO.Deposit,
                PricePerDay = itemDTO.PricePerDay,
                ImageUrls = itemDTO.ImageUrls
            };

            // Add the item using the service layer
            var created = await _itemService.AddItemAsync(item);

            // Return the created item with a 201 response
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
        /// <returns>No content if successful, 404 if the item does not exist</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem(string id, [FromBody] Item item)
        {
            if (item == null)
                return BadRequest(); // Return 400 if the request body is null

            // Ensure route ID and body ID match
            item.Id = id;

            // Update the item using the service layer
            var updated = await _itemService.UpdateItemAsync(item);
            if (!updated)
                return NotFound(); // Return 404 if the item does not exist

            return NoContent(); // Return 204 if the update was successful
        }

        /// <summary>
        /// Uploads an image for a specific item.
        /// </summary>
        /// <param name="itemId">ID of the item</param>
        /// <param name="file">Image file to upload</param>
        /// <returns>URL of the uploaded image</returns>
        [HttpPost("{itemId}/images")]
        public async Task<IActionResult> UploadImage(string itemId, IFormFile file)
        {
            
            Console.WriteLine("=== IMAGE UPLOAD ENDPOINT HIT ===");
            Console.WriteLine($"ItemId = {itemId}");
            Console.WriteLine($"File null? {file == null}");
            
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var item = await _itemService.GetByIdAsync(itemId);
            if (item == null)
                return NotFound();

            var uploadsPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                "items",
                itemId
            );
            
            Console.WriteLine($"UPLOAD PATH = {uploadsPath}");
            
            Directory.CreateDirectory(uploadsPath);
            
            Console.WriteLine($"DIR EXISTS AFTER CREATE? {Directory.Exists(uploadsPath)}");

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            // URL som klienten kan bruge
            var url = $"/uploads/items/{itemId}/{fileName}";

            item.ImageUrls ??= new List<string>();
            item.ImageUrls.Add(url);

            await _itemService.UpdateItemAsync(item);


            return Ok(new { url });
        }

        /// <summary>
        ///  Deletes an image associated with an item.
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
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