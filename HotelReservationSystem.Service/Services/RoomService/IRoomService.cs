using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Specification.RoomSpecifications;
using HotelReservationSystem.Repository.Specification.Specifications;
using HotelReservationSystem.Service.Services.RoomService.Dtos;
using Microsoft.AspNetCore.Http;

namespace HotelReservationSystem.Service.Services.RoomService
{
    public interface IRoomService
    {
        Task<Room> AddRoomAsync(RoomDto roomDto);
        Task<bool> AddPicturesToRoomAsync(List<IFormFile> pictureUrls, int roomId);
        Task<Room> UpdateRoomAsync(int id, RoomDto room);
        Task<IEnumerable<RoomToReturnDto>> GetAllRoomsIsAvaliableAsync(SpecParams roomSpec, DateTime checkInDate, DateTime checkOutDate);
        Task<int> GetAvailableRoomCount(DateTime checkInDate, DateTime checkOutDate);
        Task<bool> DeleteRoomAsync(int id);
        Task<decimal> GetRoomPriceAsync(int id);
        Task<RoomToReturnDto> GetRoomByIDAsync(int id);
    }
}
