using MongoDB.Bson;
using RentUniversal.Application.DTOs;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.Mapper;
using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Services
{
    /// <summary>
    /// Application service responsible for rental-related operations.
    /// Coordinates repository calls, manages rental lifecycle, and calculates pricing.
    /// </summary>
    public class RentalService : IRentalService
    {
        /// <summary>
        /// Repository used for rental persistence and retrieval.
        /// </summary>
        private readonly IRentalRepository _rentalRepository;

        /// <summary>
        /// Creates a new <see cref="RentalService"/> instance.
        /// </summary>
        /// <param name="rentalRepository">Repository implementation for rental storage.</param>
        public RentalService(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        /// <summary>
        /// Retrieves a rental by id and maps it to a DTO.
        /// Returns null if the rental does not exist.
        /// </summary>
        /// <param name="rentalId">The rental identifier.</param>
        /// <returns>A <see cref="RentalDTO"/> or null.</returns>
        public async Task<RentalDTO?> GetRentalAsync(string rentalId)
        {
            var rental = await _rentalRepository.GetByIdAsync(rentalId);
            return rental == null ? null : DTOMapper.ToDTO(rental);
        }

        /// <summary>
        /// Starts a new rental by creating a <see cref="Rental"/> record.
        /// Sets the start condition and start timestamp then persists it.
        /// </summary>
        /// <param name="userId">Identifier of the user starting the rental.</param>
        /// <param name="itemId">Identifier of the item being rented.</param>
        /// <param name="startCondition">Item condition at the start of the rental.</param>
        /// <returns>The created rental as a <see cref="RentalDTO"/>.</returns>
        public async Task<RentalDTO> StartRentalAsync(string userId, string itemId, string startCondition)
        {
            var rental = new Rental
            {
                // Generates a unique id for the new rental (ObjectId.)
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                ItemId = itemId,
                StartCondition = startCondition,
                RentalDate = DateTime.UtcNow
            };

            await _rentalRepository.CreateAsync(rental);
            return DTOMapper.ToDTO(rental);
        }

        /// <summary>
        /// Ends an existing rental.
        /// Sets end timestamp stores return condition, calculates the final price, and persists the update.
        /// </summary>
        /// <param name="rentalId">Identifier of the rental to end.</param>
        /// <param name="returnCondition">Item condition at the time of return.</param>
        /// <returns>The updated rental as a <see cref="RentalDTO"/>.</returns>
        /// <exception cref="Exception">Thrown if the rental cannot be found.</exception>
        public async Task<RentalDTO> EndRentalAsync(string rentalId, string returnCondition)
        {
            var rental = await _rentalRepository.GetByIdAsync(rentalId);
            if (rental == null) throw new Exception("Rental not found");

            rental.ReturnDate = DateTime.UtcNow;
            rental.ReturnCondition = returnCondition;

            // Price is derived from duration (start -> end) according to current pricing rules.
            rental.Price = CalculatePrice(rental);

            await _rentalRepository.UpdateAsync(rental);
            return DTOMapper.ToDTO(rental);
        }
        /// <summary>
        /// Retrieves all rentals belonging to a specific user and maps them to DTOs.
        /// </summary>
        /// <param name="userId">The user's identifier.</param>
        /// <returns>A collection of <see cref="RentalDTO"/>.</returns>
        public async Task<IEnumerable<Rental>> GetByUserIdAsync(string userId)
        {
            return await _rentalRepository.GetByUserIdAsync(userId);
        }

        /// <summary>
        /// Calculates the rental price based on the rental duration.
        /// Current rule: price = hours * 29.00, rounded to 2 decimals.
        /// If <see cref="Rental.EndDate"/> is not set, price is 0.
        /// </summary>
        /// <param name="rental">Rental record used for calculation.</param>
        /// <returns>The calculated price.</returns>
        public double CalculatePrice(Rental rental)
        {
            // If no return date → no price
            if (!rental.ReturnDate.HasValue)
                return 0;

            DateTime returnDate = rental.ReturnDate.Value;
            DateTime rentalDate = rental.RentalDate;

            TimeSpan rentalDuration = returnDate - rentalDate;

            int days = Math.Max(1, (int)Math.Ceiling(rentalDuration.TotalDays));

            return days * rental.PricePerDay;
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
            existingRental.ReturnDate = rentalDto.EndDate;
            existingRental.ReturnCondition = rentalDto.ReturnCondition;

            // 3. Recalculate price based on new EndDate
            existingRental.Price = CalculatePrice(existingRental);

            // 4. Save back to DB
            await _rentalRepository.UpdateAsync(existingRental);

            return true;
        }

    }
}