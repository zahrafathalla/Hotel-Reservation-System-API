using HotelReservationSystem.Service.Services.RoleService;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.API.Controllers
{

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

        [HttpDelete]
        public async Task<ActionResult> DeleteRole(string roleName)
        {
            return Ok(await _roleService.RemoveRoleAsync(roleName));
        }
    }
}
