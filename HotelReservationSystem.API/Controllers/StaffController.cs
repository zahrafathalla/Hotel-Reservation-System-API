using HotelReservationSystem.API.Errors;
using HotelReservationSystem.Mediator.StaffMediator;
using HotelReservationSystem.Service.Services.StaffService.Dtos;
using HotelReservationSystem.Service.Services.UserService.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StaffController : BaseController
    {
        private readonly IStaffMediator _staffMediator;

        public StaffController(IStaffMediator staffMediator)
        {
            _staffMediator = staffMediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserToReturnDto>> RegisterStaff(RegisterStaffDto registerDto)
        {
            var staff = await _staffMediator.RegisterStaffAsync(registerDto);

            if (staff.IsSuccessed == false)
                return BadRequest(new ApiResponse(400));

            return Ok(staff);
        }
    }
}
