using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Service.Services.RoomService.Dtos
{
    public class PictureDto
    {
       public int RoomId { get; set; } 
       public List<IFormFile> pictureUrls { get; set; }
    }
}
