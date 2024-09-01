using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Service.Services.RoleUserService
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IunitOfWork _unitOfWork;

        public UserRoleService(IunitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> AddRoleToUserAsync(int userId, string roleName)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(userId);
            var role = (await _unitOfWork.Repository<Role>().GetAsync(r => r.Name == roleName)).FirstOrDefault();

            if (user == null || role== null)
                return false; 


            var userRole = new UserRole
            {
                UserId = userId,
                RoleId = role.Id
            };

            await _unitOfWork.Repository<UserRole>().AddAsync(userRole);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        public async Task<bool> RemoveRoleFromUserAsync(int userId, string roleName)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(userId);

            var role = (await _unitOfWork.Repository<Role>()
                .GetAsync(r => r.Name == roleName)).FirstOrDefault();

            if (user == null || role == null)
                return false; 

            var userRole = (await _unitOfWork.Repository<UserRole>()
                .GetAsync(ur => ur.UserId == userId && ur.RoleId == role.Id)).FirstOrDefault();

            if (userRole == null)
                return false;

            _unitOfWork.Repository<UserRole>().Delete(userRole);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        public async Task<IEnumerable<Role>> GetRolesForUserAsync(int userId)
        {
            var userRoles = await _unitOfWork.Repository<UserRole>()
                            .GetAsync(ur => ur.UserId == userId);

            var roleIds = userRoles.Select(ur => ur.RoleId);

            var Roles = await _unitOfWork.Repository<Role>()
                         .GetAsync(r => roleIds.Contains(r.Id));
            return Roles;
        }


    }
}
