using ESourcing.Sourcing.Data.Interface;
using ESourcing.Sourcing.Entities;
using ESourcing.Sourcing.Repositories.Interface;
using MongoDB.Driver;

namespace ESourcing.Sourcing.Repositories
{
    public class AuctionRepository : IAuctionRepository
    {
        #region Field
        private readonly ISourcingContext _context;
        #endregion

        #region CTOR
        public AuctionRepository(ISourcingContext context)
        {
            _context = context;
        }
        #endregion

        #region CRUD
        public async Task Create(Auction auction)
        {
            await _context.Auction.InsertOneAsync(auction);
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Auction> filter = Builders<Auction>.Filter.Eq(m => m.Id , id);
            DeleteResult deleteResult = await _context.Auction.DeleteOneAsync(filter);  
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;    
        }

        public async Task<Auction> GetAuction(string id)
        {
            return await _context.Auction.Find(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Auction> GetAuctionByName(string name)
        {
            FilterDefinition<Auction> filter = Builders<Auction>.Filter.Eq(m => m.Name, name);
            return await _context.Auction.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Auction>> GetAuctions()
        {
            return await _context.Auction.Find( a=> true).ToListAsync();
        }

        public async Task<bool> Update(Auction auction)
        {
            var updateResult = await _context.Auction.ReplaceOneAsync( a => a.Id.Equals(auction.Id),auction);
            return updateResult.IsAcknowledged &&  updateResult.ModifiedCount > 0;  
        }
        #endregion
    }
}
