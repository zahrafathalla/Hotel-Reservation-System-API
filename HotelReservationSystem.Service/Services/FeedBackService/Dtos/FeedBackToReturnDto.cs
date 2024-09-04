
namespace HotelReservationSystem.Service.Services.FeedBackService.Dtos
{
    public class FeedBackToReturnDto
    {
        public int FeedbackId { get; set; }
        public string Text { get; set; }
        public DateTime DateSubmitted { get; set; }
        public int Rating { get; set; }
        public bool IsSuccessful { get; set; } = true;
    }
}
