/*using Core.Entities;
using Infrastructrue.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly StoreContext storeContext;
        public ProductController(StoreContext _storeContext)
        {
            _storeContext = _storeContext ?? throw new ArgumentNullException(nameof(storeContext));
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Product?>> getData(int id)
        {
            var p = await storeContext.Products.Where(c => c.ProductID == id).FirstOrDefaultAsync();
            return Ok(p);
        }
    }
}
*/
using Core.Entities;
using Core.Interfaces;
using Infrastructrue.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        // Constructor for dependency injection
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _productRepository.GetProductsAsync();
            
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
                                            
            return Ok(product);
        }
    }
}