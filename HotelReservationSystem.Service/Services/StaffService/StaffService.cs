
using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Repository.Repository;
using HotelReservationSystem.Service.Services.Helper;
using HotelReservationSystem.Service.Services.StaffService.Dtos;
using HotelReservationSystem.Service.Services.UserService.Dtos;

namespace HotelReservationSystem.Service.Services.StaffService
{
    public class StaffService : IStaffService
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StaffService(
            IunitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UserToReturnDto> RegisterStaffAsync(RegisterStaffDto registerDto)
        {
            var result = new UserToReturnDto()
            {
                IsSuccessed =false,
            };
            var user = await CreateUserAsync(registerDto);
            if (user == null)
                return result;

            await CreateStaffAsync(user, registerDto.Position);

            await _unitOfWork.SaveChangesAsync();

            
            return _mapper.Map<UserToReturnDto>(user);
        }
        private async Task<User> CreateUserAsync(RegisterStaffDto registerDto)
        {
            var existingUser = (await _unitOfWork.Repository<User>().GetAsync(u => u.Email == registerDto.Email)).FirstOrDefault();

            if (existingUser != null)
                return null; 

            var user = new User
            {
                Displayname = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.Phone,
                Username = registerDto.Email.Split("@")[0],
                PasswordHash = PasswordHasher.HashPassword(registerDto.Password)
            };

            await _unitOfWork.Repository<User>().AddAsync(user);
            return user;
        }
        private async Task CreateStaffAsync(User user, string position)
        {
            var staff = new Staff
            {
                FirstName = user.Displayname.Split(" ")[0],
                LastName = user.Displayname.Split(" ").Length > 1 ? user.Displayname.Split(" ")[1] : string.Empty,
                Position = position
            };

            await _unitOfWork.Repository<Staff>().AddAsync(staff);
        }
    }
}
