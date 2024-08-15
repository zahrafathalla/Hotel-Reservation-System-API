﻿using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Specification;

namespace HotelReservationSystem.Repository.Interface
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> Spec);
        Task<T?> GetByIdWithSpecAsync(ISpecification<T> Spec);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
