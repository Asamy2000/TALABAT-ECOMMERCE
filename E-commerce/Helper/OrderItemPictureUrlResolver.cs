using AutoMapper;
using E_commerce.Core.Entities.Order_Aggregate;
using E_commerce.Dtos;

namespace E_commerce.Helper
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product.PicUrl))
                return $"{_configuration["APiBaseUrl"]}{source.Product.PicUrl}";


            return string.Empty;
        }
    }
}
