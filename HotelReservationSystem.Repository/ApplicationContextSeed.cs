using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;

namespace HotelReservationSystem.Repository
{
    public class ApplicationContextSeed
    {
        public static async Task SeedRolesAsync(IunitOfWork unitOfWork)
        {
            var roles = new[] {"Admin" ,"Customer", "Staff" };

            foreach (var role in roles)
            {
                var roleExist = await unitOfWork.Repository<Role>().GetAsync(r => r.Name == role);
                if (!roleExist.Any())
                {
                    await unitOfWork.Repository<Role>().AddAsync(new Role { Name = role });
                }
            }
            var adminEmail = "zahra@gmail.com";

            var adminUser = (await unitOfWork.Repository<User>().GetAsync(u => u.Email == adminEmail)).FirstOrDefault();

            var adminRole = (await unitOfWork.Repository<Role>().GetAsync(r => r.Name == "Admin")).FirstOrDefault();

            if (adminRole != null)
            {
                var userRoles = await unitOfWork.Repository<UserRole>().GetAsync(ur => ur.UserId == adminUser.Id && ur.RoleId == adminRole.Id);
                if (!userRoles.Any())
                {
                    await unitOfWork.Repository<UserRole>().AddAsync(new UserRole
                    {
                        UserId = adminUser.Id,
                        RoleId = adminRole.Id
                    });
                }
            }

            await unitOfWork.SaveChangesAsync();

        }

    }
}
