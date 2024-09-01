using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Data.Entities
{
    public class FeedbackReply:BaseEntity
    {
        public string StaffResponse { get; set; }
        public DateTime DateResponded { get; set; }
        public int FeedbackId { get; set; }
        public FeedBack FeedBack { get; set; }
        public int? StaffId { get; set; }
        public Staff? staff { get; set; }
    }
}
