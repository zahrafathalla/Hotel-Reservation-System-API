using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Specification.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Repository.Specification.FeedbackSpecifications
{
    public class CountFeedbackWithSpec:BaseSpecifications<FeedBack>
    {
        public CountFeedbackWithSpec(SpecParams feedbackSpec) : base(R => !R.IsDeleted)
        {

        }
    }
}
