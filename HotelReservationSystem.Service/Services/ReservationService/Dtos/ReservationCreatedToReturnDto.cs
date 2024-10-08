﻿using HotelReservationSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Service.Services.ReservationService.Dtos
{
    public class ReservationCreatedToReturnDto
    {
        public int ReservationId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Message { get; set; }
    }
}
