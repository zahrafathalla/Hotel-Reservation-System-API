using HotelReservationSystem.Service.Services.ReservationService.Dtos;

namespace HotelReservationSystem.Service.Services.ReservationService
{
    public interface IReservationService
    {
        Task<ReservationToReturnDto> MakeReservationAsync(ReservationDto request);

    }
}
