using API.DTO;
using AutoMapper;
using Core.Entities;

namespace API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDTO>().
                ForMember(d => d.ImageURL, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<Documentation, DocumentDTO>().
                ForMember(d => d.Id, o => o.MapFrom(o => o.DocumentID)).
                ForMember(d => d.FileUrl, o => o.MapFrom<DocumentUrlResolver>());
        }
    }
}
