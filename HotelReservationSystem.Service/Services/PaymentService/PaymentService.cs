using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace HotelReservationSystem.Service.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IunitOfWork _unitOfWork;

        public PaymentService(
            IConfiguration configuration,
            IunitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }
        public async Task<Reservation> CreatePaymentIntentAsync(int reservationId)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            var reservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(reservationId);
            if (reservation == null)
                return null;

            var room = await _unitOfWork.Repository<Room>().GetByIdAsync(reservation.RoomId);
            if (room == null)
                return null;

            var totalAmount = reservation.TotalAmount;

            PaymentIntent paymentIntent;
            var paymentIntentService = new PaymentIntentService();

            if (string.IsNullOrEmpty(reservation.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(totalAmount * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                paymentIntent = await paymentIntentService.CreateAsync(options);

                reservation.PaymentIntentId = paymentIntent.Id;
                reservation.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount =(long)(totalAmount * 100)
                };
                await paymentIntentService.UpdateAsync(reservation.PaymentIntentId, options);
            }

            _unitOfWork.Repository<Reservation>().Update(reservation);
            await _unitOfWork.CompleteAsync();
            return reservation;
        }
    }
}
