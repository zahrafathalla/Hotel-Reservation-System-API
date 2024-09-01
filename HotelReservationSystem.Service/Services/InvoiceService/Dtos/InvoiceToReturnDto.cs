

namespace HotelReservationSystem.Service.Services.InvoiceService.Dtos
{
    public class InvoiceToReturnDto
    {
        public int Id { get; set; }
        public DateTime InvoiceDate { get; set; } 
        public decimal Amount { get; set; }
        public int ReservationId { get; set; }
        public bool IsSuccessful { get; set; } = true;
    }
}
