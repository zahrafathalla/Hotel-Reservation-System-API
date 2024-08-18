using HotelReservationSystem.Mediator.RoomMediator;
using HotelReservationSystem.Service.Services.RoomService.Dtos;

using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.API.Controllers
{
    public class RoomController : BaseController
    {
        private readonly IRoomMediator _roomMediator;

        public RoomController(IRoomMediator roomMediator)
        {
            _roomMediator = roomMediator;
        }
        [HttpPost]
        public async Task<ActionResult<RoomToReturnDto>> AddRoom(RoomDto roomDto)
        {
            var room = await _roomMediator.AddRoomAsync(roomDto);
            if (room == null)
                return BadRequest();

            return Ok(room);
        }
        [HttpPut]
        public async Task<ActionResult<RoomToReturnDto>> UpdatRoom(int id, RoomDto roomDto)
        {
            var Room = await _roomMediator.UpdateRoomAsync(id, roomDto);
            if (Room is null) return BadRequest();

            return Ok(Room);
        }
        //private readonly IRoomService _roomService;

        //public RoomController(IRoomService roomService)
        //{
        //    _roomService = roomService;
        //}

        //[HttpPost]
        //public async Task<ActionResult<RoomToReturnDto>> AddRoom(RoomDto roomDto)
        //{
        //    var room = await _roomService.AddRoomAsync(roomDto);
        //    if (room == null)
        //        return BadRequest();

        //    return Ok(room);
        //}

        //[HttpPut("Update Room")]
        //public async Task<ActionResult<RoomToReturnDto>> UpdatRoom(int id, RoomDto roomDto)
        //{
        //    var Room = await _roomService.UpdateRoomAsync(id, roomDto);
        //    if (Room is null) return BadRequest();

        //    return Ok(Room);
        //}
    }
}
