using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RentUniversal.Domain.Entities;
using RentUniversal.Infrastructure.Data;
using RentUniversal.Application.DTOs;

namespace RentUniversal.api.Controllers
{
    // Marks this class as an API controller (automatic model validation, etc.)
    [ApiController]

    // Base route: /api/Contact
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        // MongoDB collection used to store contact messages
        private readonly IMongoCollection<ContactMessage> _messages;

        // Constructor
        // The MongoContext is injected via dependency injection
        // and used to access the MongoDB database
        public ContactController(MongoContext context)
        {
            _messages = context.Database.GetCollection<ContactMessage>("ContactMessages");
        }

        // POST: /api/Contact
        // Used by the frontend contact form to submit a new message
        [HttpPost]
        public async Task<IActionResult> PostContact([FromBody] ContactMessageDto dto)
        {
            // Automatic model validation via [ApiController]
            // This checks required fields, email format, etc.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map DTO to domain entity
            // DTO is used to avoid exposing the database model directly
            var msg = new ContactMessage
            {
                Name = dto.Name,             
                Email = dto.Email,            
                Message = dto.Message,        
                CreatedAt = DateTime.UtcNow   
            };

            // Save the message to MongoDB
            await _messages.InsertOneAsync(msg);

            // Return HTTP 200 OK when successful
            return Ok();
        }

        // GET: /api/Contact
        // Returns all contact messages
        // Typically used for admin/support overview
        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            // Fetch all messages from MongoDB
            // Sorted so the newest messages appear first
            var messages = await _messages
                .Find(_ => true)
                .SortByDescending(m => m.CreatedAt)
                .ToListAsync();

            // Return the list as JSON
            return Ok(messages);
        }
    }
}
