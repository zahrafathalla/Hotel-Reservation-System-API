

namespace HotelReservationSystem.Service.Services.FeedBackService.Dtos
{
    public class FeedbackReplayToReturnDto
    {
        public string StaffResponse { get; set; }
        public DateTime DateResponded { get; set; }
        public int FeedbackId { get; set; }
        public int StaffId { get; set; }
        public bool IsSuccessful { get; set; } = true;

    }
}
