using HotelReservationSystem.Data.Entities;

namespace HotelReservationSystem.Service.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<Reservation> CreatePaymentIntentAsync(int reservationId);
    }
}
