using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelReservationSystem.Service.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public TokenService(
            IunitOfWork unitOfWork,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<string> GenerateTokenAsync(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Displayname),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var roles = await GetRolesAsync(user.Id);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:AuthKey"]));

            var token = new JwtSecurityToken
            (
                audience: _configuration["JWT:ValidAudience"],
                issuer: _configuration["JWT:ValidIssuer"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDays"])) ,
                claims : claims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private async Task<IEnumerable<string>> GetRolesAsync(int userId)
        {
            var userRoles = await _unitOfWork.Repository<UserRole>()
                .GetAsync(ur => ur.UserId == userId);

            var roleIds = userRoles.Select(ur => ur.RoleId).ToList();

            var roles = await _unitOfWork.Repository<Role>()
                .GetAsync(r => roleIds.Contains(r.Id));

            return roles.Select(r => r.Name);
        }

    }
}
