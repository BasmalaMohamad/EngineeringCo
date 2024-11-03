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

        /*public FilterOptions GenerateOptions()
        {
            

            return filterOptions;
        }*/
    }
}
