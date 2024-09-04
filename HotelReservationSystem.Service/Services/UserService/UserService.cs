using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Repository.Specification.UserSpecifications;
using HotelReservationSystem.Service.Services.Helper;
using HotelReservationSystem.Service.Services.TokenService;
using HotelReservationSystem.Service.Services.UserService.Dtos;

namespace HotelReservationSystem.Service.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UserService(
            IunitOfWork unitOfWork,
            ITokenService tokenService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        public async Task<UserToReturnDto> LoginAsCustomer(LoginDto loginDto)
        {
            var result = new UserToReturnDto()
            {
                IsSuccessed = false,
            };
            var user = (await _unitOfWork.Repository<User>()
                            .GetAsync(u => u.Email == loginDto.Email)).FirstOrDefault();

            if (user == null || !( PasswordHasher.checkPassword(loginDto.Password , user.PasswordHash)))
                return result;

            var mappedUser = _mapper.Map<UserToReturnDto>(user);
            mappedUser.Token = await _tokenService.GenerateTokenAsync(user);

            return mappedUser;
        }

        public async Task<UserToReturnDto> LoginAsStaff(LoginDto loginDto)
       
        {
            var result = new UserToReturnDto()
            {
                IsSuccessed = false,
            };
            var user = (await _unitOfWork.Repository<User>()
                            .GetAsync(u => u.Email == loginDto.Email)).FirstOrDefault();

            if (user == null || !(PasswordHasher.checkPassword(loginDto.Password, user.PasswordHash)))
                return result;

            var mappedUser = _mapper.Map<UserToReturnDto>(user);
            mappedUser.Token = await _tokenService.GenerateTokenAsync(user);

            return mappedUser;
        }
        public async Task<UserToReturnDto> LoginAsAdmin(LoginDto loginDto)
        {
            var result = new UserToReturnDto()
            {
                IsSuccessed = false,
            };

            var userSpec = new UserSpecification(loginDto.Email);

            var user = (await _unitOfWork.Repository<User>().GetAllWithSpecAsync(userSpec))?.FirstOrDefault();

            if (user == null || !PasswordHasher.checkPassword(loginDto.Password, user.PasswordHash))
                return result;

            var isAdmin = user.UserRoles.Any(ur => ur.Role.Name == "Admin");

            if (!isAdmin)
                return result;

            var mappedUser = _mapper.Map<UserToReturnDto>(user);
            mappedUser.Token = await _tokenService.GenerateTokenAsync(user);

            return mappedUser;
        }


        public async Task<UserToReturnDto> RegisterAsCustomer(RegisterDto registerDto)
        {
            var result = new UserToReturnDto()
            {
                IsSuccessed = false,
            };
            var user = await CreateUserAsync(registerDto);
            if (user == null)
                return result;

            await CreateCustomerAsync(user);

            await _unitOfWork.SaveChangesAsync();

            var mappedUser = _mapper.Map<UserToReturnDto>(user);
            mappedUser.Token = await _tokenService.GenerateTokenAsync(user);

            return mappedUser;
        }

        private async Task<User> CreateUserAsync(RegisterDto registerDto)
        {
            var existingUser = (await _unitOfWork.Repository<User>().GetAsync(u => u.Email == registerDto.Email)).FirstOrDefault();

            if (existingUser != null)
                return null;

            var user = new User()
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

        private async Task CreateCustomerAsync(User user)
        {
            var customer = new Customer()
            {
                FirstName = user.Displayname.Split(" ")[0],
                LastName = user.Displayname.Split(" ").Length > 1 ? user.Displayname.Split(" ")[1] : string.Empty
            };

            await _unitOfWork.Repository<Customer>().AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();

        }
    }
}
