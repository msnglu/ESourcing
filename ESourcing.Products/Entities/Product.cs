using MongoDB.Bson.Serialization.Attributes;

namespace ESourcing.Products.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        [BsonElement("Name")]
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Image { get; set; } = null!;
        public decimal Price { get; set; } 
    }
}
