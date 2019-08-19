using System;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : BaseEntity;
        Task<int> SaveChangesAsync();
        
        IOrderRepository OrderRepo { get; }
    }
}