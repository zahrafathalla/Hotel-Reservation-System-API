using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Service.Services.OfferServices.Dtos
{
    public class OfferToReturnDto
    {
        public int Id { get; set; }
        public double Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<int> RoomIds { get; set; } = new List<int>();
    }
}
