namespace HotelReservationSystem.Data.Entities
{
    public class Facility : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<RoomFacility> RoomFacilities { get; set; } = new List<RoomFacility>();

    }
}
