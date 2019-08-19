using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DTOs.OrderAggregate;

namespace Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetAllOrders();
        OrderDTO GetOrder(long id, bool noTracking = false);
        Task CreateOrders(IEnumerable<OrderDTO> orders);
        Task UpdateOrder(OrderDTO order);
    }
}