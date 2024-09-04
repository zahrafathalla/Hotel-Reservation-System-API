using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Specification.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Repository.Specification.RoomSpecifications
{
    public class CountRoomWithSpec: BaseSpecifications<Room>
    {
        public CountRoomWithSpec(DateTime checkInDate, DateTime checkOutDate)
            : base(r => r.Reservations.Any
            (
                reservation => reservation.CheckInDate < checkOutDate &&
                               reservation.CheckOutDate > checkInDate

            ))
        {
            
        }
    }
}
