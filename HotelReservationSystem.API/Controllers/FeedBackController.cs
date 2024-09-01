using HotelReservationSystem.API.Errors;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Specification.FeedbackSpecifications;
using HotelReservationSystem.Repository.Specification.InvoicSpecifications;
using HotelReservationSystem.Repository.Specification.Specifications;
using HotelReservationSystem.Service.Services.FeedBackService;
using HotelReservationSystem.Service.Services.FeedBackService.Dtos;
using HotelReservationSystem.Service.Services.Helper.ResulteViewModel;
using HotelReservationSystem.Service.Services.InvoiceService.Dtos;
using HotelReservationSystem.Service.Services.RoomService.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.API.Controllers
{
    public class FeedBackController : BaseController
    {
        private readonly IFeedBackService _feedBackService;

        public FeedBackController(IFeedBackService feedBackService)
        {
            _feedBackService = feedBackService;
        }
        [HttpPost("SubmitFeedback")]
        public async Task <ActionResult<FeedBackToReturnDto>>SubmitFeedback(FeedBackDto feedBackDto)
        {
            var result = await _feedBackService.SubmitFeedback(feedBackDto);
            if (result == null)
                return BadRequest(new ApiResponse(400));
            return Ok(result);
        }

        [HttpGet("GetAllFeedbacks")]
        public async Task<ActionResult<Pagination<FeedBackToReturnDto>>> GetAllAsync([FromQuery] SpecParams Params)
        {
            var feedbacks = await _feedBackService.GetAllAsync(Params);
            if (feedbacks == null) 
                return BadRequest(new ApiResponse(400));
            var count = await _feedBackService.GetCount(Params);
            return Ok(new Pagination<FeedBackToReturnDto>(Params.PageSize, Params.PageIndex, count, feedbacks));
        }

    }
}
