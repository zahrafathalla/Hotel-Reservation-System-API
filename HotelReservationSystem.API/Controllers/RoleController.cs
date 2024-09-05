using HotelReservationSystem.Service.Services.RoleService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddRole(string roleName)
        {
            return Ok(await _roleService.AddRoleAsync(roleName));
        }

        [HttpDelete("{roleId}")]
        public async Task<ActionResult<bool>> DeleteRole(int roleId)
        {
            return Ok(await _roleService.RemoveRoleAsync(roleId));
        }
    }
}
