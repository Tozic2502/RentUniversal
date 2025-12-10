using MongoDB.Bson;
using RentUniversal.Application.DTOs;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.Mapper;
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
                RentalDate = DateTime.UtcNow
            };

            await _rentalRepository.CreateAsync(rental);
            return DTOMapper.ToDTO(rental);
        }

        public async Task<RentalDTO> EndRentalAsync(string rentalId, string returnCondition)
        {
            var rental = await _rentalRepository.GetByIdAsync(rentalId);
            if (rental == null) throw new Exception("Rental not found");

            rental.ReturnDate = DateTime.UtcNow;
            rental.ReturnCondition = returnCondition;
            rental.Price = CalculatePrice(rental);

            await _rentalRepository.UpdateAsync(rental);
            return DTOMapper.ToDTO(rental);
        }
        public async Task<IEnumerable<Rental>> GetByUserIdAsync(string userId)
        {
            return await _rentalRepository.GetByUserIdAsync(userId);
        }



        public double CalculatePrice(Rental rental)
        {
            if (!rental.ReturnDate.HasValue)
                return 0;

            var rentalDuration = rental.ReturnDate.Value - rental.RentalDate;
            double duration = rentalDuration.ToBsonDocument().GetValue("Days").ToDouble() + 1; // Include partial days as full days
            return duration * rental.PricePerDay;
        }


        public async Task CreateAsync(Rental rental)
        {
            await _rentalRepository.CreateAsync(rental);
        }

        public async Task<bool> UpdateRentalAsync(RentalDTO rentalDto)
        {
            // 1. Find existing rental in DB
            var existingRental = await _rentalRepository.GetByIdAsync(rentalDto.Id);

            if (existingRental == null)
                return false;

            // 2. Update allowed fields
            existingRental.ReturnDate = rentalDto.EndDate ?? DateTime.UtcNow;
            existingRental.ReturnCondition = rentalDto.ReturnCondition;

            // 3. Recalculate price based on new EndDate
            existingRental.Price = CalculatePrice(existingRental);

            // 4. Save back to DB
            await _rentalRepository.UpdateAsync(existingRental);

            return true;
        }

    }
}
