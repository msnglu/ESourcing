using ESourcing.Sourcing.Entities;
using ESourcing.Sourcing.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ESourcing.Sourcing.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        #region Fields
        private IBidRepository _bidRepository;
        #endregion

        #region Ctor
        public BidController(IBidRepository bidRepository)
        {
            _bidRepository = bidRepository;
        }
        #endregion

        [HttpPost]
        [ProducesResponseType(typeof(Bid), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SendBid([FromBody] Bid bid)
        {
            await _bidRepository.SendBid(bid);
            return Ok(bid);
        }

        [HttpGet("GetBidByAuctionId")]
        [ProducesResponseType(typeof(IEnumerable<Bid>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Bid>>> BetBidByAuctionId(string id)
        {
            IEnumerable<Bid> bids = await _bidRepository.GetBidsByAuctionId(id);
            return Ok(bids);
        }
        [HttpGet("GetWinnerBid")]
        [ProducesResponseType(typeof(Bid), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Bid>> GetWinnerBid(string id)
        {
            Bid bid  = await _bidRepository.GetWinnerBid(id);
            return Ok(bid);
        }
    }
}
