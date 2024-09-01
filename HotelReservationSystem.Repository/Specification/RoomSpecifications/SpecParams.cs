namespace HotelReservationSystem.Repository.Specification.Specifications
{
    public class SpecParams
    {
        public string? Sort { get; set; }

        private int pageSize=5;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > 5 ? 5 : value; }
        }
        public int PageIndex { get; set; } = 1;

    }
}