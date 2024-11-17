using API.DTO;
using AutoMapper;
using Core.Entities;

namespace API.Helper
{
    public class AccessoriesUrlResolver : IValueResolver<Accessories, AccessoriesDTO, string>
    {
        private readonly IConfiguration _Config;
        public AccessoriesUrlResolver(IConfiguration config)
        {
            _Config = config;

        }
        public string Resolve(Accessories source, AccessoriesDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImageURL))
            {
                return _Config["ApiUrl"] + source.ImageURL;
            }
            return null;
        }
    }
}
