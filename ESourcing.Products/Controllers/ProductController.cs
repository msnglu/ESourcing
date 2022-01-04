using ESourcing.Products.Entities;
using ESourcing.Products.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ESourcing.Products.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Variables
        private readonly IConfiguration _configuration;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;

        #endregion

        #region Constructor

        public ProductController(IConfiguration configuration,IProductRepository productRepository, ILogger<ProductController> logger)
        {
            _configuration = configuration;
            _productRepository = productRepository;
            _logger = logger;
        }

        #endregion

        #region CRUD

        [HttpGet]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)] 
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return Ok(products);    
        }

        [HttpGet("{id:length(24)}",Name ="GetProduct")]
        [ProducesResponseType( (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProduct (string id)
        {
            var product = await _productRepository.GetProducts(id);
            if (product == null)
            {
                _logger.LogError($"Product with id : {id} , hasn't been found in database");
                return NotFound();  
            }     
            return Ok(product); 
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _productRepository.Create(product);
            return CreatedAtRoute("GetProduct",new { id=product.Id},product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _productRepository.Update(product));
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            return Ok(await _productRepository.Delete(id));
        }       
        #endregion

    }
}
