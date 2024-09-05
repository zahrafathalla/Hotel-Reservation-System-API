using HotelReservationSystem.Service.Services.StaffService.Dtos;
using HotelReservationSystem.Service.Services.UserService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Mediator.UserMediator
{
    public interface IUserMediator
    {
        Task<UserToReturnDto> RegisterCustomerAsync(RegisterDto registerDto);

    }
}
