using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;

namespace HotelReservationSystem.Service.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly IunitOfWork _unitOfWork;

        public RoleService(IunitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> AddRoleAsync(string roleName)
        {
            var role = await _unitOfWork.Repository<Role>().GetAsync(r => r.Name == roleName);
            if(role.Any())
                return false;

            await _unitOfWork.Repository<Role>().AddAsync(new Role { Name = roleName });
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveRoleAsync(string roleName)
        {
            var role = (await _unitOfWork.Repository<Role>().GetAsync(r => r.Name == roleName)).FirstOrDefault();
            if (role == null)
                return false;

            _unitOfWork.Repository<Role>().Delete(role);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
