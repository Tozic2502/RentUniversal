using RentUniversal.Application.DTOs;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.Mappers;
using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Services;

public class RentalService : IRentalService
{
    private readonly List<Rental> _rentals = new();

    public Task<RentalDTO?> GetRentalAsync(string rentalId)
    {
        var rental = _rentals.FirstOrDefault(r => r.Id == rentalId);
        return Task.FromResult(rental is null ? null : DTOMapper.ToDTO(rental));
    }

    public Task<RentalDTO> StartRentalAsync(string userId, string itemId, string startCondition)
    {
        var rental = new Rental
        {
            Id = Guid.NewGuid().ToString(),
            UserId = userId,
            ItemId = itemId,
            StartCondition = startCondition,
            StartDate = DateTime.UtcNow
        };

        _rentals.Add(rental);
        return Task.FromResult(DTOMapper.ToDTO(rental));
    }

    public Task<RentalDTO> EndRentalAsync(string rentalId, string returnCondition)
    {
        var rental = _rentals.First(r => r.Id == rentalId);
        rental.EndDate = DateTime.UtcNow;
        rental.ReturnCondition = returnCondition;
        rental.Price = CalculatePrice(rental);

        return Task.FromResult(DTOMapper.ToDTO(rental));
    }

    public double CalculatePrice(Rental rental)
    {
        if (rental.EndDate == null) return 0;
        var hours = (rental.EndDate.Value - rental.StartDate).TotalHours;
        return Math.Round(hours * 29.00, 2); // Your pricing model
    }
}
