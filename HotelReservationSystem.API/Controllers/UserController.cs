using HotelReservationSystem.API.Errors;
using HotelReservationSystem.Mediator.UserMediator;
using HotelReservationSystem.Service.Services.UserService;
using HotelReservationSystem.Service.Services.UserService.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.API.Controllers
{

    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IUserMediator _userMediator;

        public UserController(
            IUserService userService,
            IUserMediator userMediator)
        {
            _userService = userService;
            _userMediator = userMediator;
        }
        [HttpPost ("register")]
        public async Task<ActionResult<UserToReturnDto>> Register (RegisterDto model)
        {
            var result = await _userMediator.RegisterCustomerAsync(model);
            if (result == null) 
                return BadRequest (new ApiResponse(400));
            return Ok (result);
        }
        [HttpPost("customerLogin")]
        public async Task<ActionResult<UserToReturnDto>> CustomerLogin(LoginDto model)
        {
            var result = await _userService.LoginAsCustomerAsync(model);
            if (result == null)
                return BadRequest(new ApiResponse(400));
            return Ok(result);
        }
        [HttpPost("staffLogin")]
        public async Task<ActionResult<UserToReturnDto>> StaffLogin(LoginDto model)
        {
            var result = await _userService.LoginAsStaffAsync(model);
            if (result == null)
                return BadRequest(new ApiResponse(400));
            return Ok(result);
        }
        [HttpPost("adminLogin")]
        public async Task<ActionResult<UserToReturnDto>> AdminLogin(LoginDto model)
        {
            var result = await _userService.LoginAsAdminAsync(model);
            if (result == null)
                return BadRequest(new ApiResponse(400));
            return Ok(result);
        }


    }
}
