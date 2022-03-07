using Ordering.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Entities
{
    public class Order :Entity
    {
        public string AuctıonID { get; set; } = null!;
        public string SellerUserName { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }  
    }
}
