using RentUniversal.Application.DTOs;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.Mappers;
using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;

        public RentalService(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task<RentalDTO?> GetRentalAsync(string rentalId)
        {
            var rental = await _rentalRepository.GetByIdAsync(rentalId);
            return rental == null ? null : DTOMapper.ToDTO(rental);
        }

        public async Task<RentalDTO> StartRentalAsync(string userId, string itemId, string startCondition)
        {
            var rental = new Rental
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                ItemId = itemId,
                StartCondition = startCondition,
                StartDate = DateTime.UtcNow
            };

            await _rentalRepository.CreateAsync(rental);
            return DTOMapper.ToDTO(rental);
        }

        public async Task<RentalDTO> EndRentalAsync(string rentalId, string returnCondition)
        {
            var rental = await _rentalRepository.GetByIdAsync(rentalId);
            if (rental == null) throw new Exception("Rental not found");

            rental.EndDate = DateTime.UtcNow;
            rental.ReturnCondition = returnCondition;
            rental.Price = CalculatePrice(rental);

            await _rentalRepository.UpdateAsync(rental);
            return DTOMapper.ToDTO(rental);
        }

        public double CalculatePrice(Rental rental)
        {
            if (rental.EndDate == null) return 0;
            var hours = (rental.EndDate.Value - rental.StartDate).TotalHours;
            return Math.Round(hours * 29.00, 2);
        }
    }
}
