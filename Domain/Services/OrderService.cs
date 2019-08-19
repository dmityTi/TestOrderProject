using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.DTOs.OrderAggregate;
using Domain.Entities;
using Domain.Interfaces.Data;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _db;

        public OrderService(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrders()
        {
            var orders = await _db.OrderRepo.GetAllOrdersWithItemsAsync();
            return Mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(orders);
        }

        public OrderDTO GetOrder(long id, bool noTracking = false)
        {
            var order = _db.OrderRepo.GetOrderByIdWithItems(id, noTracking);
            return Mapper.Map<Order, OrderDTO>(order);
        }

        public async Task CreateOrders(IEnumerable<OrderDTO> orders)
        {
            if (orders == null)
                throw new  ArgumentNullException(nameof(orders));

            var entities = Mapper.Map<IEnumerable<OrderDTO>, IEnumerable<Order>>(orders);
            
            _db.Repository<Order>().AddRange(entities);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateOrder(OrderDTO order)
        {
            var entity = Mapper.Map<OrderDTO, Order>(order);
            _db.Repository<Order>().Update(entity);
            await _db.SaveChangesAsync();
        }
        
        
    }
}