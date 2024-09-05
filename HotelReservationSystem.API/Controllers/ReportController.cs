using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Service.Services.ReservationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.API.Controllers
{
    [Authorize(Policy = "AdminOrStaffPolicy")]

    public class ReportController : BaseController
    {
        private readonly IReservationService _reservationService;
        public ReportController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }


        [HttpGet("Booking")]
        public async Task<ActionResult<IEnumerable<BookingReport>>> GenerateBookingReport(DateTime startDate, DateTime endDate)
        {
            var bookingReport = await _reservationService.GetAllReservationForBookingReport(startDate, endDate);
            return Ok(bookingReport);
        }
        [HttpGet("Renevue")]
        public async Task<ActionResult<IEnumerable<RevenueReport>>> GenerateRenevueReport(DateTime startDate, DateTime endDate)
        {
            var bookingReport = await _reservationService.GetAllReservationForRevenueReport(startDate, endDate);
            return Ok(bookingReport);
        }
        [HttpGet("Customer")]
        public async Task<ActionResult<IEnumerable<CustomerReport>>> GenerateCustomerReport(int customerID, DateTime startDate, DateTime endDate)
        {
            var bookingReport = await _reservationService.
                GetAllReservationForCustomerReport(customerID, startDate, endDate);
            return Ok(bookingReport);
        }
    }
}
