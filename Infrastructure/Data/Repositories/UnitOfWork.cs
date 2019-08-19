using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Data;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderContext _dbContext;
        private bool _disposed;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        private IOrderRepository _orderRepo;

        public IOrderRepository OrderRepo => _orderRepo ?? (_orderRepo = new OrderRepository(_dbContext));
        
        public IRepository<T> Repository<T>() where T : BaseEntity
        {
            if (_repositories.Keys.Contains(typeof(T)))
                return _repositories[typeof(T)] as IRepository<T>;

            IRepository<T> repo = new BaseRepository<T>(_dbContext);
            _repositories.Add(typeof(T), repo);
            return repo;
        }
        
        public UnitOfWork(DbContextOptions<OrderContext> options)
        {
            _dbContext = new OrderContext(options);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}