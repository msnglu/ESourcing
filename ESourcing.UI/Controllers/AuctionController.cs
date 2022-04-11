using ESourcing.Core.Repositories;
using ESourcing.Core.ResultModels;
using ESourcing.UI.Clients;
using ESourcing.UI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESourcing.UI.Controllers
{
    //[Authorize]
    public class AuctionController : Controller
    {

        private readonly IUserRepository _userRepository;
        private readonly ProductClient _productClient;
        private readonly AuctionClient _auctionClient;


        public AuctionController(IUserRepository userRepository, ProductClient productClient, AuctionClient auctionClient)
        {
            _userRepository = userRepository;
            _productClient = productClient;
            _auctionClient = auctionClient;
        }

        public IActionResult Index()
        {
            List<AuctionViewModel> model = new List<AuctionViewModel>();

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var productList = await  _productClient.GetProducts();
            if(productList.IsSuccess)
                ViewBag.ProductList = productList.Data;
            var userList =await _userRepository.GetAllAsync();
            ViewBag.UserList=userList;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AuctionViewModel model)
        {
            model.Status = 1;
            model.CreatedAt = DateTime.Now;
            var createAuction = await _auctionClient.CreateAuction(model);
            if (createAuction.IsSuccess)
                return RedirectToAction("Index");
            return  View(model);
        }
        public IActionResult Detail()
        {

            return View();
        }

    }
}
