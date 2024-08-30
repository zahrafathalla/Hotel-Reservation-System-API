using HotelReservationSystem.Data.Entities;


namespace HotelReservationSystem.Service.Services.TokenService
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(User user);
    }
}
