using HotelReservationSystem.Service.Services.ReservationService.Dtos;

namespace HotelReservationSystem.Mediator.ReservationMediator
{
    public interface IReservationMediator
    {
        Task<ReservationCreatedToReturnDto> UpdateReservationAsync(int id, ReservationDto reservationDto);
    }
}
