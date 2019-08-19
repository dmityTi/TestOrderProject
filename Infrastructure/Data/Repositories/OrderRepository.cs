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
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Order>> GetAllOrdersWithItemsAsync()
        {
            return await DbContext.Orders.AsNoTracking().Include(x => x.Articles)
                .Include(x => x.Payments)
                .Include(x => x.BillingAddress).ToListAsync();
        }

        public Order GetOrderByIdWithItems(long id, bool noTracking = true)
        {
            return DbContext.Orders.NoTracking(noTracking).Include(x => x.Articles)
                .Include(x => x.Payments)
                .Include(x => x.BillingAddress).FirstOrDefault(x => x.Id == id);
        }
        
    }
}