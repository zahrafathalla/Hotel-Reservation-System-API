using HotelReservationSystem.Service.Services.ReservationService.Dtos;
using HotelReservationSystem.Service.Services.RoomService.Dtos;

namespace HotelReservationSystem.Service.Services.ReservationService
{
    public interface IReservationService
    { 
        Task<ReservationToReturnDto> MakeReservationAsync(ReservationDto reservationDto, decimal totalAmount);
        Task<bool> IsReservationConflictAsync(ReservationDto reservationDto);
        Task<bool> IsReservationConflictOnUpdateAsync(int reservationId, ReservationUpdatedDto reservationDto);
        Task<ReservationToReturnDto> ViewReservationDetailsAsync(int reservationId);
        Task<bool> CancelReservationAsync(int reservationId);
        Task<ReservationToReturnDto> UpdateReservationAsync(int id, ReservationUpdatedDto reservationDto,decimal totalAmount);
        Task UpdateCheckInStatusesAsync();
        Task UpdateCheckOutStatusesAsync();

    }
}
