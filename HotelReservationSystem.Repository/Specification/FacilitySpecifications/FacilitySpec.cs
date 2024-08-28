using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Specification.RoomSpecifications;
using HotelReservationSystem.Repository.Specification.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Repository.Specification.FacilitySpecifications
{
    public class FacilitySpec :BaseSpecifications<Facility>
    {
       

        public FacilitySpec(SpecParams spec) : base(F=> !F.IsDeleted) 
        {
            ApplyPagination(spec.PageSize * (spec.PageIndex - 1), spec.PageSize);
        }
        public FacilitySpec( int id ) : base(F=>F.Id == id)
        {
            
        }

    }
}
