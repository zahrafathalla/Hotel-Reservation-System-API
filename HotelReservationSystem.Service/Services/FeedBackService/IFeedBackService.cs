using HotelReservationSystem.Repository.Specification.Specifications;
using HotelReservationSystem.Service.Services.FeedBackService.Dtos;
namespace HotelReservationSystem.Service.Services.FeedBackService
{
    public interface IFeedBackService
    {
        Task<FeedBackToReturnDto> SubmitFeedbackAsync(FeedBackDto feedBackDto);
        Task<FeedbackReplayToReturnDto> ResponseToFeedbackAsync(FeedbackReplyDto feedbackReplyDto);
        Task<IEnumerable<FeedBackToReturnDto>> GetAllAsync(SpecParams Params);
        Task<FeedBackToReturnDto> GetFeedBackByIdAsync(int id);
        Task<int> GetCount(SpecParams spec);
    }
}
