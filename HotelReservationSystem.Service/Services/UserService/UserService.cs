﻿using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Service.Services.TokenService;
using HotelReservationSystem.Service.Services.UserService.Dtos;

namespace HotelReservationSystem.Service.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public UserService(
            IunitOfWork unitOfWork,
            ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }
        public async Task<UserToReturnDto> LoginAsCustomer(LoginDto loginDto)
        {
            var result = new UserToReturnDto()
            {
                IsSuccessed = false,
            };
            var user = (await _unitOfWork.Repository<User>()
                            .GetAsync(u => u.Email == loginDto.Email)).FirstOrDefault();

            if (user == null || user.PasswordHash != loginDto.Password)
                return result;

            return new UserToReturnDto()
            {
                DisplayName = user.Displayname,
                Email = user.Email,
                Token = await _tokenService.GenerateTokenAsync(user)
            };
        }

        public Task<UserToReturnDto> LoginAsStaff(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public async Task<UserToReturnDto> Register(RegisterDto registerDto)
        {
            var user = new User()
            {
                Displayname = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.Phone,
                Username = registerDto.Email.Split("@")[0],
                PasswordHash = registerDto.Password
            };

            await _unitOfWork.Repository<User>().AddAsync(user);

            var customer = new Customer()
            {
                FirstName = user.Displayname.Split(" ")[0],
                LastName = user.Displayname.Split(" ").Length > 1? user.Displayname.Split(" ")[1] : string.Empty 
            };

            await _unitOfWork.Repository<Customer>().AddAsync(customer);
            await _unitOfWork.CompleteAsync();

            return new UserToReturnDto()
            {
                DisplayName = user.Displayname,
                Email = user.Email,
                Token = await _tokenService.GenerateTokenAsync(user)
            };

        }
    }
}
