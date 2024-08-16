using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Service.Services.FacilityService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Service.Services.FacilityService
{
    public class FacilityService : IFacilityService
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FacilityService(
            IunitOfWork unitOfWork,
            IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FacilityDto> CreateFacilityAsync(FacilityDto facilityDto)
        {
            var newFacility = new Facility
            {
                Name = facilityDto.Name,
                Description = facilityDto.Description,
                Price = facilityDto.Price             
            };

            await _unitOfWork.Repository<Facility>().AddAsync(newFacility);
            await _unitOfWork.CompleteAsync();

            var mappedFacility = _mapper.Map<FacilityDto>(newFacility);

            return mappedFacility;
        }
    }
}
