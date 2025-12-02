using Microsoft.AspNetCore.Mvc;
using RentUniversal.Application.DTOs;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.Mappers;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> RegisterUser([FromBody] LoginDTO register)
        {
            var user = new User
            {
                Name = register.Name,
                Email = register.Email
            };

            var createdUser = await _userService.RegisterAsync(user, register.Password);

            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }



        [HttpPost("authenticate")]
        public async Task<ActionResult<UserDTO>> AuthenticateAndGetUser([FromBody] LoginDTO login)
        {
            var authUser = await _userService.AuthenticateAsync(login.Email, login.Password);

            if (authUser == null)
                return Unauthorized();

            return Ok(authUser);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDTO>> UpdateUser(string id, [FromBody] UserDTO updatedUser)
        {
            var user = await _userService.UpdateUserAsync(id, updatedUser);
            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }



    }
}
