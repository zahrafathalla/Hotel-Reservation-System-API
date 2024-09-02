using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Service.Services.FacilityService.Dtos;
using HotelReservationSystem.Service.Services.FeedBackService.Dtos;
using HotelReservationSystem.Service.Services.InvoiceService.Dtos;
using HotelReservationSystem.Service.Services.OfferRoomsServices.Dtos;
using HotelReservationSystem.Service.Services.OfferServices.Dtos;
using HotelReservationSystem.Service.Services.PaymentService.Dtos;
using HotelReservationSystem.Service.Services.ReservationService.Dtos;
using HotelReservationSystem.Service.Services.RoomService.Dtos;
using HotelReservationSystem.Service.Services.UserService.Dtos;

namespace HotelReservationSystem.Service.Services.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Facility, FacilityDto>();

            CreateMap<Room, RoomToReturnDto>()
                .ForMember(d => d.Facility, opt => opt.MapFrom(s => s.RoomFacilities.Select(rf => rf.Facility)))
                .ForMember(d => d.PictureUrls, opt => opt.MapFrom<PictureResolver>());

            CreateMap<Room, RoomcreatedToReturnDto>()
                 .ForMember(d => d.Facility, opt => opt.MapFrom(s => s.RoomFacilities.Select(rf => rf.Facility)));

            CreateMap<RoomDto, Room>();

            CreateMap<ReservationDto, Reservation>();
            CreateMap<ReservationUpdatedDto, Reservation>();


            CreateMap<Reservation, ReservationToReturnDto>()
                .ForMember(dest => dest.ReservationId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

            CreateMap<ReservationDto, Reservation>();

            CreateMap<Reservation, ReservationForPaymentToReturnDto>();

            CreateMap<Invoice, InvoiceToReturnDto>();

            CreateMap<FeedBackDto, FeedBack>()
                  .ForMember(dest => dest.DateSubmitted, opt => opt.MapFrom(src => DateTime.UtcNow))
                  .ForMember(dest => dest.FeedBackReplys, opt => opt.Ignore());

            CreateMap<FeedBack, FeedBackToReturnDto>()
                  .ForMember(dest => dest.FeedbackId, opt => opt.MapFrom(src => src.Id));


            CreateMap<FeedbackReplyDto, FeedbackReply>();
            CreateMap<FeedbackReply, FeedbackReplayToReturnDto>();

            CreateMap<User, UserToReturnDto>();

            CreateMap<Offer, OfferDto>();
            CreateMap<OfferRoom, OfferRoomsDto>();
        }
    }
}
