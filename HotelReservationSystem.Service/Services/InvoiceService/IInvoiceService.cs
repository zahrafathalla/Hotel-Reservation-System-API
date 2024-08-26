
using HotelReservationSystem.Service.Services.InvoiceService.Dtos;


namespace HotelReservationSystem.Service.Services.InvoiceService
{
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceToReturnDto>> GetAllAsync();
    }
}
