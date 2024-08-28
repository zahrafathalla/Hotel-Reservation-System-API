using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Specification.RoomSpecifications;
using HotelReservationSystem.Service.Services.RoomService.Dtos;

namespace HotelReservationSystem.Service.Services.RoomService
{
    public interface IRoomService
    {
        Task<Room> AddRoomAsync(RoomDto roomDto);
        Task<bool> AddPicturesToRoomAsync(PictureDto pictureDto);
        Task<Room> UpdateRoomAsync(int id, RoomDto room);
        Task<IEnumerable<RoomToReturnDto>> GetAllRoomsIsAvaliableAsync(RoomSpecParams roomSpec, DateTime checkInDate, DateTime checkOutDate);
        Task<int> GetAvailableRoomCount(RoomSpecParams roomSpec, DateTime checkInDate, DateTime checkOutDate);
        Task<bool> DeleteRoomAsync(int id);
        Task<decimal> GetRoomPriceAsync(int id);
        Task<RoomToReturnDto> GetRoomByIDAsync(int id);
    }
}
