

namespace HotelReservationSystem.Service.Services.OfferServices.Dtos
{
    public class OfferToReturnDto
    {
        public int Id { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsSuccess { get; set; } = true;
    }
}
