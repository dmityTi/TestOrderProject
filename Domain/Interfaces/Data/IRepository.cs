using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        Task<T> GetByIdAsync(long id);
        IEnumerable<T> Get(Func<T, bool> predicate);
        Task<IEnumerable<T>> ListAllAsync(bool noTracking = false);
    }
}