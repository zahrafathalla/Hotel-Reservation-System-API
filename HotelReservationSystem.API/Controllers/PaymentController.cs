using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Service.Services.PaymentService;
using HotelReservationSystem.Service.Services.PaymentService.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Climate;
using Stripe;

namespace HotelReservationSystem.API.Controllers
{

    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        private const string endpointSecret = "whsec_f757a9f10b80d4d0d50a426962cd866ef4be5f417beef34bfd7e5da9e2ea06ba";

        public PaymentController(
            IPaymentService paymentService,
            ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpPost("{reservationId}")]
        public async Task<ActionResult<ReservationToReturnDto>> CreatePaymentAsync(int reservationId)
        {
            var reservation = await _paymentService.CreatePaymentIntentAsync(reservationId);
            return Ok(reservation);
        }


        [HttpPost("webhook")]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json,
                Request.Headers["Stripe-Signature"], endpointSecret);

            var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
            Reservation reservation;

            if (stripeEvent.Type == Events.PaymentIntentSucceeded)
            {

                reservation = await _paymentService.UpdateReservationStatusAsync(paymentIntent.Id, true);

                _logger.LogInformation("Reservation is succeeded {0}", reservation?.PaymentIntentId);
                _logger.LogInformation("unhandled event type {0}", stripeEvent.Type);
            }
            else if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
            {
                reservation = await _paymentService.UpdateReservationStatusAsync(paymentIntent.Id, false);

                _logger.LogInformation("Reservation is succeeded {0}", reservation?.PaymentIntentId);
                _logger.LogInformation("unhandled event type {0}", stripeEvent.Type);

            }
            return Ok();
        }
    }
}
