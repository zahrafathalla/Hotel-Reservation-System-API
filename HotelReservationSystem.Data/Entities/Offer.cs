using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Data.Entities
{
    public class Offer : BaseEntity
    {
        public double Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<OfferRoom> OfferRoms { get; set; } = new HashSet<OfferRoom>();
    }
}
