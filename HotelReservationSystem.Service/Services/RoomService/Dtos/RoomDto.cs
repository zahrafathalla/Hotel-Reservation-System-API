using HotelReservationSystem.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Service.Services.RoomService.Dtos
{
    public class RoomDto
    {
        [Required]
        public RoomType Type { get; set; }
        [Required]

        public RoomStatus Status { get; set; }
        [Required]

        public decimal Price { get; set; }
        [Required]

        public IFormFile PictureFile { get; set; }
        [Required]

        public List<int> FacilityIds { get; set; }
    }
}
