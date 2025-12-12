using Microsoft.AspNetCore.Mvc;
using RentUniversal.Application.DTOs;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.Mapper;
using RentUniversal.Domain.Entities;

namespace RentUniversal.api.Controllers
{
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

        // ---------------------------------------------------------
        // GET /api/rentals/{id}
        // ---------------------------------------------------------
        [HttpGet("{id}")]
        public async Task<ActionResult<RentalDTO>> GetRental(string id)
        {
            var rental = await _rentalService.GetRentalAsync(id);
            if (rental == null) return NotFound();
            return Ok(rental);
        }

        // ---------------------------------------------------------
        // PUT /api/rentals/return/{id}
        // ---------------------------------------------------------
        [HttpPut("return/{id}")]
        public async Task<IActionResult> ReturnRental(string id)
        {
            var rentalDTO = await _rentalService.GetRentalAsync(id);
            if (rentalDTO == null)
                return NotFound("Rental not found");

            // Set return timestamp
            rentalDTO.EndDate = DateTime.UtcNow;

            // Start date is non-nullable in DTO
            DateTime start = rentalDTO.StartDate;

            // EndDate is nullable, but we just set it
            DateTime end = rentalDTO.EndDate.Value;

            // Calculate rental period
            TimeSpan period = end - start;
            int days = Math.Max(1, (int)Math.Ceiling(period.TotalDays));

            rentalDTO.TotalPrice = days * rentalDTO.PricePerDay;

            // FIX: correct service variable name
            var result = await _rentalService.UpdateRentalAsync(rentalDTO);
            if (!result)
                return BadRequest("Failed to update rental");

            await _itemService.UpdateAvailabilityAsync(rentalDTO.ItemId, true);

            return Ok();
        }

        // ---------------------------------------------------------
        // GET /api/rentals/user/{userId}
        // ---------------------------------------------------------
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

                    // FIX: cast to nullable so it matches ReturnDate
                    startDate = (DateTime?)rental.RentalDate,

                    // Rental.ReturnDate is already nullable
                    endDate = rental.ReturnDate,

                    userId = rental.UserId,
                    itemId = rental.ItemId,
                    item = item
                });
            }

            return Ok(result);
        }

        // ---------------------------------------------------------
        // POST /api/rentals
        // ---------------------------------------------------------
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

                // FIX: request.StartDate might be nullable → fallback to now
                RentalDate = request.StartDate ?? DateTime.UtcNow,

                // FIX: ReturnDate is nullable now, so request.EndDate is OK
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
