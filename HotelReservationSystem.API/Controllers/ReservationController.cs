using HotelReservationSystem.API.Errors;
using HotelReservationSystem.Service.Services.ReservationService;
using HotelReservationSystem.Service.Services.ReservationService.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.API.Controllers
{

    public class ReservationController : BaseController
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
        [HttpPost]
        public async Task<ActionResult<ReservationToReturnDto>> MakeReservation(ReservationDto reservationDto)
        {
           var reservation = await _reservationService.MakeReservationAsync(reservationDto);
            if (reservation == null)
                return BadRequest(new ApiResponse(400));
            return Ok(reservation);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<bool>> CancelReservationAsync(int Id)
        {
            return Ok(await _reservationService.CancelReservationAsync(Id));
        }
    }
}
