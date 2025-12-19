using RentUniversal.Application.DTOs;

namespace RentUniversal.Application.Interfaces;

public interface IContactService
{
   
    Task<List<ContactDTO>> GetAllAsync();
    Task DeleteAsync(string id);
    Task<string> CreateAsync(ContactDTO dto);

}