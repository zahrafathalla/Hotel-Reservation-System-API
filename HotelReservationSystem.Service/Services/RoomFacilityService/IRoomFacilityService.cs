
namespace HotelReservationSystem.Service.Services.RoomFacilityService
{
    public interface IRoomFacilityService
    {
        Task AddFacilitiesToRoomAsync(int roomId, List<int> facilityIds);

        Task UpdateFacilitiesInRoomAsync(int roomId, List<int> facilityIds);
        
        Task RemoveFacilityFromRoomAsync(int roomId, List<int> facilityIds);

    }
}
