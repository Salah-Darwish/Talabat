using AutoMapper;
using Talabat.Core.Entities;
using Talbat.Dtos;

namespace Talbat.Helpers
{
    public class ProductPictureUrl : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrl(IConfiguration configuration)
        {
           _configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_configuration["BaseUrl"]}/{source.PictureUrl}";
            return string.Empty; 
        }
    }
}
