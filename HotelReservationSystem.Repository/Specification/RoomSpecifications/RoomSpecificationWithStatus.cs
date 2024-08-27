using HotelReservationSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Repository.Specification.RoomSpecifications
{
    public class RoomSpecificationWithStatus :BaseSpecifications<Room>
    {
        public RoomSpecificationWithStatus()
            :base(r => r.Status == RoomStatus.Available)
        {
            Includes.Add(r => r.Include(r => r.RoomFacilities)
                         .ThenInclude(rf => rf.Facility));

            Includes.Add(r => r.Include(r => r.PictureUrls));
        }
    }
}
