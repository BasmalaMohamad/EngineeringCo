using API.DTO;
using AutoMapper;
using Core.Entities;

namespace API.Helper
{
    public class ProductUrlResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration _Config;
        public ProductUrlResolver(IConfiguration config)
        {
            _Config = config;

        }
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImageURL))
            {
                return _Config["ApiUrl"] + source.ImageURL;
            }
            return null;
        }
    }
}
