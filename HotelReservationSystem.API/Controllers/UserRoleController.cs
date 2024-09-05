using HotelReservationSystem.Service.Services.RoleUserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserRoleController : BaseController
    {
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddRoleToUser(int userId, string roleName)
        {
            return Ok(await _userRoleService.AddRoleToUserAsync(userId, roleName));
        }

        [HttpDelete("remove")]
        public async Task<ActionResult<bool>> RemoveRoleFromUser(int userId, string roleName)
        {
            return Ok(await _userRoleService.RemoveRoleFromUserAsync(userId, roleName));
        }

    }
}
