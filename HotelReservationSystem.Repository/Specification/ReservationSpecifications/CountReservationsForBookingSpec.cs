using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Specification.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Repository.Specification.ReservationSpecifications
{
    public class CountReservationsForBookingSpec : BaseSpecifications<Reservation>
    {
        public CountReservationsForBookingSpec(SpecParams spec, DateTime firstDate, DateTime secondDate)
             : base(res =>
           (res.CheckInDate >= firstDate &&
            res.CheckInDate <= secondDate))
             
        {

        }
    }

}
