using Microsoft.AspNetCore.Mvc;
using RentUniversal.Application.DTOs;
using RentUniversal.Application.Interfaces;

namespace RentUniversal.Api.Controllers
{
    /// <summary>
    /// API controller responsible for handling contact form requests.
    /// Acts as a thin entry point that forwards requests to the application layer
    /// without containing business logic or database access.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactController"/>.
        /// The controller depends on an application service abstraction
        /// to follow Clean Architecture and Dependency Inversion principles.
        /// </summary>
        /// <param name="contactService">
        /// Application service responsible for contact-related business logic.
        /// </param>
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        /// <summary>
        /// Creates a new contact message from a contact form submission.
        /// The contact message is assigned an auto-generated unique identifier
        /// in the domain layer.
        /// </summary>
        /// <param name="dto">
        /// Data Transfer Object containing sender information and message content.
        /// </param>
        /// <returns>
        /// Returns HTTP 201 Created along with the unique identifier of the new contact message.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContactDTO dto)
        {
            // The ID is generated automatically in the domain entity
            var id = await _contactService.CreateAsync(dto);

            // Return the generated ID to the client
            return CreatedAtAction(nameof(GetAll), new { id }, null);
        }

        /// <summary>
        /// Retrieves all contact messages.
        /// Typically used by administrative or support functionality.
        /// </summary>
        /// <returns>
        /// A list of contact messages wrapped in an HTTP 200 OK response.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _contactService.GetAllAsync();
            return Ok(contacts);
        }

        /// <summary>
        /// Deletes a specific contact message by its unique identifier.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the contact message to delete.
        /// </param>
        /// <returns>
        /// Returns HTTP 204 No Content when the deletion is successful.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _contactService.DeleteAsync(id);
            return NoContent();
        }
    }
}
