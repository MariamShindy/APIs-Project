using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Product;

namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
	{
        //This constructor will be used to create an object that will be used to get all products
        public ProductWithBrandAndCategorySpecifications(ProductSpecParams specParams) : base(P =>
		                                                              (string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search)&&
		                                                              (!specParams.BrandId.HasValue    || P.BrandId == specParams.BrandId.Value) &&
		                                                              (!specParams.CategoryId.HasValue || P.CategoryId == specParams.CategoryId.Value)
		))
		{
			//AddIncludes();
			Includes.Add(P => P.Brand);
			Includes.Add(P => P.Category);

			if (!string.IsNullOrEmpty(specParams.Sort))
			{
				switch(specParams.Sort)
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
			//total products = 18 , pagesize = 5
			ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);
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
