using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Service.Services.ReservationService.Dtos
{
    public class ReservationToReturnDto
    {
        public int ReservationId { get; set; }
        public string Message { get; set; }
    }
}
