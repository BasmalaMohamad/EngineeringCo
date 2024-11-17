using AutoMapper;
using Core.Entities;

namespace API.Helper
{
    public class AccesFilterOptionResolver
    {
        private readonly IEnumerable<Accessories> _accessories;
        private readonly IMapper _mapper;

        public AccesFilterOptionResolver(IEnumerable<Accessories> accessories, IMapper mapper)
        {
            _accessories = accessories;
            _mapper = mapper;
        }

        public AccessFilterOptions GenerateOptions()
        {
            var accesfilterOptions = new AccessFilterOptions()
            {
                Name = _accessories.Select(p => p.Name).Distinct().ToList(),
                PumpName = _accessories.Select(p => p.PumpName).Distinct().ToList(),
                Model = _accessories.Select(p => p.Model).Distinct().ToList(),
                Construction = _accessories.Select(p => p.Construction).Distinct().ToList()
            };

            return accesfilterOptions;
        }
    }
}
