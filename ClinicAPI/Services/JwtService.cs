using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ClinicAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace ClinicAPI.Services
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");

            var key = jwtSettings["Key"];

            var issuer = jwtSettings["Issuer"];

            var audience = jwtSettings["Audience"];

            var duration =
                int.Parse(jwtSettings["DurationInMinutes"]!);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                new Claim(ClaimTypes.Name, user.Username),

                new Claim("UserId", user.Id.ToString())
            };

            var securityKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(key!));

            var credentials =
                new SigningCredentials(
                    securityKey,
                    SecurityAlgorithms.HmacSha256);

            var token =
                new JwtSecurityToken(
                    issuer,
                    audience,
                    claims,
                    expires: DateTime.Now.AddMinutes(duration),
                    signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}