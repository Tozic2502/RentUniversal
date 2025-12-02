using Microsoft.AspNetCore.Mvc;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.DTOs;

namespace RentUniversal.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RentalDTO>> GetRental(string id)
        {
            var rental = await _rentalService.GetRentalAsync(id);
            if (rental == null) return NotFound();
            return Ok(rental);
        }

        [HttpPost]
        public async Task<ActionResult<RentalDTO>> StartRental(string userId, string itemId, string condition)
        {
            var rental = await _rentalService.StartRentalAsync(userId, itemId, condition);
            return CreatedAtAction(nameof(GetRental), new { id = rental.Id }, rental);
        }

        [HttpPut("{id}/return")]
        public async Task<ActionResult<RentalDTO>> EndRental(string id, string condition)
        {
            var rental = await _rentalService.EndRentalAsync(id, condition);
            return Ok(rental);
        }
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<RentalDTO>>> GetUserRentals(string userId)
        {
            var rentals = await _rentalService.GetRentalsByUserAsync(userId);
            return Ok(rentals);
        }

    }
}

