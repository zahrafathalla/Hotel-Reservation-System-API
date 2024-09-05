using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Specification.Specifications;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Repository.Specification.ReservationSpecifications
{
    public class ReservationSpecificationForCustomerReporting :BaseSpecifications<Reservation>
    {
        public ReservationSpecificationForCustomerReporting(SpecParams spec, DateTime firstDate, DateTime secondDate, int customerId )
             : base(res =>
           (res.CheckInDate >= firstDate &&
            res.CheckInDate <= secondDate && 
             res.CustomerId == customerId))
        {
            Includes.Add(r => r.Include(r => r.Room));
            Includes.Add(r => r.Include(r => r.Customer));


            if (!string.IsNullOrEmpty(spec.Sort))
            {

                switch (spec.Sort)
                {
                    case "AmountAsc":
                        AddOrderBy(R => R.TotalAmount);
                        break;

                    case "AmountDesc":
                        AddOrderByDesc(R => R.TotalAmount);
                        break;
                }

            }

            ApplyPagination(spec.PageSize * (spec.PageIndex - 1), spec.PageSize);

        }
    }
}
