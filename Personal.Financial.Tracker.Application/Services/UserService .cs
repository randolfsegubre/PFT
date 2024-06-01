using Microsoft.Extensions.Configuration;
using Personal.Financial.Tracker.Application.DTOs;
using Personal.Financial.Tracker.Application.Helpers;
using Personal.Financial.Tracker.Application.Interfaces;

namespace Personal.Financial.Tracker.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<UserDto> Authenticate(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            var token = JwtTokenHelper.GenerateJwtToken(user.Username, user.Role, _configuration["Jwt:Key"], _configuration["Jwt:Issuer"]);

            return new UserDto
            {
                Username = user.Username,
                Token = token
            };
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();

            return users.Select(user => new UserDto
            {
                Username = user.Username,
                Role = user.Role
            });
        }
    }
}
