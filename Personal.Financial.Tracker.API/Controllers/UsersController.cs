using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personal.Financial.Tracker.Application.DTOs;
using Personal.Financial.Tracker.Application.Interfaces;

namespace Personal.Financial.Tracker.API.Controllers
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

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequestDto loginRequest)
        {
            var user = await _userService.Authenticate(loginRequest.Username, loginRequest.Password);

            if (user == null)
                return BadRequest("Username or password is incorrect");

            // If authentication succeeds, return the token
            return Ok(user.Token);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }
    }
}
