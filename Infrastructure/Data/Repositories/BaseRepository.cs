using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Data;
using Infrastructure.Data.Context;
using Infrastructure.Extentions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly OrderContext DbContext;

        public BaseRepository(OrderContext dbContext)
        {
            DbContext = dbContext;
        }
        
        public void Add(T entity)
        {
            DbContext.Set<T>().Add(entity);
        }
        
        public void AddRange(IEnumerable<T> entities)
        {
            DbContext.Set<T>().AddRange(entities);
        }

        public void Update(T entity)
        {
            DbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            DbContext.Set<T>().Update(entity);
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await DbContext.Set<T>().FindAsync(id);
        }

        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return DbContext.Set<T>().Where(predicate).ToList() as IEnumerable<T>;
        }

        public async Task<IEnumerable<T>> ListAllAsync(bool noTracking = false)
        {
            return await DbContext.Set<T>().NoTracking(noTracking).ToListAsync();
        }
    }
}