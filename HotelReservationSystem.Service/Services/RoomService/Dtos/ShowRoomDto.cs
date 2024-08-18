using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Service.Services.RoomService.Dtos
{
    public class ShowRoomDto
    {
        public string Type { get; set; }
        public string Status { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
    }
}
