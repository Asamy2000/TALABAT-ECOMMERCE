using AutoMapper;
using E_commerce.Core.Entities;
using E_commerce.Dtos;

namespace E_commerce.Helper
{
    public class ProductPicUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPicUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PicUrl))
                return $"{_configuration["APiBaseUrl"]}{source.PicUrl}";


            return string.Empty;
        }
    }
}
