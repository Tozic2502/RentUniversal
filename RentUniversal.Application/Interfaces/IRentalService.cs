using RentUniversal.Application.DTOs;
using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Interfaces;

public interface IRentalService
{
    Task<RentalDTO?> GetRentalAsync(string rentalId);
    Task<RentalDTO> StartRentalAsync(string userId, string itemId, string startCondition);
    Task<RentalDTO> EndRentalAsync(string rentalId, string returnCondition);
    Task<IEnumerable<RentalDTO>> GetRentalsByUserAsync(string userId);
    double CalculatePrice(Rental rental);
}
