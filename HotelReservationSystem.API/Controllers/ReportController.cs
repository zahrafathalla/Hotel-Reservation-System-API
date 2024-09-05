using HotelReservationSystem.API.Errors;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Specification.Specifications;
using HotelReservationSystem.Service.Services.FeedBackService.Dtos;
using HotelReservationSystem.Service.Services.Helper.ResulteViewModel;
using HotelReservationSystem.Service.Services.ReservationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

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
        public async Task<ActionResult<Pagination<BookingReport>>> GenerateBookingReport([FromQuery] SpecParams Params, DateTime startDate, DateTime endDate)
        {
            var bookingReport = await _reservationService.GetAllReservationForBookingReport(Params, startDate, endDate);

            var count = await _reservationService
                .GetCountForBookingReport(Params, startDate, endDate);

            return Ok(new Pagination<BookingReport>(Params.PageSize, Params.PageIndex, count, bookingReport));
        }
        [HttpGet("Renevue")]
        public async Task<ActionResult<Pagination<RevenueReport>>> GenerateRenevueReport([FromQuery] SpecParams Params,DateTime startDate, DateTime endDate)
        {
            var bookingReport = await _reservationService.GetAllReservationForRevenueReport(Params, startDate, endDate);

            var count = await _reservationService
                .GetCountForRevenueReport(Params, startDate, endDate);

            return Ok(new Pagination<RevenueReport>(Params.PageSize, Params.PageIndex, count, bookingReport));
        }
        [HttpGet("Customer")]
        public async Task<ActionResult<Pagination<CustomerReport>>> GenerateCustomerReport([FromQuery] SpecParams Params,int customerID, DateTime startDate, DateTime endDate)
        {
            var bookingReport = await _reservationService
                .GetAllReservationForCustomerReport(Params, customerID, startDate, endDate);

            var count = await _reservationService.GetCountForCustomerReport(Params, customerID, startDate, endDate);

            return Ok(new Pagination<CustomerReport>(Params.PageSize, Params.PageIndex, count, bookingReport));
        }
    }
}
