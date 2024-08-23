using HotelReservationSystem.Mediator.ReservationMediator;
using HotelReservationSystem.Service.Services.ReservationService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReservationController :BaseController
    {
        private readonly IReservationMediator _reservationMediator;
        public ReservationController(IReservationMediator reservationMediator)
        {
            _reservationMediator = reservationMediator;

        }
        [HttpPut]
        public async Task<ActionResult<ReservationToReturnDto>> UpdateResevation(int id, ReservationDto reservationDto)
        {
            var reservation = await _reservationMediator.UpdateReservationAsync(id, reservationDto);
            if (reservation is null) return BadRequest();

            return Ok(reservation);

        }
    }
}
