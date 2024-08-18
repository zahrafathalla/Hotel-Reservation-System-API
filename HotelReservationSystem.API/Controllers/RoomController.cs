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
        public async Task<ActionResult<IEnumerable<RoomViewModel>>> GetAllRooms()
        {
            var roomsDtos = await _roomService.GetAllAsync();
            var roomsViewModel = _mapper.Map<IEnumerable<RoomViewModel>>(roomsDtos);
            if (roomsViewModel is null) return BadRequest();

            return Ok(roomsViewModel);
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
