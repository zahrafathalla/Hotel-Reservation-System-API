using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Service.Services.RoomService.Dtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Service.Services.Helper
{
    public class PictureResolver : IValueResolver<Room, RoomToReturnDto, IEnumerable<string>>
    {
        private readonly IConfiguration _configuration;

        public PictureResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<string> Resolve(Room source, RoomToReturnDto destination, IEnumerable<string> destMember, ResolutionContext context)
        {
            if (source.PictureUrls != null && source.PictureUrls.Any())
            {
                return source.PictureUrls.Select(p => $"{_configuration["BaseUrl"]}/Files/RoomImages/{p.Url}");
            }

            return Enumerable.Empty<string>();
        }
    }
}
