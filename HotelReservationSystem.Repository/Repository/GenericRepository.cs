using HotelReservationSystem.Data.Context;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Repository.Specification;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HotelReservationSystem.Repository.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDBContext _dBContext;

        public GenericRepository(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> Spec)
        {
            return await ApplySpecification(Spec).ToListAsync();
        }
        public async Task<T?> GetByIdWithSpecAsync(ISpecification<T> Spec)
        {
            return await ApplySpecification(Spec).FirstOrDefaultAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> Spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dBContext.Set<T>(), Spec);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dBContext.Set<T>().Where(x => !x.IsDeleted).ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _dBContext.Set<T>().Where(expression).ToListAsync();
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dBContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task AddAsync(T entity)
        {
            await _dBContext.Set<T>().AddAsync(entity);
        }
        public void Update(T entity)
        {
            _dBContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }

    }
}
