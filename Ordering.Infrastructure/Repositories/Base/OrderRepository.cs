using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using Ordering.Domain.Repositories;
using Ordering.Infrastructure.Data;


namespace Ordering.Infrastructure.Repositories.Base
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {

        }
        public async Task<IEnumerable<Order>> GetOrdersBySellerUserName(string userName)
        {
            var orderList = await _dbContext.Orders.Where(s => s.SellerUserName == userName).ToListAsync();
            return orderList;   
        }
    }
}
