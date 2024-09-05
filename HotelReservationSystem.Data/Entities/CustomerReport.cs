using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Data.Entities
{
    public class CustomerReport
    {
        public int ReservationId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string ReservationStatus { get; set; }
        public int RoomId { get; set; }
        public string RoomType { get; set; }
        public string CustomerName { get; set; }
    }
}
