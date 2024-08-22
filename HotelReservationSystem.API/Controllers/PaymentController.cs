using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Service.Services.PaymentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.API.Controllers
{

    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("{reservationId}")]
        public async Task<ActionResult<PaymentService>> CreatePaymentAsync(int reservationId)
        {
            var reservation = await _paymentService.CreatePaymentIntentAsync(reservationId);
            return Ok(reservation);
        }
    }
}
