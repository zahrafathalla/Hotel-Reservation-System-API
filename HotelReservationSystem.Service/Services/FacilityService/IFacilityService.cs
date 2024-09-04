using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Specification.RoomSpecifications;
using HotelReservationSystem.Repository.Specification.Specifications;
using HotelReservationSystem.Service.Services.FacilityService.Dtos;

namespace HotelReservationSystem.Service.Services.FacilityService
{
    public interface IFacilityService
    {
        Task<IEnumerable<FacilityToReturnDto>> GetAllFacilitiesAsync(SpecParams specParams);
        Task<FacilityToReturnDto> UpdateFacilityAsync(int id, FacilityDto facilityDto);
        Task<decimal> CalculateFacilitiesPriceAsync(List<int> facilityIds);
        Task<FacilityToReturnDto> CreateFacilityAsync(FacilityDto facilityDto);
        Task<FacilityToReturnDto> GetFacilitiesByIdAsync(int id);
        Task<bool> DeleteFacilityAsync(int id);
        Task<int> GetCount(SpecParams spec);
        
    }
}




