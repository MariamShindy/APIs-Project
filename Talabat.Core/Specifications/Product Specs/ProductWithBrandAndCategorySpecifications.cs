using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Product_Specs
{
	public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
	{
        //This constructor will be used to create an object that will be used to get all products
        public ProductWithBrandAndCategorySpecifications(string? sort , int? brandId , int? categoryId) : base(P =>
		
		                                                              (!brandId.HasValue    || P.BrandId == brandId.Value) &&
		                                                              (!categoryId.HasValue || P.CategoryId == categoryId.Value)
		)
		{
			//AddIncludes();
			Includes.Add(P => P.Brand);
			Includes.Add(P => P.Category);

			if (!string.IsNullOrEmpty(sort))
			{
				switch(sort)
				{
					case "priceAsc":
						//OrderBy = P => P.Price;
						AddOrderBy(P => P.Price);
						break;
					case "priceDesc":
						//OrderByDesc = P => P.Price;
						AddOrderByDesc(P => P.Price);
						break;
					default:
						//OrderBy = P => P.Name;
						AddOrderBy(P => P.Name);
						break;
				}
			}
			else
			{
				AddOrderBy(P => P.Name);
			}
		}
		//This constructor will be used to create an object that will be used to get a specifc product with id
		public ProductWithBrandAndCategorySpecifications(int id ) : base(P => P.Id == id)
        {
			//AddIncludes();
			Includes.Add(P => P.Brand);
			Includes.Add(P => P.Category);
		}
		//private void AddIncludes()
		//{
		//	Includes.Add(P => P.Brand);
		//	Includes.Add(P => P.Category);
		//}
	}
}
