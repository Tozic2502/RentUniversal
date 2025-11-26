using Microsoft.AspNetCore.Mvc;
using RentUniversal.Application.Interfaces;
using RentUniversal.Domain.Entities;
using RentUniversal.Application.DTOs;

namespace RentUniversal.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> RegisterUser([FromBody] User user)
        {
            var result = await _userService.RegisterAsync(user, "testpassword");
            return CreatedAtAction(nameof(GetUserById), new { id = result.Id }, result);
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<UserDTO>> Authenticate([FromBody] string email)
        {
            var user = await _userService.AuthenticateAsync(email, "testpassword");
            if (user == null) return Unauthorized();
            return Ok(user);
        }
    }
}
