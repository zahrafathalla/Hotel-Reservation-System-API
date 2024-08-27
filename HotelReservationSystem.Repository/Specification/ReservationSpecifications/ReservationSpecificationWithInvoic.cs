using HotelReservationSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Repository.Specification.ReservationSpecifications
{
    public class ReservationSpecificationWithInvoic : BaseSpecifications<Reservation>
    {
        public ReservationSpecificationWithInvoic(int id) : base(R => R.Id == id)
        {

        }

    }
}
