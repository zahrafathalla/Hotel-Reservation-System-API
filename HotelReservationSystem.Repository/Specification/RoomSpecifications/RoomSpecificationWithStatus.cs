using HotelReservationSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Repository.Specification.RoomSpecifications
{
    public class RoomSpecificationWithStatus :BaseSpecifications<Room>
    {
        public RoomSpecificationWithStatus()
            :base(r => r.Status == RoomStatus.Available)
        {
            Includes.Add(r => r.Include(r => r.RoomFacilities)
                         .ThenInclude(rf => rf.Facility));

            Includes.Add(r => r.Include(r => r.PictureUrls));
        }
    }
}
