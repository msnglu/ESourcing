using ESourcing.Sourcing.Data.Interface;
using ESourcing.Sourcing.Entities;
using ESourcing.Sourcing.Settings.Interface;
using MongoDB.Driver;

namespace ESourcing.Sourcing.Data
{
    public class SourcingContext : ISourcingContext
    {
        public SourcingContext(ISourcingDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Auction = database.GetCollection<Auction>(nameof(Auction));
            Bid = database.GetCollection<Bid>(nameof(Bid));
        }
        public IMongoCollection<Auction> Auction { get; }

        public IMongoCollection<Bid> Bid { get; }
    }
}
