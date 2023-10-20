
using AutoMapper;
using E_commerce.Core.Entities;
using E_commerce.Core.Entities.identity;
using E_commerce.Core.Entities.Order_Aggregate;
using E_commerce.Dtos;

namespace E_commerce.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Brand,O => O.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.ProductType, O => O.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PicUrl,O => O.MapFrom<ProductPicUrlResolver>());

            CreateMap<E_commerce.Core.Entities.identity.Address, AddressDto>().ReverseMap();
            CreateMap<E_commerce.Core.Entities.Order_Aggregate.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(S => S.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(S => S.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(S => S.Product.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(S => S.Product.ProductName))
                .ForMember(d => d.PicUrl, o => o.MapFrom(S => S.Product.PicUrl))
                .ForMember(d => d.PicUrl, O => O.MapFrom<OrderItemPictureUrlResolver>()); 


        }
    }
}
