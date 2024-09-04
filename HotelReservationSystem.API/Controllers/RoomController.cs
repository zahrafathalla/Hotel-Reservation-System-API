using HotelReservationSystem.API.Errors;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Mediator.RoomMediator;
using HotelReservationSystem.Repository.Specification.Specifications;
using HotelReservationSystem.Service.Services.Helper.ResulteViewModel;
using HotelReservationSystem.Service.Services.RoomService;
using HotelReservationSystem.Service.Services.RoomService.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HotelReservationSystem.API.Controllers
{

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

        [Authorize(Policy = "AdminOrStaffPolicy")]
        [HttpPost]
        public async Task<ActionResult<RoomcreatedToReturnDto>> AddRoom(RoomDto roomDto)
        {
            var room = await _roomMediator.AddRoomAsync(roomDto);
            if (room == null)
                return BadRequest(new ApiResponse(400));

            return Ok(room);
        }
        [Authorize(Policy = "AdminOrStaffPolicy")]
        [HttpPost("picture/{roomId}")]
        public async Task<ActionResult<bool>> AddPictures(List<IFormFile> pictureUrls, int roomId)
        {
            var picture = await _roomService.AddPicturesToRoomAsync(pictureUrls, roomId);
            return Ok(picture);
        }

        [Authorize(Policy = "AdminOrStaffPolicy")]
        [HttpPut("{id}")]
        public async Task<ActionResult<RoomcreatedToReturnDto>> UpdatRoom(int id, RoomDto roomDto)
        {
            var Room = await _roomMediator.UpdateRoomAsync(id, roomDto);
            if (Room is null) 
                return BadRequest(new ApiResponse(400));

            return Ok(Room);
        }

        //[Authorize(Policy = "GeneralPolicy")]
        [HttpGet("Available")]
        public async Task<ActionResult<Pagination<RoomToReturnDto>>> GetAllRoomsAvailable([FromQuery] SpecParams roomSpec, DateTime checkInDate, DateTime checkOutDate)
        {
            var rooms = await _roomService.GetAllRoomsIsAvaliableAsync(roomSpec, checkInDate, checkOutDate);

            if (rooms is null)
                return NotFound(new ApiResponse(404));

            var count = await _roomService.GetAvailableRoomCount(checkInDate, checkOutDate);

            return Ok(new Pagination<RoomToReturnDto>(roomSpec.PageSize, roomSpec.PageIndex, count, rooms));
        }
        [Authorize(Policy = "AdminOrStaffPolicy")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteRoom(int id)
        {
            return Ok( await _roomMediator.RemoveRoomAsync(id));

        }

    }
}
