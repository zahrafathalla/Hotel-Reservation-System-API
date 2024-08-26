using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Service.Services.FacilityService.Dtos;
using HotelReservationSystem.Service.Services.PaymentService.Dtos;
using HotelReservationSystem.Service.Services.ReservationService.Dtos;
using HotelReservationSystem.Service.Services.RoomService.Dtos;

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

        }
    }
}
