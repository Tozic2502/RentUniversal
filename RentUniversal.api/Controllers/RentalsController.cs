using Microsoft.AspNetCore.Mvc;
using RentUniversal.Application.DTOs;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.Mapper;
using RentUniversal.Application.Services;
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


        [HttpGet("{id}")]
        public async Task<ActionResult<RentalDTO>> GetRental(string id)
        {
            var rental = await _rentalService.GetRentalAsync(id);
            if (rental == null) return NotFound();
            return Ok(rental);
        }

        [HttpPut("return/{id}")]
        public async Task<IActionResult> ReturnRental(string id)
        {
            var rentalDTO = await _rentalService.GetRentalAsync(id);
            if (rentalDTO == null)
                return NotFound("Rental not found");

            rentalDTO.EndDate = DateTime.UtcNow;

            var result = await _rentalService.UpdateRentalAsync(rentalDTO);

            if (!result)
                return BadRequest("Failed to update rental");

            // Make item available again
            await _itemService.UpdateAvailabilityAsync(rentalDTO.ItemId, true);

            return Ok();
        }


        [HttpGet("user/{userId}")]
        public async Task<IEnumerable<RentalDTO>> GetByUserId(string userId)
        {
            var rentals = await _rentalService.GetByUserIdAsync(userId);
            var rentalDTOs = new List<RentalDTO>();

            foreach (var rental in rentals)
            {
                var item = await _itemService.GetByIdAsync(rental.ItemId);
                rentalDTOs.Add(DTOMapper.ToDTO(rental, item));
            }

            return rentalDTOs;
        }

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
                RentalDate = DateTime.UtcNow,
                StartCondition = "OK",
                Price = item.Value
            };

            await _rentalService.CreateAsync(rental);
            await _itemService.UpdateAvailabilityAsync(request.ItemId, false);

            return Ok(DTOMapper.ToDTO(rental, item));
        }



    }
}