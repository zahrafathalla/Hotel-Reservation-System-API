﻿using HotelReservationSystem.Service.Services.FacilityService.Dtos;

namespace HotelReservationSystem.Service.Services.RoomService.Dtos
{
    public class RoomToReturnDto
    {
        public string Type { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public IEnumerable<FacilityDto> Facility { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
