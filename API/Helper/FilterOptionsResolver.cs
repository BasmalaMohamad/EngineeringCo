using AutoMapper;
using Core.Entities;

namespace API.Helper
{
    public class FilterOptionsResolver
    {
        private readonly IEnumerable<Product> _products;
        private readonly IMapper _mapper;

        public FilterOptionsResolver(IEnumerable<Product> products, IMapper mapper)
        {
            _products = products;
            _mapper = mapper;
        }

        public FilterOptions GenerateOptions()
        {
            var filterOptions = new FilterOptions()
            {
                ProductName = _products.Select(p => p.ProductName).ToList(),
                Model=_products.Select(p => p.Model).ToList(),
                Construction = _products.Select(p => p.Construction).ToList()
            };

            return filterOptions;
        }
    }
}
