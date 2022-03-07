using Ordering.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Entities
{
#pragma warning disable CS0436 // Type conflicts with imported type
    public class Order : Entity
#pragma warning restore CS0436 // Type conflicts with imported type
    {
        public string AuctionId { get; set; } = null!;
        public string SellerUserName { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }  
    }
}
