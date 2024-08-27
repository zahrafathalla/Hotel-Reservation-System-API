
using HotelReservationSystem.Service.Services.InvoiceService.Dtos;


namespace HotelReservationSystem.Service.Services.InvoiceService
{
    public interface IInvoiceService
    {
        Task<InvoiceToReturnDto> GenerateInvoiceAsync(int reservationId);
        Task<IEnumerable<InvoiceToReturnDto>> GetAllAsync();
    }
}
