using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Data.Entities
{
    public class Reservation : BaseEntity
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int? OfferId { get; set; }
        public Offer Offer { get; set; }
    }
}
