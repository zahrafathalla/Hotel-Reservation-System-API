using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Service.Services.RoleService
{
    public interface IRoleService
    {
        Task<bool> AddRoleAsync(string roleName);
        Task<bool> RemoveRoleAsync(string roleName);
    }
}
