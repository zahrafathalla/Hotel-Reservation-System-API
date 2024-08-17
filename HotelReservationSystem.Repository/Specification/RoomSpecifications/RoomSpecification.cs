using HotelReservationSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Repository.Specification.RoomSpecifications
{
    public class RoomSpecification : BaseSpecifications<Room>
    {
        public RoomSpecification() : base()
        {
            AddOrderByDesc(R => R.Price);
        }

        public RoomSpecification(int id) :base(R=>R.Id == id) 
        {
            AddOrderByDesc(R => R.Price);
        }
    }
}
