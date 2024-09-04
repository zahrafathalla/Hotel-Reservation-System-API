using HotelReservationSystem.Service.Services.UserService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Service.Services.UserService
{
    public interface IUserService
    {
        Task<UserToReturnDto> LoginAsCustomer(LoginDto loginDto);
        Task<UserToReturnDto> LoginAsStaff(LoginDto loginDto);
        Task<UserToReturnDto> LoginAsAdmin(LoginDto loginDto);
        Task<UserToReturnDto> RegisterAsCustomer(RegisterDto registerDto);

    }
}
