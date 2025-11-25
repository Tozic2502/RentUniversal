using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Interfaces
{
    public interface IRentalRepository
    {
        Task<Rental?> GetByIdAsync(string id);
        Task<IEnumerable<Rental>> GetByUserIdAsync(string userId);
        Task CreateAsync(Rental rental);
        Task UpdateAsync(Rental rental);
    }
}
