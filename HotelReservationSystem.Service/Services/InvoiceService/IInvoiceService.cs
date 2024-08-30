
using HotelReservationSystem.Repository.Specification.RoomSpecifications;
using HotelReservationSystem.Repository.Specification.Specifications;
using HotelReservationSystem.Service.Services.FacilityService.Dtos;
using HotelReservationSystem.Service.Services.InvoiceService.Dtos;


namespace HotelReservationSystem.Service.Services.InvoiceService
{
    public interface IInvoiceService
    {
        Task<InvoiceToReturnDto> GenerateInvoiceAsync(int reservationId);
        Task<IEnumerable<InvoiceToReturnDto>> GetAllAsync(SpecParams specParams);
        Task<InvoiceToReturnDto> GetInvoicByIdAsync(int id);
        Task<bool> DeleteInvoicAsync(int id);
        Task<int> GetCount(SpecParams spec);
    }
}
