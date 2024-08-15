using HotelReservationSystem.Data.Context;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using System.Collections;

namespace HotelReservationSystem.Repository.Repository
{
    public class UnitOfWork : IunitOfWork
    {
        private Hashtable _repository;
        private readonly ApplicationDBContext _dbContext;

        public UnitOfWork(ApplicationDBContext dbContext)
        {
            _repository = new Hashtable();
            _dbContext = dbContext;
        }
        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            var key = typeof(T);

            if (!_repository.ContainsKey(key))
            {
                var value = new GenericRepository<T>(_dbContext);

                _repository.Add(key, value);
            }

            return _repository[key] as IGenericRepository<T>;
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
