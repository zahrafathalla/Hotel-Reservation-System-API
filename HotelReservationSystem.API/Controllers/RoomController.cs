using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Mediator.RoomMediator;
using HotelReservationSystem.Service.Services.RoomService;
using HotelReservationSystem.Service.Services.RoomService.Dtos;

using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RoomController : BaseController
    {
        private readonly IRoomMediator _roomMediator;
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;
        public RoomController(IRoomMediator roomMediator, IRoomService roomService, IMapper mapper)
        {
            _roomMediator = roomMediator;
            _roomService = roomService;
            _mapper = mapper;
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomToReturnDto>>> GetAllRooms()
        {
            var rooms = await _roomService.GetAllAsync();
            return Ok(rooms);
        }
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> a27e02cbeb230b4d3ab68ae388a9f06a80d9dc91
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var result = await _roomService.DeleteRoomAsync(id);
            if (!result)
            {
                return NotFound();  
            }

            return NoContent();  
        }
<<<<<<< HEAD
=======
=======
>>>>>>> 54960b0c497297f35f6062eb668b0534b6f04ef2
>>>>>>> a27e02cbeb230b4d3ab68ae388a9f06a80d9dc91
    }
}
