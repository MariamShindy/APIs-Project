using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities.Basket;
using Talabat.Core.Entities.Identity;
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
			CreateMap<Address, AddressDto>();
		}

	}
}