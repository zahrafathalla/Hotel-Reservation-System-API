using HotelReservationSystem.Data.Entities;

namespace HotelReservationSystem.Repository.Interface
{
    public interface IunitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : BaseEntity;
        Task<int> CompleteAsync();

    }
}
