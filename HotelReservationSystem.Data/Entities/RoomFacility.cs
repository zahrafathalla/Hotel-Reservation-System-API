
namespace HotelReservationSystem.Data.Entities
{
    public class RoomFacility : BaseEntity
    {
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int FacilityId { get; set; }
        public Facility Facility { get; set; }
    }
}
