using HotelReservationSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Service.Services.PaymentService.Dtos
{
    public class ReservationForPaymentToReturnDto
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } 
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int RoomId { get; set; }
        public int CustomerId { get; set; }

    }
}
