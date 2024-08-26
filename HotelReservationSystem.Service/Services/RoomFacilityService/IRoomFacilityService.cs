
using HotelReservationSystem.Data.Entities;

namespace HotelReservationSystem.Service.Services.RoomFacilityService
{
    public interface IRoomFacilityService
    {
        Task AddFacilitiesToRoomAsync(int roomId, List<int> facilityIds);
        Task<bool> RemoveFacilityFromRoomAsync(int roomId);
        Task<IEnumerable<Facility>> GetFacilitiesByRoomIdAsync(int roomId);
    }
}
