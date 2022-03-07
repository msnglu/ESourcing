using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ESourcing.Sourcing.Entities
{
    public class Auction
    {
        public Auction()
        {
            IncludedSellers = new List<string>();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public int Quantity { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }
        public List<string> IncludedSellers { get; set; } = null!;
    }

    public enum Status
    {
        Active = 0,
        Closed = 1,
        Passive = 2
    }
}

