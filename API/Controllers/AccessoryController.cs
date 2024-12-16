using API.DTO;
using API.Helper;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructrue.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessoryController : ControllerBase
    {
        private readonly IAccessoryRepository _accessoryRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        public AccessoryController(IAccessoryRepository accessoryRepository, IMapper mapper, IWebHostEnvironment environment)
        {
            _accessoryRepository = accessoryRepository;
            _mapper = mapper;
            _environment = environment;

        }

        [HttpGet]
        public async Task<ActionResult<List<AccessoriesDTO>>> GetAccessory([FromQuery] AccessoriesSpecParams accessoriesParams)
        {
            var accessories = await _accessoryRepository.GetAccessoriesAsync();
            var accessoriesWithSpecifications = await _accessoryRepository.GetAccessoriesWithSpecificationsAsync(accessoriesParams);
            var accesfilterOptions = new AccesFilterOptionResolver(accessories, _mapper).GenerateOptions();
            var AccespaginationList = new AccesPaginationList
            {
                PageSize = accessoriesParams.PageSize,
                PageIndex = accessoriesParams.PageIndex,
                Count = await _accessoryRepository.CountAsync(),
                Data = _mapper.Map<IReadOnlyList<AccessoriesDTO>>(accessoriesWithSpecifications),
                AccessFilterOptions = accesfilterOptions

            };
            return Ok(AccespaginationList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccessoriesDTO>> GetAccessoryById(int id)
        {
            var accessory = await _accessoryRepository.GetAccessoryByIdAsync(id);
            if (accessory is null)
            {
                return NotFound();
            }
            var AccessoryMapped = _mapper.Map<AccessoriesDTO>(accessory);

            return Ok(AccessoryMapped);
        }

        [HttpPost]
        public async Task<ActionResult<Accessories>> CreateAccessory( AccessoriesDTO accessorydto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var accessoryMapped = _mapper.Map<Accessories>(accessorydto);
            return await _accessoryRepository.AddAccessory(accessoryMapped);
        }
    
        [HttpPut]
        public async Task<ActionResult<Accessories>> UpdateAccessory( int id,AccessoriesDTO accessorydto)
        {
            var accessoryMapped = _mapper.Map<Accessories>(accessorydto);
            return await _accessoryRepository.EditAccessory(id,accessoryMapped);
        }
        [HttpDelete("{id}")]
        public async Task DeleteAccessory(int id)
        {
             await _accessoryRepository.RemoveAccessory(id);
        }
     
      


        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("Invalid image file.");
            }

            string uploadPath = Path.Combine(_environment.WebRootPath, "Images/Parts");
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
            string fileUrl = $"/Images/Parts/{fileName}";
            return Ok(new { url = fileUrl });
        }
    }
}
