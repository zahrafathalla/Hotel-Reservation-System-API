using HotelReservationSystem.Service.Services.ReservationService.Dtos;

namespace HotelReservationSystem.Mediator.ReservationMediator
{
    public interface IReservationMediator
    {
        Task<ReservationToReturnDto> CreateReservationAsync(ReservationDto reservationDto);
        Task<ReservationToReturnDto> UpdateReservationAsync(int id, ReservationDto reservationDto);
    }
}
