using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Service.Services.StaffService.Dtos;
using HotelReservationSystem.Service.Services.UserService.Dtos;

namespace HotelReservationSystem.Service.Services.StaffService
{
    public interface IStaffService
    {
        Task<UserToReturnDto> RegisterStaffAsync(RegisterStaffDto registerDto);
    }
}
