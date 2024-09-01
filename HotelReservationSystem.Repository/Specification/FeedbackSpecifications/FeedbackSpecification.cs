using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Specification.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Repository.Specification.FeedbackSpecifications
{
    public class FeedbackSpecification:BaseSpecifications<FeedBack>
    {
        public FeedbackSpecification(SpecParams spec) : base(f => !f.IsDeleted)
        {
             
            Includes.Add(f => f.Include(f => f.Customer));
            Includes.Add(f => f.Include(f => f.FeedBackReplys));
            Includes.Add(f => f.Include(f => f.Reservation));

            ApplyPagination(spec.PageSize * (spec.PageIndex - 1), spec.PageSize);
        }
        public FeedbackSpecification(int id) : base(f => f.Id == id)
        {
            Includes.Add(f => f.Include(f => f.Customer));
            Includes.Add(f => f.Include(f => f.FeedBackReplys));
            Includes.Add(f => f.Include(f => f.Reservation));
        }
    }
}
