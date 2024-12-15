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
            CreateMap<ProductDTO, Product>();
            // ForMember(d => d.ImageURL,)
            // ForMember(d => d.MapFrom<ProductUrlResolver>(), o => o.ImageURL);
            CreateMap<DocumentDTO, Documentation>().
                ForMember(d => d.DocumentID, o => o.MapFrom(o => o.Id));
            CreateMap<Documentation, DocumentDTO>().
                ForMember(d => d.Id, o => o.MapFrom(o => o.DocumentID)).
                ForMember(d => d.FileUrl, o => o.MapFrom<DocumentUrlResolver>());

            CreateMap<Accessories, AccessoriesDTO>().
                 ForMember(d => d.ImageURL, o => o.MapFrom<AccessoriesUrlResolver>());
            CreateMap<AccessoriesDTO, Accessories>();
            CreateMap<CategoryDTO, Category>();
            CreateMap<Category, CategoryDTO>();

        }

    }
}
