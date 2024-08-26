using HotelReservationSystem.Service.Services.FacilityService.Dtos;

namespace HotelReservationSystem.Service.Services.FacilityService
{
    public interface IFacilityService
    {
        Task<FacilityDto> CreateFacilityAsync(FacilityDto facilityDto);
        Task<decimal> CalculateFacilitiesPriceAsync(List<int> facilityIds);
    }
}




