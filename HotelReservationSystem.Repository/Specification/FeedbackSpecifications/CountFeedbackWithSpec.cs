using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Specification.Specifications;


namespace HotelReservationSystem.Repository.Specification.FeedbackSpecifications
{
    public class CountFeedbackWithSpec:BaseSpecifications<FeedBack>
    {
        public CountFeedbackWithSpec(SpecParams feedbackSpec) : base(R => !R.IsDeleted)
        {

        }
    }
}
