using HotelReservationSystem.API.Errors;

using Microsoft.AspNetCore.Mvc;
using HotelReservationSystem.Service.Services.OfferRoomsServices;
using HotelReservationSystem.Service.Services.OfferRoomsServices.Dtos;

namespace HotelReservationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferRoomsController : ControllerBase
    {
        private readonly IOfferRooms _offerRoomService;

        public OfferRoomsController(IOfferRooms offerRoomService)
        {
            _offerRoomService = offerRoomService;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddOfferToRooms(OfferRoomsDto OfferRoom)
        {
            var OfferRooms = await _offerRoomService.AddOfferRoomAsync(OfferRoom);
            if (OfferRooms == null)
                return BadRequest(new ApiResponse(400));
            return Ok(OfferRoom);
        }

        [HttpPut]
        public async Task<ActionResult<OfferRoomsDto>> UpdateOfferToRooms(int id, OfferRoomsDto offerRoomDto)
        {
            var OfferRoom = await _offerRoomService.UpdateOfferRoomAsync(id, offerRoomDto);
            if (OfferRoom == null)
                return BadRequest(new ApiResponse(400));
            return Ok(OfferRoom);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteOfferToRooms(int id)
        {
            return Ok(await _offerRoomService.DeleteOfferAsync(id));
        }
    }
}
