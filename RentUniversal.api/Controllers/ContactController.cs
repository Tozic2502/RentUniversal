using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RentUniversal.Domain.Entities;
using RentUniversal.Infrastructure.Data;   
using RentUniversal.Application.DTOs;

namespace RentUniversal.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        // MongoDB collection for storing contact messages
        private readonly IMongoCollection<ContactMessage> _messages;

        // Constructor to initialize the MongoDB collection
        public ContactController(MongoContext context)
        {
            _messages = context.Database.GetCollection<ContactMessage>("ContactMessages");
        }

        // Handles HTTP POST requests to /api/contact
        // Accepts a ContactMessageDto object in the request body
        // Validates the input and saves the contact message to the database
        [HttpPost]
        public async Task<IActionResult> PostContact([FromBody] ContactMessageDto dto)
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return 400 Bad Request if validation fails
            }

            // Create a new ContactMessage entity from the DTO
            var msg = new ContactMessage
            {
                Name = dto.Name, // Name of the sender
                Email = dto.Email, // Email of the sender
                Message = dto.Message, // Message content
                CreatedAt = DateTime.UtcNow // Timestamp of message creation
            };

            // Insert the new message into the MongoDB collection
            await _messages.InsertOneAsync(msg);

            // Return 200 OK response
            return Ok();
        }
    }
}