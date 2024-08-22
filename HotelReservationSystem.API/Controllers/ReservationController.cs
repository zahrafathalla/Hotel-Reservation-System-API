using AutoMapper;
using HotelReservationSystem.Service.Services.ReservationService;
using HotelReservationSystem.Service.Services.ReservationService.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReservationController : BaseController
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public ReservationController(IReservationService reservationService,
            IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<ReservationToReturnDto>> MakeReservation([FromBody] ReservationDto request)
        {
            try
            {
                var reservationDto = await _reservationService.MakeReservationAsync(request);

                if (reservationDto == null)
                    return BadRequest("Reservation could not be made.");

                return _mapper.Map<ReservationToReturnDto>(reservationDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, new { error = "An unexpected error occurred." });
            }
        }
    }
    
}
