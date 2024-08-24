using HotelReservationSystem.API.Errors;
using HotelReservationSystem.Service.Services.ReservationService;
using HotelReservationSystem.Mediator.ReservationMediator;
using HotelReservationSystem.Service.Services.ReservationService.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReservationController : BaseController
    {
        private readonly IReservationMediator _reservationMediator;
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationMediator reservationMediator, IReservationService reservationService)
        {
            _reservationService = reservationService;
            _reservationMediator = reservationMediator;

        }
        [HttpPost]
        public async Task<ActionResult<ReservationCreatedToReturnDto>> MakeReservation(ReservationDto reservationDto)
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
        [HttpPut]
        public async Task<ActionResult<ReservationCreatedToReturnDto>> UpdateResevation(int id, ReservationDto reservationDto)
        {
            var reservation = await _reservationMediator.UpdateReservationAsync(id, reservationDto);
            if (reservation is null) return BadRequest();

            return Ok(reservation);
        }
    }
}
