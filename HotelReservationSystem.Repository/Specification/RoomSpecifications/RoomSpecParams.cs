﻿namespace HotelReservationSystem.Repository.Specification.RoomSpecifications
{
    public class RoomSpecParams
    {
        public string? Sort { get; set; }

        private int pageSize=10;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > 10 ? 10 : value; }
        }
        public int PageIndex { get; set; } = 1;

    }
}