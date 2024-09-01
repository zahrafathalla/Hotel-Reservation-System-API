using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Repository.Specification.FeedbackSpecifications;
using HotelReservationSystem.Repository.Specification.Specifications;
using HotelReservationSystem.Service.Services.FeedBackService.Dtos;

namespace HotelReservationSystem.Service.Services.FeedBackService
{
    public class FeedBackService : IFeedBackService
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FeedBackService(IunitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<FeedBackToReturnDto> SubmitFeedbackAsync(FeedBackDto feedBackDto)
        {
            var reservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(feedBackDto.ReservationId);

            var result = new FeedBackToReturnDto
            {
                IsSuccessful = false,
            };

            if (reservation == null)
                return result;

            var feedback = _mapper.Map<FeedBack>(feedBackDto);

            await _unitOfWork.Repository<FeedBack>().AddAsync(feedback);
            await _unitOfWork.SaveChangesAsync();

            var returnedFeedBack = _mapper.Map<FeedBackToReturnDto>(feedback);
            return returnedFeedBack;
        }


        public async Task<FeedBackToReturnDto> GetFeedBackByIdAsync(int id)
        {
            var feedback = await _unitOfWork.Repository<FeedBack>().GetByIdAsync(id);
            var mappedFeedback = _mapper.Map<FeedBackToReturnDto>(feedback);
            return mappedFeedback;
        }

        public async Task<FeedbackReplayToReturnDto> ResponseToFeedbackAsync(FeedbackReplyDto feedbackReplyDto)
        {
            var feedback = await _unitOfWork.Repository<FeedBack>().GetByIdAsync(feedbackReplyDto.FeedbackId);

            var result = new FeedbackReplayToReturnDto
            {
                IsSuccessful = false,
            };

            if (feedback == null)
                return result;

            var feedbackReply = _mapper.Map<FeedbackReply>(feedbackReplyDto);

            await _unitOfWork.Repository<FeedbackReply>().AddAsync(feedbackReply);
            await _unitOfWork.SaveChangesAsync();

            var returnedFeedbackReply = _mapper.Map<FeedbackReplayToReturnDto>(feedbackReply);

            return returnedFeedbackReply;
        }

        public async Task<IEnumerable<FeedBackToReturnDto>> GetAllAsync(SpecParams Params)
        {
            var spec = new FeedbackSpecification(Params);
            var feedbacks = await _unitOfWork.Repository<FeedBack>().GetAllWithSpecAsync(spec);

            var feedbackDtos = _mapper.Map<IEnumerable<FeedBackToReturnDto>>(feedbacks);
            return feedbackDtos;
        }

        public async Task<int> GetCount(SpecParams spec)
        {
            var CountFeedback = new CountFeedbackWithSpec(spec);
            var Count = await _unitOfWork.Repository<FeedBack>().GetCountWithSpecAsync(CountFeedback);
            return Count;
        }
        public async Task<bool> DeleteFeedbackAsync(int feedbackId)
        {
            var feedback = await _unitOfWork.Repository<FeedBack>().GetByIdAsync(feedbackId);

            if (feedback == null)
                return false;

            _unitOfWork.Repository<FeedBack>().Delete(feedback);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteFeedbackReplyAsync(int replyId)
        {
            var reply = await _unitOfWork.Repository<FeedbackReply>().GetByIdAsync(replyId);

            if (reply == null)
                return false;

            _unitOfWork.Repository<FeedbackReply>().Delete(reply);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
