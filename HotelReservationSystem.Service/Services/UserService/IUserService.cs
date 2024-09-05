using HotelReservationSystem.Service.Services.UserService.Dtos;

namespace HotelReservationSystem.Service.Services.UserService
{
    public interface IUserService
    {
        Task<UserToReturnDto> LoginAsCustomerAsync(LoginDto loginDto);
        Task<UserToReturnDto> LoginAsStaffAsync(LoginDto loginDto);
        Task<UserToReturnDto> LoginAsAdminAsync(LoginDto loginDto);
        Task<UserToReturnDto> RegisterAsCustomerAsync(RegisterDto registerDto);

    }
}
