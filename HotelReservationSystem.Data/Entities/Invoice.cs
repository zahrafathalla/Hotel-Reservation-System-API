using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Data.Entities
{
    public class Invoice : BaseEntity
    {
        public DateTime InvoiceDate { get; set; }
        public decimal Amount { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

    }
}
