using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Service.Services.ReservationService.Dtos;
using HotelReservationSystem.Service.Services.RoomService.Dtos;

namespace HotelReservationSystem.Service.Services.ReservationService
{
    public interface IReservationService
    { 
        Task<ReservationToReturnDto> MakeReservationAsync(ReservationDto reservationDto, decimal totalAmount);
        Task<bool> IsReservationConflictAsync(ReservationDto reservationDto);
        Task<bool> IsReservationConflictOnUpdateAsync(int reservationId, ReservationDto reservationDto);
        Task<ReservationToReturnDto> ViewReservationDetailsAsync(int reservationId);
        Task<bool> CancelReservationAsync(int reservationId);
        Task<ReservationToReturnDto> UpdateReservationAsync(int id, ReservationDto reservation,decimal totalAmount);
        Task UpdateCheckInStatusesAsync();
        Task UpdateCheckOutStatusesAsync();
        Task<IEnumerable<BookingReport>> GetAllReservationForBookingReport(DateTime firstDate, DateTime secondDate);
        Task<IEnumerable<RevenueReport>> GetAllReservationForRevenueReport(DateTime firstDate, DateTime secondDate);
        Task<IEnumerable<CustomerReport>> GetAllReservationForCustomerReport(int customerID, DateTime firstDate, DateTime secondDate);
    

    }
}
