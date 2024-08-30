using HotelReservationSystem.Data.Entities;


namespace HotelReservationSystem.Repository.Specification.ReservationSpecifications
{
    public class ReservationSpecificationWithInvoic : BaseSpecifications<Reservation>
    {
        public ReservationSpecificationWithInvoic(int id) : base(R => R.Id == id)
        {
     
        }

    }
}
