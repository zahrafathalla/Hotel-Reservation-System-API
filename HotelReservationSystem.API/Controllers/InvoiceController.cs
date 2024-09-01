using HotelReservationSystem.API.Errors;
using HotelReservationSystem.Repository.Specification.Specifications;
using HotelReservationSystem.Service.Services.Helper.ResulteViewModel;
using HotelReservationSystem.Service.Services.InvoiceService;
using HotelReservationSystem.Service.Services.InvoiceService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.API.Controllers
{
    public class InvoiceController : BaseController
    {
        IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [ProducesResponseType(typeof(InvoiceToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("Get All Invoic")]
        public async Task<ActionResult<Pagination<InvoiceToReturnDto>>> GetAll([FromQuery] SpecParams Params)
        {
            var Invoice = await _invoiceService.GetAllAsync(Params);
            if (Invoice == null) return BadRequest(new ApiResponse(400));
            var count = await _invoiceService.GetCount(Params);
            return Ok(new Pagination<InvoiceToReturnDto>(Params.PageSize, Params.PageIndex, count, Invoice));
        }

        [ProducesResponseType(typeof(InvoiceToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpPost("Generate Invoice{reservationId}")]
        public async Task<ActionResult<InvoiceToReturnDto>> GenerateInvoice(int reservationId)
        {
            var invoice = await _invoiceService.GenerateInvoiceAsync(reservationId);
            if (!invoice.IsSuccessful)
                return BadRequest(new ApiResponse(400));

            return Ok(invoice);
        }


        [ProducesResponseType(typeof(InvoiceToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("Get Invoic By {id}")]
        public async Task<ActionResult<InvoiceToReturnDto>> GetInvoicById(int id)
        {
            var facility = await _invoiceService.GetInvoicByIdAsync(id);
            if (facility == null) return BadRequest(new ApiResponse(400));
            return Ok(facility);
        }


        [HttpDelete("Delete Invoic{id}")]
        public async Task<ActionResult<bool>> DeleteInvoic(int id)
        {
            return Ok(await _invoiceService.DeleteInvoicAsync(id));
        }
    }
}
