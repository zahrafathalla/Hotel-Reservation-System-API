using HotelReservationSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
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
            Includes.Add(r => r.Include(r => r.RoomFacilities)
                           .ThenInclude(rf => rf.Facility)); 

            AddOrderByDesc(R => R.Price);
        }

    }
}
