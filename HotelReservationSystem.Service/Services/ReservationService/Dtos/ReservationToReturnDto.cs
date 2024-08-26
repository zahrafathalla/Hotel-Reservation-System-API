using HotelReservationSystem.Data.Entities;
using System;


namespace HotelReservationSystem.Service.Services.ReservationService.Dtos
{
    public class ReservationToReturnDto 
    {
        public int ReservationId { get; set; }
        public int RoomId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }
}
