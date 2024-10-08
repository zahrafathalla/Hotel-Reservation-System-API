﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Data.Entities
{
    public class RevenueReport
    {
        public decimal TotalAmount { get; set; }
        public int ReservationId { get; set; }
        public string ReservationStatus { get; set; }
        public string RoomId { get; set; }
        public string RoomType { get; set; }
        public string CustomerName { get; set; }
    }
}
