using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Data.Entities
{
    public class OfferRoom : BaseEntity
    {
        public int OfferId { get; set; }
        public Offer offer { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; } 
  
    }
}
