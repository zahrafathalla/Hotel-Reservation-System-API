namespace HotelReservationSystem.Data.Entities
{
    public class ReservationFacility :BaseEntity
    {
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public int FacilityId { get; set; }
        public Facility Facility { get; set; }
    }
}