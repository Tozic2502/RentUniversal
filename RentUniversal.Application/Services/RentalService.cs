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
            // Fetch rental from repository by ID
            var rental = await _rentalRepository.GetByIdAsync(rentalId);

            // Map to DTO if rental exists, otherwise return null
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
            // Create a new rental object with provided details
            var rental = new Rental
            {
                Id = Guid.NewGuid().ToString(), // Generate unique ID
                UserId = userId,
                ItemId = itemId,
                StartCondition = startCondition,
                RentalDate = DateTime.UtcNow // Set current UTC time as rental start time
            };

            // Persist the new rental in the repository
            await _rentalRepository.CreateAsync(rental);

            // Map the rental to a DTO and return it
            return DTOMapper.ToDTO(rental);
        }

        /// <summary>
        /// Ends an existing rental.
        /// Sets end timestamp, stores return condition, calculates the final price, and persists the update.
        /// </summary>
        /// <param name="rentalId">Identifier of the rental to end.</param>
        /// <param name="returnCondition">Item condition at the time of return.</param>
        /// <returns>The updated rental as a <see cref="RentalDTO"/>.</returns>
        /// <exception cref="Exception">Thrown if the rental cannot be found.</exception>
        public async Task<RentalDTO> EndRentalAsync(string rentalId, string returnCondition)
        {
            // Fetch rental from repository by ID
            var rental = await _rentalRepository.GetByIdAsync(rentalId);

            // Throw exception if rental does not exist
            if (rental == null) throw new Exception("Rental not found");

            // Set return details
            rental.ReturnDate = DateTime.UtcNow; // Set current UTC time as return time
            rental.ReturnCondition = returnCondition;

            // Calculate the rental price based on duration
            rental.Price = CalculatePrice(rental);

            // Persist the updated rental in the repository
            await _rentalRepository.UpdateAsync(rental);

            // Map the updated rental to a DTO and return it
            return DTOMapper.ToDTO(rental);
        }

        /// <summary>
        /// Retrieves all rentals belonging to a specific user and maps them to DTOs.
        /// </summary>
        /// <param name="userId">The user's identifier.</param>
        /// <returns>A collection of <see cref="RentalDTO"/>.</returns>
        public async Task<IEnumerable<Rental>> GetByUserIdAsync(string userId)
        {
            // Fetch rentals for the specified user from the repository
            return await _rentalRepository.GetByUserIdAsync(userId);
        }

        /// <summary>
        /// Calculates the rental price based on the rental duration.
        /// Current rule: price = days * PricePerDay, rounded to 2 decimals.
        /// If <see cref="Rental.ReturnDate"/> is not set, price is 0.
        /// </summary>
        /// <param name="rental">Rental record used for calculation.</param>
        /// <returns>The calculated price.</returns>
        public double CalculatePrice(Rental rental)
        {
            // If no return date is set, return price as 0
            if (!rental.ReturnDate.HasValue)
                return 0;

            // Calculate rental duration
            DateTime returnDate = rental.ReturnDate.Value;
            DateTime rentalDate = rental.RentalDate;
            TimeSpan rentalDuration = returnDate - rentalDate;

            // Calculate the number of rental days (minimum 1 day)
            int days = Math.Max(1, (int)Math.Ceiling(rentalDuration.TotalDays));

            // Calculate total price based on days and price per day
            return days * rental.PricePerDay;
        }

        /// <summary>
        /// Creates a new rental record in the repository.
        /// </summary>
        /// <param name="rental">The rental entity to create.</param>
        public async Task CreateAsync(Rental rental)
        {
            // Persist the rental in the repository
            await _rentalRepository.CreateAsync(rental);
        }

        /// <summary>
        /// Updates an existing rental with new details.
        /// </summary>
        /// <param name="rentalDto">The rental DTO containing updated details.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        public async Task<bool> UpdateRentalAsync(RentalDTO rentalDto)
        {
            // Fetch the existing rental from the repository by ID
            var existingRental = await _rentalRepository.GetByIdAsync(rentalDto.Id);

            // Return false if the rental does not exist
            if (existingRental == null)
                return false;

            // Update allowed fields
            existingRental.ReturnDate = rentalDto.EndDate;
            existingRental.ReturnCondition = rentalDto.ReturnCondition;

            // Recalculate the price based on the updated return date
            existingRental.Price = CalculatePrice(existingRental);

            // Persist the updated rental in the repository
            await _rentalRepository.UpdateAsync(existingRental);

            return true;
        }
    }
}