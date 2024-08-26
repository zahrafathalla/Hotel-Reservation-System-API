using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Specification.RoomSpecifications;
using HotelReservationSystem.Service.Services.RoomService.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Service.Services.RoomService
{
    public interface IRoomService
    {
        Task<Room> AddRoomAsync(RoomDto roomDto);
        Task<bool> AddPicturesToRoomAsync(PictureDto pictureDto);
        Task<Room> UpdateRoomAsync(int id, RoomDto room);
        Task<IEnumerable<RoomToReturnDto>> GetAllAsync(RoomSpecParams specParams);
        Task<IEnumerable<RoomToReturnDto>> GetAllRoomsIsAvaliableAsync();
        Task<bool> DeleteRoomAsync(int id);
        Task<decimal> GetRoomPriceAsync(int id);
        Task<int> GetCount(RoomSpecParams spec);
        Task<RoomToReturnDto> GetRoomByIDAsync(int id);
    }
}
