using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Specification.RoomSpecifications;
using HotelReservationSystem.Repository.Specification.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Repository.Specification.Facilitypecifications
{
    public class CountFacilityWithSpec: BaseSpecifications<Facility>
    {
        public CountFacilityWithSpec(SpecParams Spec) : base(R=>!R.IsDeleted) 
        {
            
        }
    }
}
