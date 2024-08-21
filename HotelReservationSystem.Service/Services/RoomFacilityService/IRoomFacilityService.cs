
namespace HotelReservationSystem.Service.Services.RoomFacilityService
{
    public interface IRoomFacilityService
    {
        Task AddFacilitiesToRoomAsync(int roomId, List<int> facilityIds);
        Task<bool> RemoveFacilityFromRoomAsync(int roomId);

    }
}
