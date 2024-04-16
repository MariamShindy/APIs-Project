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
        public ProductWithBrandAndCategorySpecifications() : base()
		{
			//AddIncludes();
			Includes.Add(P => P.Brand);
			Includes.Add(P => P.Category);
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
