using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Specification.Specifications;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Repository.Specification.RoomSpecifications
{
    public class RoomAvailabilitySpecification : BaseSpecifications<Room>
    {
        public RoomAvailabilitySpecification(SpecParams spec, DateTime checkInDate, DateTime checkOutDate)
            : base(r => r.Reservations.Any
            (
                reservation => reservation.CheckInDate  < checkOutDate  &&
                               reservation.CheckOutDate  > checkInDate  
                    
            ))
        {
            Includes.Add(r => r.Include(r => r.RoomFacilities)
               .ThenInclude(rf => rf.Facility));
            Includes.Add(r => r.Include(r => r.PictureUrls));


            if (!string.IsNullOrEmpty(spec.Sort))
            {

                switch (spec.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(R => R.Price);
                        break;

                    case "priceDesc":
                        AddOrderByDesc(R => R.Price);
                        break;
                }

            }

            ApplyPagination(spec.PageSize * (spec.PageIndex - 1), spec.PageSize);
        }
    }
}
