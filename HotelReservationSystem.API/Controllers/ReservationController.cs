using HotelReservationSystem.API.Errors;
using HotelReservationSystem.Service.Services.ReservationService;
using HotelReservationSystem.Mediator.ReservationMediator;
using HotelReservationSystem.Service.Services.ReservationService.Dtos;
using Microsoft.AspNetCore.Mvc;
using HotelReservationSystem.Data.Entities;
using Microsoft.AspNetCore.Authorization;

namespace HotelReservationSystem.API.Controllers
{
   
    public class ReservationController : BaseController
    {
        private readonly IReservationMediator _reservationMediator;
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationMediator reservationMediator, IReservationService reservationService)
        {
            _reservationService = reservationService;
            _reservationMediator = reservationMediator;

        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<ActionResult<ReservationCreatedToReturnDto>> MakeReservation(ReservationDto reservationDto)
        {
            var reservation = await _reservationMediator.CreateReservationAsync(reservationDto);
            if (reservation == null)
                return BadRequest(new ApiResponse(400));
            return Ok(reservation);
        }

        [Authorize(Policy = "GeneralPolicy")]
        [HttpGet("{Id}")]
        public async Task<ActionResult<Reservation>> GetReservaionDetails(int Id)
        {
            var reservation = await _reservationService.ViewReservationDetailsAsync(Id);
            if (reservation == null)
                return NotFound();
            return Ok(reservation);
        }

        [Authorize(Policy = "GeneralPolicy")]
        [HttpDelete("{Id}")]
        public async Task<ActionResult<bool>> CancelReservationAsync(int Id)
        {
            return Ok(await _reservationService.CancelReservationAsync(Id));
        }

        [Authorize(Roles = "Customer")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ReservationCreatedToReturnDto>> UpdateResevation(int id, ReservationUpdatedDto reservationDto)
        {
            var reservation = await _reservationMediator.UpdateReservationAsync(id, reservationDto);
            if (reservation is null) return BadRequest();

            return Ok(reservation);
        }
    }
}
