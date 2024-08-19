using HotelReservationSystem.Data.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace HotelReservationSystem.Repository.Specification
{
    public class BaseSpecifications<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> Includes { get; set; } = new List<Func<IQueryable<T>, IIncludableQueryable<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }

        public BaseSpecifications()
        {

        }
        public BaseSpecifications(Expression<Func<T, bool>> CriteriaExpression)
        {

            Criteria = CriteriaExpression;

        }

        public void AddOrderBy(Expression<Func<T, object>> OrderByExpression)
        {

            OrderBy = OrderByExpression;

        }

        public void AddOrderByDesc(Expression<Func<T, object>> OrderByDescExpression)
        {

            OrderByDesc = OrderByDescExpression;

        }

    }
}
