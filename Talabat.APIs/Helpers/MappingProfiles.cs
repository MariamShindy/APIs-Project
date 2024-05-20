using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities.Basket;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Entities.Product;

namespace Talabat.APIs.Helpers
{
    public class MappingProfiles : Profile
	{

		public MappingProfiles()
		{
			//for member : destination , mapfrom : source
			CreateMap<Product, ProductToReturnDto>().ForMember(p => p.Brand, O => O.MapFrom(s => s.Brand.Name))
													.ForMember(p => p.Category, O => O.MapFrom(s => s.Category.Name))
													.ForMember(p => p.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());
			CreateMap<CustomerBasketDto, CustomerBasket>();
			CreateMap<BasketItemDto, BasketItem>();
			CreateMap<Talabat.Core.Entities.Identity.Address, AddressDto>().ReverseMap();
			CreateMap< Talabat.Core.Entities.OrderAggregate.Address,AddressDto >().ReverseMap();
			CreateMap<Order, OrderToReturnDto>().ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
				                                .ForMember(d => d.DeliveryMethodCost , o => o.MapFrom(s => s.DeliveryMethod.Cost));

			CreateMap<OrderItem, OrderItemDto>().ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
											   .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
											   .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Product.PictureUrl))
											   .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());
		}

	}
}