using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Repository.Specification.Facilitypecifications;
using HotelReservationSystem.Repository.Specification.FacilitySpecifications;
using HotelReservationSystem.Repository.Specification.Specifications;
using HotelReservationSystem.Service.Services.FacilityService.Dtos;

namespace HotelReservationSystem.Service.Services.FacilityService
{
    public class FacilityService : IFacilityService
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FacilityService(IunitOfWork unitOfWork,IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FacilityDto>> GetAllFacilitiesAsync(SpecParams Params)
        {
            var spec = new FacilitySpec(Params);
            var facility = await _unitOfWork.Repository<Facility>().GetAllWithSpecAsync(spec);
            var facilityMapped = _mapper.Map<IEnumerable<FacilityDto>>(facility);

            return facilityMapped;
        }
        public async Task<FacilityDto> UpdateFacilityAsync(int id, FacilityDto facilityDto)
        {
            var Spec = new FacilitySpec(id);
            var OldFacility = await _unitOfWork.Repository<Facility>().GetByIdWithSpecAsync(Spec);
            if (OldFacility == null) return null;

            OldFacility.Price = facilityDto.Price;
            OldFacility.Description = facilityDto.Description;
            OldFacility.Name = facilityDto.Name;

            _unitOfWork.Repository<Facility>().Update(OldFacility);
            await _unitOfWork.SaveChangesAsync();
            var mappedFacility = _mapper.Map<FacilityDto>(OldFacility);

            return mappedFacility;
        }
        public async Task<decimal> CalculateFacilitiesPriceAsync(List<int> facilityIds)
        {
            var facilities = await _unitOfWork.Repository<Facility>()
                     .GetAsync(f => facilityIds.Contains(f.Id));

            return facilities.Sum(f => f.Price);
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
            await _unitOfWork.SaveChangesAsync();

            var mappedFacility = _mapper.Map<FacilityDto>(newFacility);

            return mappedFacility;
        }
        public async Task<FacilityDto> GetFacilitiesByIdAsync(int id)
        {
            var spec = new FacilitySpec(id);
            var facility = await _unitOfWork.Repository<Facility>().GetByIdWithSpecAsync(spec);
            var facilityMapped = _mapper.Map<FacilityDto>(facility);

            return facilityMapped;
        }
        public async Task<bool> DeleteFacilityAsync(int id)
        {

            var Facility = await _unitOfWork.Repository<Facility>().GetByIdAsync(id);
            if (Facility == null) return false;

            _unitOfWork.Repository<Facility>().Delete(Facility);
            var Result = await _unitOfWork.SaveChangesAsync();

            return Result > 0;
        }
        public async Task<int> GetCount(SpecParams Spec)
        {
            var CountFacility = new CountFacilityWithSpec(Spec);
            var Count = await _unitOfWork.Repository<Facility>().GetCountWithSpecAsync(CountFacility);
            return Count;
        }

    }
}
