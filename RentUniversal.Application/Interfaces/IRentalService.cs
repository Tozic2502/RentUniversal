using RentUniversal.Application.DTOs;
using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Interfaces;

public interface IRentalService
{
    Task<RentalDTO?> GetRentalAsync(string rentalId);
    Task<RentalDTO> StartRentalAsync(string userId, string itemId, string startCondition);
    Task<RentalDTO> EndRentalAsync(string rentalId, string returnCondition);
    Task<IEnumerable<Rental>> GetByUserIdAsync(string userId);
    Task<bool> UpdateRentalAsync(RentalDTO rental);
    Task CreateAsync(Rental rental);
    double CalculatePrice(Rental rental);
}
