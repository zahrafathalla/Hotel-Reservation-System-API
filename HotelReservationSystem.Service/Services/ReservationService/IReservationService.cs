using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Service.Services.ReservationService.Dtos;

namespace HotelReservationSystem.Service.Services.ReservationService
{
    public  interface IReservationService
    {
        Task<Reservation> UpdateReservationAsync(int id, ReservationDto reservation,decimal roomPrice);
    }
}
