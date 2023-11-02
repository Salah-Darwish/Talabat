using AutoMapper;
using Talabat.Core.Entities;
using Talbat.Dtos;
using static System.Net.WebRequestMethods;

namespace Talbat.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>().ForMember(D => D.Brand, O => O.MapFrom(S => S.Brand.Name))
                .ForMember(D => D.Category, O => O.MapFrom(S => S.Category.Name))
            //     .ForMember(D => D.PictureUrl, O => O.MapFrom(S=> $"{"https://localhost:7002"}/{ S.PictureUrl}"))
            .ForMember(D => D.PictureUrl, O => O.MapFrom<ProductPictureUrl>()); 
                ;  
               
        }
    }
}
