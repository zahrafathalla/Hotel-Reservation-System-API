using HotelReservationSystem.API.Errors;
using HotelReservationSystem.Service.Services.UserService;
using HotelReservationSystem.Service.Services.UserService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.API.Controllers
{

    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost ("register")]
        public async Task<ActionResult<UserToReturnDto>> Register (RegisterDto model)
        {
            var result = await _userService.Register (model);
            if (result == null) 
                return BadRequest (new ApiResponse(400));
            return Ok (result);
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserToReturnDto>> CustomerLogin(LoginDto model)
        {
            var result = await _userService.LoginAsCustomer(model);
            if (result == null)
                return BadRequest(new ApiResponse(400));
            return Ok(result);
        }


    }
}
