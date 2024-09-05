namespace HotelReservationSystem.Data.Entities
{
    public class BookingReport
    {
        public int ReservationId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string ReservationStatus { get; set; }
        public int RoomId { get; set; }
        public string RoomType { get; set; }
        public string CustomerName { get; set; }

        //public string Email { get; set; }
        //public string PhoneNumber { get; set; }
    }
}
