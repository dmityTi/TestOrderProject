using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces.Data
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllOrdersWithItemsAsync();
        Order GetOrderByIdWithItems(long id, bool noTracking = true);
    }
}