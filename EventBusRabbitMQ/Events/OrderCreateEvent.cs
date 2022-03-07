using EventBusRabbitMQ.Events.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusRabbitMQ.Events
{
    public class OrderCreateEvent :IEvent
    {
        public string Id { get; set; } = null!;
        public string AuctionId { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public string SellerUserName { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Quantity { get; set; }
    }
}
