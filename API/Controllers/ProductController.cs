using API.DTO;
using API.Helper;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructrue.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        public async Task<ActionResult<List<ProductDTO>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            var products = await _productRepository.GetProductsAsync();
            var productWithSpecifications = await _productRepository.GetProductsWithSpecificationsAsync(productParams);
            var filterOptions = new FilterOptionsResolver(products, _mapper).GenerateOptions();
            var paginationList = new PaginationList
            {
                PageSize = productParams.PageSize,
                PageIndex = productParams.PageIndex,
                Count = await _productRepository.CountAsync(),
                Data = _mapper.Map<IReadOnlyList<ProductDTO>>(productWithSpecifications),
                FilterOptions = filterOptions

            };
            return Ok(paginationList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product is null)
            {
                return NotFound();
            }
            var productMapped = _mapper.Map<ProductDTO>(product);

            return Ok(productMapped);
        }

        [HttpGet("document/{id}")]
        public async Task<ActionResult<DocumentDTO>> GetDocById(int id)
        {
            var doc = await _productRepository.GetDocByIdAsync(id);
            var docMapped = _mapper.Map<DocumentDTO>(doc);

            return Ok(docMapped);
        }
        [HttpPost]
        public async Task<bool> CreateProduct([FromQuery] ProductDTO productdto)
        {
            if(!ModelState.IsValid)
            {
                return false;
            }
            var productMapped = _mapper.Map<Product>(productdto);
            return await _productRepository.AddProduct(productMapped);
        }
        [HttpPut]
        public async Task<bool> UpdateProduct([FromQuery] int productId, [FromQuery] string productName, [FromQuery] string imageURL, [FromQuery] string model, [FromQuery] string construction, [FromQuery] float inletSize, [FromQuery] float outletSize, [FromQuery] int documentID)
        {
            //var productMapped = _mapper.Map<Product>(productdto);
            return await _productRepository.EditProduct(new Product()
            {
                ProductID = productId,
                ProductName = productName,
                ImageURL = imageURL,
                Model = model,
                Construction = construction,
                InletSize = inletSize,
                OutletSize = outletSize
            });
        }
        [HttpDelete]
        public async Task<bool> DeleteProduct([FromQuery] int id)
        {
            return await _productRepository.RemoveProduct(id);
        }

    }
}