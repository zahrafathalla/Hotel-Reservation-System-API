

using HotelReservationSystem.Service.Services.RoomService.Dtos;

namespace HotelReservationSystem.Mediator.RoomMediator
{
    public interface IRoomMediator
    {
        Task<RoomToReturnDto> AddRoomAsync (RoomDto roomDto);

        Task<RoomToReturnDto> UpdateRoomAsync(int id, RoomDto roomDto);

    }
}
