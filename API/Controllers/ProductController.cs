using AutoMapper;
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
        private readonly IMapper _mapper;

        // Constructor for dependency injection
        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _productRepository.GetProductsAsync();
            
            return Ok(products);
        }

        [HttpGet("{id}")]
        //public async Task<ActionResult<Product>> GetProductById(int id)
        //{
        //    var product = await _productRepository.GetProductByIdAsync(id);
                                            
        //    return Ok(product);
        //}
        
        public async Task<ActionResult<Documentation>> GetDocById(int id)
        {
            var doc = await _productRepository.GetDocByIdAsync(id);

            return Ok(doc);
        }

<<<<<<< HEAD
        
=======
>>>>>>> 96a022a0f166661176d807174d03e625b369fba7

    }
}