using AutoMapper;
using HotelReservationSystem.API.Errors;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Mediator.RoomMediator;
using HotelReservationSystem.Repository.Specification.RoomSpecifications;
using HotelReservationSystem.Service.Services.RoomService;
using HotelReservationSystem.Service.Services.RoomService.Dtos;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HotelReservationSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RoomController : BaseController
    {
        private readonly IRoomMediator _roomMediator;
        private readonly IRoomService _roomService;
        public RoomController(
            IRoomMediator roomMediator, 
            IRoomService roomService)
        {
            _roomMediator = roomMediator;
            _roomService = roomService;
        }


        [ProducesResponseType(typeof(RoomToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<RoomToReturnDto>> AddRoom(RoomDto roomDto)
        {
            var room = await _roomMediator.AddRoomAsync(roomDto);
            if (room == null)
                return BadRequest(new ApiResponse(400));

            return Ok(room);
        }

        [HttpPost("picture")]
        public async Task<ActionResult<bool>> AddPictures(PictureDto pictureDto)
        {
            var picture = await _roomService.AddPicturesToRoomAsync(pictureDto);
            return Ok(picture);
        }

        [ProducesResponseType(typeof(RoomToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public async Task<ActionResult<RoomToReturnDto>> UpdatRoom(int id, RoomDto roomDto)
        {
            var Room = await _roomMediator.UpdateRoomAsync(id, roomDto);
            if (Room is null) 
                return BadRequest(new ApiResponse(400));

            return Ok(Room);
        }

        [ProducesResponseType(typeof(RoomToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomToReturnDto>>> GetAllRooms([FromQuery] RoomSpecParams roomSpec)
        {
            var rooms = await _roomService.GetAllAsync(roomSpec);
            return Ok(rooms);
        }

        [ProducesResponseType(typeof(RoomToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomToReturnDto>>> GetAllRoomsAvalibale()
        {
            var rooms = await _roomService.GetAllRoomsIsAvaliableAsync();
            if (rooms is null) return NotFound(new ApiResponse(404));
            return Ok(rooms);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteRoom(int id)
        {
            return Ok( await _roomMediator.RemoveRoomAsync(id));

        }

    }
}
