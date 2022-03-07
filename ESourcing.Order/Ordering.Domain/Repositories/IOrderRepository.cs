using Ordering.Domain.Entities;
using Ordering.Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Repositories
{
#pragma warning disable CS0436 // Type conflicts with imported type
    public interface IOrderRepository : IRepository<Order>
#pragma warning restore CS0436 // Type conflicts with imported type
    {
#pragma warning disable CS0436 // Type conflicts with imported type
        Task<IEnumerable<Order>> GetOrderBySellerUserName(string userName);
#pragma warning restore CS0436 // Type conflicts with imported type
    }
}
