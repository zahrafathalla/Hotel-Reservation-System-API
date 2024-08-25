using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Service.Services.ReservationService.Dtos;

namespace HotelReservationSystem.Service.Services.ReservationService
{
    public interface IReservationService
    {
        Task<ReservationCreatedToReturnDto> MakeReservationAsync(ReservationDto request);
        Task<bool> CancelReservationAsync(int reservationId);
        Task UpdateReservationStatusAsync();
        Task<Reservation> UpdateReservationAsync(int id, ReservationDto reservation,decimal roomPrice);
    }
}
