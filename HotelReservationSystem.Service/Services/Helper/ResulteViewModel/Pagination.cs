using HotelReservationSystem.Service.Services.RoomService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Service.Services.Helper.ResulteViewModel
{
    public class Pagination<T>
    {

        public Pagination(int pageSize, int pageIndex,int count, IEnumerable<T> data)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            Data = data;
            Count = count;

        }

        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
