using HotelReservationSystem.Service.Services.RoleUserService;
using HotelReservationSystem.Service.Services.StaffService.Dtos;
using HotelReservationSystem.Service.Services.UserService;
using HotelReservationSystem.Service.Services.UserService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Mediator.UserMediator
{
    public class UserMediator : IUserMediator
    {
        private readonly IUserRoleService _userRoleService;
        private readonly IUserService _userService;

        public UserMediator(
            IUserRoleService userRoleService,
            IUserService userService)
        {
            _userRoleService = userRoleService;
            _userService = userService;
        }
        public async Task<UserToReturnDto> RegisterCustomerAsync(RegisterDto registerDto)
        {
            var result = new UserToReturnDto()
            {
                IsSuccessed = false,
            };
            var user = await _userService.RegisterAsCustomerAsync(registerDto);

            if (user == null)
                return result;

            var roleAssigned = await _userRoleService.AddRoleToUserAsync(user.Id, "Customer");

            if (!roleAssigned)
                return result;

            return user;
        }
    }
}
