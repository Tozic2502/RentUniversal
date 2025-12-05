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
        private readonly IMongoCollection<ContactMessage> _messages;

        public ContactController(MongoContext context)
        {
            _messages = context.Database.GetCollection<ContactMessage>("ContactMessages");
        }

        // POST: /api/contact
        [HttpPost]
        public async Task<IActionResult> PostContact([FromBody] ContactMessageDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var msg = new ContactMessage
            {
                Name = dto.Name,
                Email = dto.Email,
                Message = dto.Message,
                CreatedAt = DateTime.UtcNow
            };

            await _messages.InsertOneAsync(msg);

           
            return Ok();
        }
    }
}