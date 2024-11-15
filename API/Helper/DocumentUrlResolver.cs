using API.DTO;
using AutoMapper;
using Core.Entities;

namespace API.Helper
{
    public class DocumentUrlResolver : IValueResolver<Documentation, DocumentDTO, string>
    {
        private readonly IConfiguration _Config;
        public DocumentUrlResolver(IConfiguration config)
        {
            _Config = config;

        }
        public string Resolve(Documentation source, DocumentDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.FileURL))
            {
                return _Config["ApiUrl"] + source.FileURL;
            }
            return null;
        }
    }
}
