

namespace HotelReservationSystem.Service.Services.InvoiceService.Dtos
{
    public class InvoiceToReturnDto
    {
        public DateTime InvoiceDate { get; set; }
        public decimal Amount { get; set; }
        public int ReservationId { get; set; }
    }
}
