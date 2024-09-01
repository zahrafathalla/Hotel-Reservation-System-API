using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Service.Services.FeedBackService.Dtos
{
    public class FeedbackReplyDto
    {
        public string StaffResponse { get; set; }
        public DateTime DateResponded { get; set; } = DateTime.Now;
        public int FeedbackId { get; set; }
        public int StaffId { get; set; }
    }
}
