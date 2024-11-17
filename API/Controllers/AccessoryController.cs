using API.DTO;
using API.Helper;
using AutoMapper;
using Core.Interfaces;
using Core.Specifications;
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
            var accessoriesWithSpecifications = await _accessoryRepository.GetAccessoriesWithSpecificationsAsync(accessoriesParams);
            var accesfilterOptions = new AccesFilterOptionResolver(accessoriesWithSpecifications , _mapper).GenerateOptions();
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

        /*[HttpGet("document/{id}")]
        public async Task<ActionResult<DocumentDTO>> GetDocById(int id)
        {
            var doc = await _accessoryRepository.GetDocByIdAsync(id);
            var docMapped = _mapper.Map<DocumentDTO>(doc);

            return Ok(docMapped);
        }*/


    }
}
