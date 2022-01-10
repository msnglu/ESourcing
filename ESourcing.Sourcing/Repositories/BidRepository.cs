using ESourcing.Sourcing.Data.Interface;
using ESourcing.Sourcing.Entities;
using ESourcing.Sourcing.Repositories.Interface;
using MongoDB.Driver;

namespace ESourcing.Sourcing.Repositories
{
    public class BidRepository : IBidRepository
    {
        #region Field
        private readonly ISourcingContext _context;
        #endregion

        #region CTOR
        public BidRepository(ISourcingContext context)
        {
            _context = context;
        }
        #endregion

        #region CRUD
        public async Task<Bid> GetWinnerBid(string id)
        {
            List<Bid> birds = await GetBidsByAuctionId(id);
            return birds.OrderByDescending(a => a.Price).FirstOrDefault();
        }

        public async Task<List<Bid>> GetBidsByAuctionId(string id)
        {
            FilterDefinition<Bid> filter = Builders<Bid>.Filter.Eq(m => m.AuctionId, id);
            List<Bid> bids = await _context.Bid.Find(filter).ToListAsync();
            bids = bids.OrderByDescending(a => a.CreatedAt).GroupBy( m => m.SellerUserName).Select(a => new Bid
            {
                AuctionId = a.FirstOrDefault().AuctionId,
                Price = a.FirstOrDefault().Price,   
                CreatedAt = a.FirstOrDefault().CreatedAt,   
                SellerUserName = a.FirstOrDefault().SellerUserName,
                ProductId = a.FirstOrDefault().ProductId,
                Id = a.FirstOrDefault().Id
            }).ToList();

            return bids;    
        }

        public async Task SendBid(Bid bid)
        {
           await  _context.Bid.InsertOneAsync(bid);
        }
        #endregion
    }
}
