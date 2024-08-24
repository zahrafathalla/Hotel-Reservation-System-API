using HotelReservationSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Repository.Specification.RoomSpecifications
{
    public class RoomSpecification : BaseSpecifications<Room>
    {
        public RoomSpecification(RoomSpecParams spec) : base()
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

                    default:
                        AddOrderBy(R => R.IsDeleted==false);
                        break;

                }

            }
            else
            {
                AddOrderBy(R => R.IsDeleted == false);
            }

            ApplyPagination(spec.PageSize*(spec.PageIndex-1),spec.PageSize);


           
        }

        public RoomSpecification(int id) : base(R=>R.Id==id) 
        {
            Includes.Add(r => r.Include(r => r.RoomFacilities)
                           .ThenInclude(rf => rf.Facility));
            Includes.Add(r => r.Include(r => r.PictureUrls));

        }



    }
}
