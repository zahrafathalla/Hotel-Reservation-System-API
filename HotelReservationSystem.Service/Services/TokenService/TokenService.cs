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

            var userRoles = await _unitOfWork.Repository<UserRole>()
                                    .GetAsync(u => u.UserId == user.Id);

            var roles = await _unitOfWork.Repository<Role>()
                                    .GetAsync(r => userRoles.Select(ur => ur.RoleId).Contains(r.Id));

            var roleNames = roles.Select(r => r.Name).ToList();

            foreach(var role in roleNames)
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
    }
}
