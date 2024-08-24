using HotelReservationSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Repository.Specification.RoomSpecifications
{
    public class CountRoomWithSpec: BaseSpecifications<Room>
    {
        public CountRoomWithSpec(RoomSpecParams roomSpec) : base(R=>!R.IsDeleted) 
        {
            
        }
    }
}
