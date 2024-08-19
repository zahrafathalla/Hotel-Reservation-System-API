using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Service.Services.RoomService.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Service.Services.RoomService
{
    public interface IRoomService
    {
        Task<Room> AddRoomAsync(RoomDto roomDto);
        Task<Room> UpdateRoomAsync(int id, RoomDto room);
        Task<IEnumerable<RoomToReturnDto>> GetAllAsync();
<<<<<<< HEAD
        Task<bool> DeleteRoomAsync(int id);
=======
>>>>>>> 54960b0c497297f35f6062eb668b0534b6f04ef2
    }
}
