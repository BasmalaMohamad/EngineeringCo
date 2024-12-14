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
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;


        // Constructor for dependency injection
        public ProductController(IProductRepository productRepository, IMapper mapper, IWebHostEnvironment environment)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _environment = environment;
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
            Console.WriteLine(JsonConvert.SerializeObject(productdto));
            if (!ModelState.IsValid)
            {
                return false;
            }
            var productMapped = _mapper.Map<Product>(productdto);
            return await _productRepository.AddProduct(productMapped);
        }
        [HttpPut]
        public async Task<bool> UpdateProduct([FromQuery] ProductDTO productdto)
        {
            var productMapped = _mapper.Map<Product>(productdto);
            return await _productRepository.EditProduct(productMapped);
        }
        [HttpDelete("{id}")]
        public async Task<bool> DeleteProduct(int id)
        {
            return await _productRepository.RemoveProduct(id);
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("Invalid image file.");
            }

            string uploadPath = Path.Combine(_environment.WebRootPath, "Images/Pumps");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            string filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            // Return the URL of the uploaded image
            string fileUrl = $"{Request.Scheme}://{Request.Host}/Images/Pumps/{fileName}";
            return Ok(fileUrl);
        }

        // Endpoint for uploading a document
        [HttpPost("upload-document")]
        public async Task<IActionResult> UploadDocument(IFormFile document)
        {
            if (document == null || document.Length == 0)
            {
                return BadRequest("Invalid document file.");
            }

            string uploadPath = Path.Combine(_environment.WebRootPath, "Docs");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(document.FileName)}";
            string filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await document.CopyToAsync(stream);
            }

            // Simulate saving document ID in the database (this could be a real DB operation)
            // int documentId = new Random().Next(1, 10000);

            string fileUrl = $"{Request.Scheme}://{Request.Host}/Docs/{fileName}";
            return Ok(fileUrl);
        }

    }
}