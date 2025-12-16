using Microsoft.AspNetCore.Mvc;
using RentUniversal.Application.DTOs;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.Mapper;
using RentUniversal.Domain.Entities;

namespace RentUniversal.api.Controllers
{
    /// <summary>
    /// API controller responsible for rental lifecycle management.
    /// </summary>
    /// <remarks>
    /// Handles renting, returning, and querying rentals.
    /// Coordinates between rental and item services.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        private readonly IItemService _itemService;

        public RentalsController(IRentalService rentalService, IItemService itemService)
        {
            _rentalService = rentalService;
            _itemService = itemService;
        }

        /// <summary>
        /// Retrieves a rental by its ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<RentalDTO>> GetRental(string id)
        {
            var rental = await _rentalService.GetRentalAsync(id);
            if (rental == null) return NotFound();
            return Ok(rental);
        }

        /// <summary>
        /// Marks a rental as returned and calculates total price.
        /// </summary>
        /// <remarks>
        /// Calculates rental duration in days and ensures a minimum of 1 day.
        /// Also updates item availability.
        /// </remarks>
        [HttpPut("return/{id}")]
        public async Task<IActionResult> ReturnRental(string id)
        {
            var rentalDTO = await _rentalService.GetRentalAsync(id);
            if (rentalDTO == null)
                return NotFound("Rental not found");

            rentalDTO.EndDate = DateTime.UtcNow;

            DateTime start = rentalDTO.StartDate;

            DateTime end = rentalDTO.EndDate.Value;

            TimeSpan period = end - start;
            int days = Math.Max(1, (int)Math.Ceiling(period.TotalDays));

            rentalDTO.TotalPrice = days * rentalDTO.PricePerDay;

            var result = await _rentalService.UpdateRentalAsync(rentalDTO);
            if (!result)
                return BadRequest("Failed to update rental");

            await _itemService.UpdateAvailabilityAsync(rentalDTO.ItemId, true);

            return Ok();
        }

        /// <summary>
        /// Retrieves all rentals for a given user.
        /// </summary>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var rentals = await _rentalService.GetByUserIdAsync(userId);

            var result = new List<object>();

            foreach (var rental in rentals)
            {
                var item = await _itemService.GetByIdAsync(rental.ItemId);

                result.Add(new
                {
                    id = rental.Id,
                    startDate = (DateTime?)rental.RentalDate,
                    endDate = rental.ReturnDate,
                    userId = rental.UserId,
                    itemId = rental.ItemId,
                    item = item
                });
            }

            return Ok(result);
        }

        /// <summary>
        /// Creates a new rental.
        /// </summary>
        /// <remarks>
        /// Validates item availability and sets default dates when missing.
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> RentItem([FromBody] RentalCreateDTO request)
        {
            if (request == null || string.IsNullOrEmpty(request.UserId) || string.IsNullOrEmpty(request.ItemId))
                return BadRequest("UserId and ItemId are required");

            var item = await _itemService.GetByIdAsync(request.ItemId);
            if (item == null)
                return NotFound("Item not found");

            if (!item.IsAvailable)
                return BadRequest("Item is not available");

            var rental = new Rental
            {
                UserId = request.UserId,
                ItemId = request.ItemId,
                RentalDate = request.StartDate ?? DateTime.UtcNow,
                ReturnDate = request.EndDate,
                PricePerDay = item.PricePerDay,
                StartCondition = "OK",
                Price = item.Value
            };

            await _rentalService.CreateAsync(rental);
            await _itemService.UpdateAvailabilityAsync(request.ItemId, false);

            return Ok(DTOMapper.ToDTO(rental, item));
        }

    }
}
