using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Service.Services.PaymentService.Dtos;

namespace HotelReservationSystem.Service.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<ReservationForPaymentToReturnDto> CreatePaymentIntentAsync(int reservationId);
        Task<bool> ConfirmPaymentAsync(string paymentIntentId, string paymentMethodId);
        Task<Reservation> UpdateReservationStatusAsync(string paymnetIntentId, bool isPaid);
    }
}
