﻿using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Service.Services.ReservationService.Dtos;

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

    }
}
