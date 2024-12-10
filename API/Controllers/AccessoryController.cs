using API.DTO;
using API.Helper;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructrue.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessoryController : ControllerBase
    {
        private readonly IAccessoryRepository _accessoryRepository;
        private readonly IMapper _mapper;
        public AccessoryController(IAccessoryRepository accessoryRepository, IMapper mapper)
        {
            _accessoryRepository = accessoryRepository;
            _mapper = mapper;
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
        public async Task<bool> CreateAccessory([FromQuery] AccessoriesDTO accessorydto)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }
            var accessoryMapped = _mapper.Map<Accessories>(accessorydto);
            return await _accessoryRepository.AddAccessory(accessoryMapped);
        }
        [HttpPut]
        public async Task<bool> UpdateAccessory([FromQuery] int id, [FromQuery] AccessoriesSpecParams accessorydto)
        {
            var accessoryMapped = _mapper.Map<Accessories>(accessorydto);
            return await _accessoryRepository.EditAccessory(accessoryMapped);
        }
        [HttpDelete]
        public async Task<bool> DeleteAccessory([FromQuery] int id)
        {
            return await _accessoryRepository.RemoveAccessory(id);
        }



    }
}
