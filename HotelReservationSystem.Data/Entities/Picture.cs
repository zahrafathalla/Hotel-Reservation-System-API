using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Data.Entities
{
    public class Picture:BaseEntity
    {
        public string Url { get; set; } 
        public int RoomId { get; set; } 
        public Room Room { get; set; }
    }
}
