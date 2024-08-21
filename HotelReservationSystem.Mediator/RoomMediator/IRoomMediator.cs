

using HotelReservationSystem.Service.Services.RoomService.Dtos;

namespace HotelReservationSystem.Mediator.RoomMediator
{
    public interface IRoomMediator
    {
        Task<RoomcreatedToReturnDto> AddRoomAsync (RoomDto roomDto);

        Task<RoomcreatedToReturnDto> UpdateRoomAsync(int id, RoomDto roomDto);
        Task<bool> RemoveRoomAsync(int id);

    }
}
