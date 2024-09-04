using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Data.Entities
{
    public class BookingReport
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string ReservationStatus { get; set; }
        public string RoomType { get; set; }
        public string Username { get; set; }
        //public string Email { get; set; }
        //public string PhoneNumber { get; set; }
    }
}
