using HotelReservationSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Repository.Specification
{
    public static class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var Query = inputQuery;

            if (spec.Criteria is not null)
            {
                Query = Query.Where(spec.Criteria);
            }

            if (spec.OrderBy is not null)
            {
                Query = Query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDesc is not null)
            {
                Query = Query.OrderByDescending(spec.OrderByDesc);
            }
            Query = spec.Includes.Aggregate(Query, (CurrentQuery, includeExprition)
                  => CurrentQuery.Include(includeExprition));
            return Query;
        }

    }

}
