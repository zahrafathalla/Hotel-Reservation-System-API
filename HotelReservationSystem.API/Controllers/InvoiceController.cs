using HotelReservationSystem.API.Errors;
using HotelReservationSystem.Service.Services.InvoiceService;
using HotelReservationSystem.Service.Services.InvoiceService.Dtos;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace HotelReservationSystem.API.Controllers
{

    public class InvoiceController : BaseController
    {
        IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceToReturnDto>>> GetAll()
        {
            var invoices = await _invoiceService.GetAllAsync();
            if (invoices is null) return NotFound(new ApiResponse(404));
            return Ok(invoices);
        }

    }
}
