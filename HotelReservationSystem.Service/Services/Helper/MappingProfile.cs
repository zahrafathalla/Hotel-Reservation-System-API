using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Service.Services.FacilityService.Dtos;
using HotelReservationSystem.Service.Services.RoomService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Service.Services.Helper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Facility, FacilityDto>();

            CreateMap<Room, RoomToReturnDto>()
                .ForMember(d=>d.Facility , opt=>opt.MapFrom(s=>s.RoomFacilities.Select(rf=>rf.Facility)))
                .ForMember(d=>d.PictureUrl, opt=>opt.MapFrom<PictureResolver>());
        }
    }
}
