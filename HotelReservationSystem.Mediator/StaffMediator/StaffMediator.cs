using HotelReservationSystem.Service.Services.RoleUserService;
using HotelReservationSystem.Service.Services.StaffService;
using HotelReservationSystem.Service.Services.StaffService.Dtos;
using HotelReservationSystem.Service.Services.UserService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Mediator.StaffMediator
{
    public class StaffMediator : IStaffMediator
    {
        private readonly IStaffService _staffService;
        private readonly IUserRoleService _userRoleService;

        public StaffMediator(
            IStaffService staffService,
            IUserRoleService userRoleService)
        {
            _staffService = staffService;
            _userRoleService = userRoleService;
        }
        public async Task<UserToReturnDto> RegisterStaffAsync(RegisterStaffDto registerDto)
        {
            var result = new UserToReturnDto()
            {
                IsSuccessed = false,
            };
            var user = await _staffService.RegisterStaffAsync(registerDto);

            if (user == null)
                return result;

            var roleAssigned = await _userRoleService.AddRoleToUserAsync(user.Id, "Staff");

            if(!roleAssigned)
                return result;

            return user;
        }
    }
}
