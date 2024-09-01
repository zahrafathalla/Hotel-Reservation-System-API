using Azure;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Specification.Specifications;
using HotelReservationSystem.Service.Services.FeedBackService.Dtos;
using HotelReservationSystem.Service.Services.InvoiceService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Service.Services.FeedBackService
{
    public interface IFeedBackService
    {
        Task<FeedBackToReturnDto> SubmitFeedback(FeedBackDto feedBackDto);
        Task<IEnumerable<FeedBackToReturnDto>> GetAllAsync(SpecParams Params);
        Task<FeedBackToReturnDto> GetFeedBackByIdAsync(int id);
        Task<int> GetCount(SpecParams spec);
        Task<FeedbackReply> RespondToFeedback(FeedbackReplyDto feedbackReplyDto);
    }
}
