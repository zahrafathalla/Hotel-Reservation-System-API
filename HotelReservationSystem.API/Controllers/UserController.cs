using HotelReservationSystem.Service.Services.UserService;
using HotelReservationSystem.Service.Services.UserService.Dtos;
using Microsoft.AspNetCore.Http;
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
            return Ok (result);
        }

    }
}
