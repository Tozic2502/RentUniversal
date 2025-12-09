using Microsoft.AspNetCore.Mvc;
using RentUniversal.Application.DTOs;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.Mapper;
using RentUniversal.Domain.Entities;

namespace RentUniversal.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("Users API is running");
        }

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllItems()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> RegisterUser([FromBody] CreateUserRequestDTO request)
        {
            // Basic format validation (API boundary)
            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest("Name is required");

            if (string.IsNullOrWhiteSpace(request.Email) || !request.Email.Contains('@'))
                return BadRequest("Valid email is required");

            if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 6)
                return BadRequest("Password must be at least 6 characters");

            // Call service
            var result = await _userService.RegisterAsync(request);

            return CreatedAtAction(nameof(GetUserById),
                new { id = result.Id },
                result);
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<UserDTO>> Authenticate([FromBody] LoginDTO login)
        {
            var user = await _userService.AuthenticateAsync(login.Email, login.Password);

            if (user == null)
                return Unauthorized("Invalid email or password");

            return Ok(user);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<UserDTO>> UpdateUser(string id, [FromBody] UserDTO updatedUser)
        {
            var user = await _userService.UpdateUserAsync(id, updatedUser);
            if (user == null) return NotFound("User not found");

            return Ok(user);
        }

        [HttpPut("{id}/password")]
        public async Task<ActionResult> ChangePassword(string id, [FromBody] ChangePasswordDTO dto)
        {
            var result = await _userService.ChangePasswordAsync(id, dto.OldPassword, dto.NewPassword);

            if (!result)
                return Unauthorized("Forkert nuværende password");

            return Ok("Password opdateret");
        }

    }
}
