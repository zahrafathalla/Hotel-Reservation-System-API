﻿
namespace HotelReservationSystem.Data.Entities
{
    public class FeedBack : BaseEntity
    {
        public string Text { get; set; }
        public int Rating { get; set; }
        public DateTime DateSubmitted { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<FeedbackReply>? FeedBackReplys { get; set; } = new List<FeedbackReply>();

    }
}
