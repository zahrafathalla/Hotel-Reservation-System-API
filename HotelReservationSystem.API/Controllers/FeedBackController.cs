using HotelReservationSystem.API.Errors;
using HotelReservationSystem.Repository.Specification.Specifications;
using HotelReservationSystem.Service.Services.FeedBackService;
using HotelReservationSystem.Service.Services.FeedBackService.Dtos;
using HotelReservationSystem.Service.Services.Helper.ResulteViewModel;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Customer")]

        [HttpPost]
        public async Task <ActionResult<FeedBackToReturnDto>>SubmitFeedback(FeedBackDto feedBackDto)
        {
            var result = await _feedBackService.SubmitFeedbackAsync(feedBackDto);
            if (!result.IsSuccessful)
                return BadRequest(new ApiResponse(400));
            return Ok(result);
        }

        [Authorize(Policy = "AdminOrStaffPolicy")]

        [HttpPost("reply")]
        public async Task<ActionResult<FeedbackReplayToReturnDto>> ResponseToFeedback([FromBody] FeedbackReplyDto feedbackReplyDto)
        {
            var reply = await _feedBackService.ResponseToFeedbackAsync(feedbackReplyDto);

            if (!reply.IsSuccessful)
                return BadRequest(new ApiResponse(400));

            return Ok(reply);
        }

        [Authorize(Policy = "AdminOrStaffPolicy")]
        [HttpGet]
        public async Task<ActionResult<Pagination<FeedBackToReturnDto>>> GetAllAsync([FromQuery] SpecParams Params)
        {
            var feedbacks = await _feedBackService.GetAllAsync(Params);
            if (feedbacks == null) 
                return BadRequest(new ApiResponse(400));
            var count = await _feedBackService.GetCount(Params);
            return Ok(new Pagination<FeedBackToReturnDto>(Params.PageSize, Params.PageIndex, count, feedbacks));
        }

        [Authorize(Policy = "AdminOrStaffPolicy")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedbackById(int id)
        {
            var feedback = await _feedBackService.GetFeedBackByIdAsync(id);

            if (!feedback.IsSuccessful)
                return NotFound(new ApiResponse(404));

            return Ok(feedback);
        }

    }
}
