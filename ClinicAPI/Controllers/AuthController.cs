using BCrypt.Net;
using ClinicAPI.Data;
using ClinicAPI.DTOs;
using ClinicAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly JwtService _jwtService;

        public AuthController(
            ApplicationDbContext context,
            JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(
                    u => u.Username == request.Username);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            bool valid =
                BCrypt.Net.BCrypt.Verify(
                    request.Password,
                    user.PasswordHash);

            if (!valid)
            {
                return Unauthorized("Invalid username or password.");
            }

            var token =
                _jwtService.GenerateToken(user);

            return Ok(new LoginResponse
            {
                Token = token
            });
        }
    }
}