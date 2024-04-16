using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			//for member : destination , mapfrom : source
			CreateMap<Product, ProductToReturnDto>().ForMember(d => d.Brand, O => O.MapFrom(s => s.Brand.Name))
				                                    .ForMember(d => d.Category , O => O.MapFrom(s => s.Category.Name));
		}
	}
}
