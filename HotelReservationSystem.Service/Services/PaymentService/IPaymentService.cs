using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Service.Services.PaymentService.Dtos;

namespace HotelReservationSystem.Service.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<ReservationToReturnDto> CreatePaymentIntentAsync(int reservationId);
        Task ConfirmPaymentAsync(string paymentIntentId, string paymentMethodId);
        Task<Reservation> UpdateReservationStatusAsync(string paymnetIntentId, bool isPaid);
    }
}
