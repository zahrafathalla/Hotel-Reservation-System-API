using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Specification.Specifications;
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
        public RoomSpecification(SpecParams spec) : base()
        {
            Includes.Add(r => r.Include(r => r.RoomFacilities)
                           .ThenInclude(rf => rf.Facility));
            Includes.Add(r => r.Include(r => r.PictureUrls));
      
        }

        public RoomSpecification(int id) : base(R=>R.Id==id) 
        {
            Includes.Add(r => r.Include(r => r.RoomFacilities)
                           .ThenInclude(rf => rf.Facility));
            Includes.Add(r => r.Include(r => r.PictureUrls));

        }



    }
}
