using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Data.Entities
{
    public class Room : BaseEntity
    {
        public RoomType Type { get; set; }
        public RoomStatus Status { get; set; }
        public decimal Price { get; set; }
        public ICollection<Picture> PictureUrls { get; set; } = new List<Picture>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public ICollection<RoomFacility> RoomFacilities { get; set; } = new List<RoomFacility>();

    }
}
