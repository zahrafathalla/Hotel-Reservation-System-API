using HotelReservationSystem.Data.Entities;

namespace HotelReservationSystem.Repository.Specification.ReservationSpecifications
{
    public class ReservationSpecificationWithPaymentIntent :BaseSpecifications<Reservation>
    {
        public ReservationSpecificationWithPaymentIntent(string paymentIntentId)
            :base(r=> r.PaymentIntentId == paymentIntentId)
        {
            
        }
    }
}
