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
        public CountRoomWithSpec(SpecParams roomSpec) : base(R=>!R.IsDeleted) 
        {
            
        }
    }
}
