using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using System.Security.Cryptography;
using System.Text;

namespace HotelReservationSystem.Repository
{
    public class ApplicationContextSeed
    {
        public static async Task SeedRolesAsync(IunitOfWork unitOfWork)
        {
            var roles = new[] { "Admin", "Customer", "Staff" };

            foreach (var role in roles)
            {
                var roleExist = await unitOfWork.Repository<Role>().GetAsync(r => r.Name == role);
                if (!roleExist.Any())
                {
                    await unitOfWork.Repository<Role>().AddAsync(new Role { Name = role });
                }
            }

            var userRepository = unitOfWork.Repository<User>();

            var existingUser = (await userRepository.GetAsync(u => true)).Any();
            if (!existingUser)
            {
                var adminUser = new User
                {
                    Displayname = "Zahra Gamal",
                    Email = "zahra@gmail.com",
                    Username = "zahra",
                    PhoneNumber = "1234",
                    PasswordHash = HashPassword("password123@")
                };
                await userRepository.AddAsync(adminUser);
                await unitOfWork.SaveChangesAsync();

                var adminRole = (await unitOfWork.Repository<Role>().GetAsync(r => r.Name == "Admin")).FirstOrDefault();
                if (adminRole != null)
                {
                    var userRole = new UserRole
                    {
                        UserId = adminUser.Id,
                        RoleId = adminRole.Id
                    };
                    await unitOfWork.Repository<UserRole>().AddAsync(userRole);
                    await unitOfWork.SaveChangesAsync();
                }
            }

        }

        public static string HashPassword(string password)
        {
            var sha256 = SHA256.Create();

            var bytes = Encoding.UTF8.GetBytes(password);

            var hashBytes = sha256.ComputeHash(bytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    } 
}
