using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Service.Services.ReservationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Service.Services.ReservationService
{
    public interface IReservationService
    {
        Task<Reservation> MakeReservationAsync(ReservationDto request);
    }
}
