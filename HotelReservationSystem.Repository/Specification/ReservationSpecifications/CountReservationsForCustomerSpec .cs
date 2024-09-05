using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Specification.Specifications;

namespace HotelReservationSystem.Repository.Specification.ReservationSpecifications
{
    public class CountReservationsForCustomerSpec : BaseSpecifications<Reservation>
    {
        public CountReservationsForCustomerSpec(SpecParams spec, int customerID, DateTime firstDate, DateTime secondDate)
             : base(res =>
           (res.CheckInDate >= firstDate &&
            res.CheckInDate <= secondDate &&
             res.CustomerId == customerID))
        {
            
        }
    }
}
