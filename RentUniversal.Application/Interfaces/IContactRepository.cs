using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Interfaces;

public interface IContactRepository
{
    Task CreateAsync(Contact contact);
    Task<List<Contact>> GetAllAsync();
    Task DeleteAsync(string id);
}