using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;

namespace HotelReservationSystem.Repository
{
    public class ApplicationContextSeed
    {
        public static async Task SeedRolesAsync(IunitOfWork unitOfWork)
        {
            var roles = new[] { "Customer", "Staff" };

            foreach (var role in roles)
            {
                var roleExist = await unitOfWork.Repository<Role>().GetAsync(r => r.Name == role);
                if (!roleExist.Any())
                {
                    await unitOfWork.Repository<Role>().AddAsync(new Role { Name = role });
                }
            }

            await unitOfWork.SaveChangesAsync();
        }
    }
}
