using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Repository.Specification.FeedbackSpecifications;
using HotelReservationSystem.Repository.Specification.InvoicSpecifications;
using HotelReservationSystem.Repository.Specification.Specifications;
using HotelReservationSystem.Service.Services.FeedBackService.Dtos;
using HotelReservationSystem.Service.Services.InvoiceService.Dtos;
using HotelReservationSystem.Service.Services.ReservationService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<FeedBackToReturnDto> SubmitFeedback(FeedBackDto feedBackDto)
        {
            var reservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(feedBackDto.ReservationId);

            var result = new FeedBackToReturnDto
            {
                IsSuccessful = false,
            };

            if (reservation == null || reservation.Status == ReservationStatus.Cancelled ||reservation.CheckInDate>feedBackDto.DateSubmitted)
                return result;

            var feedback = _mapper.Map<FeedBack>(feedBackDto);
            await _unitOfWork.Repository<FeedBack>().AddAsync(feedback);
            await _unitOfWork.CompleteAsync();
            var returnedFeedBack = _mapper.Map<FeedBackToReturnDto>(feedback);
            return returnedFeedBack;
        }


        public async Task<FeedBackToReturnDto> GetFeedBackByIdAsync(int id)
        {
            var feedback = await _unitOfWork.Repository<FeedBack>().GetByIdAsync(id);
            var mappedFeedback = _mapper.Map<FeedBackToReturnDto>(feedback);
            return mappedFeedback;
        }

        public Task<FeedbackReply> RespondToFeedback(FeedbackReplyDto feedbackReplyDto)
        {
            throw new NotImplementedException();
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
    }
}
