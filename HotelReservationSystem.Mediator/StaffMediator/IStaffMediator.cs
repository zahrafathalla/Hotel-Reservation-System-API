using HotelReservationSystem.Service.Services.StaffService.Dtos;
using HotelReservationSystem.Service.Services.UserService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Mediator.StaffMediator
{
    public interface IStaffMediator
    {
        Task<UserToReturnDto> RegisterStaffAsync(RegisterStaffDto registerDto);
    }
}
