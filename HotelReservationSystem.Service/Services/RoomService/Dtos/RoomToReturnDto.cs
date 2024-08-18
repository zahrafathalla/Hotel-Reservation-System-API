using HotelReservationSystem.Service.Services.FacilityService.Dtos;

namespace HotelReservationSystem.Service.Services.RoomService.Dtos
{
    public class RoomToReturnDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string PictureUrl { get; set; }
        public IEnumerable<FacilityDto> Facility { get; set; }
        public decimal Price { get; set; }

    }
}



