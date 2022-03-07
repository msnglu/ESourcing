using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ESourcing.Sourcing.Entities
{
    public class Bid
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public string AuctionId { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public string SellerUserName { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
