
namespace HotelReservationSystem.Service.Services.RoomFacilityService
{
    public interface IRoomFacilityService
    {
        Task AddOrUpdateFacilitiesToRoomAsync(int roomId, List<int> facilityIds);
        Task RemoveFacilityFromRoomAsync(int roomId, List<int> facilityIds);

    }
}
