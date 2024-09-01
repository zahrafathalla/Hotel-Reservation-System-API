using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Service.Services.FeedBackService.Dtos
{
    public class FeedBackToReturnDto
    {
        public int FeedbackId { get; set; }
        public string Text { get; set; }
        public DateTime DateSubmitted { get; set; }
        public bool IsSuccessful { get; set; } = true;
    }
}
