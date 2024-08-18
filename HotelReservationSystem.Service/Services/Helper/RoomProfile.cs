using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Service.Services.RoomService.Dtos;


namespace HotelReservationSystem.Service.Services.Helper
{
    public class RoomProfile:Profile
    {
        
            public RoomProfile()
            {
                CreateMap<Room, ShowRoomDto>().ForMember(dst => dst.Type,
                    opt => opt.MapFrom(src => src.Type.ToString()));
                CreateMap<ShowRoomDto, RoomViewModel>().ForMember(dst => dst.Status,
                    opt => opt.MapFrom(src => src.Status.ToString()));
            }
        
    }
}
