using RentUniversal.Application.DTOs;
using RentUniversal.Application.Interfaces;
using RentUniversal.Domain.Entities;
using RentUniversal.Application.Mapper;

namespace RentUniversal.Application.Services
{
    /// <summary>
    /// Application service responsible for handling contact-related business logic.
    /// </summary>
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactService"/>.
        /// </summary>
        public ContactService(IContactRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Creates a new contact message.
        /// The identifier is generated automatically by MongoDB.
        /// </summary>
        public async Task<string> CreateAsync(ContactDTO dto)
        {
            var contact = new Contact
            {
                Name = dto.Name,
                Email = dto.Email,
                Message = dto.Message
            };

            await _repository.CreateAsync(contact);

            // MongoDB assigns the BSON ObjectId during insertion
            return contact.Id;
        }

        public async Task<List<ContactDTO>> GetAllAsync()
        {
            var contacts = await _repository.GetAllAsync();
            return contacts.Select(DTOMapper.ToDTO).ToList();
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
