using HotelReservationSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Repository.Specification.RoomFacilitySpecification
{
    public class RoomFacilitySpecification : BaseSpecifications<RoomFacility>
    {
        public RoomFacilitySpecification(int roomId)
            :base(rf => rf.RoomId == roomId)
        {
            Includes.Add(rf => rf.Include(rf => rf.Facility));
        }
    }
}
