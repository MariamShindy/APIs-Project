using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repsitories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Product_Specs;
using Talabat.Infrastructure;

namespace Talabat.APIs.Controllers
{
	public class ProductsController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productsRepo;
		private readonly IGenericRepository<ProductCategory> _categoriesRepo;
		private readonly IGenericRepository<ProductBrand> _brandsRepo;
		private readonly IMapper _mapper;

		public ProductsController(IGenericRepository<Product> productsRepo , IGenericRepository<ProductBrand> brandsRepo , IGenericRepository<ProductCategory> categoriesRepo, IMapper mapper )
        {
			_productsRepo = productsRepo;
			_categoriesRepo = categoriesRepo;
			_brandsRepo = brandsRepo;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
		{
			var spec = new ProductWithBrandAndCategorySpecifications();
			var products = await _productsRepo.GetAllWithSpecAsync(spec);
			return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products));
		}
		[ProducesResponseType(typeof(ProductToReturnDto),StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

		[HttpGet("{id}")]
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(id);
			var product = await _productsRepo.GetWithSpecAsync(spec);
			if (product is null)
			{
				return NotFound(new ApiResponse(404)); 
			}
			return Ok(_mapper.Map<Product,ProductToReturnDto>(product)); //200
		}

		[HttpGet("brands")]
		public async Task<ActionResult<IEnumerable<ProductBrand>>> GetBrands()
		{
			var brands = await _brandsRepo.GetAllAsync();
			return Ok(brands);
		}
		[HttpGet("categories")]
		public async Task<ActionResult<IEnumerable<ProductCategory>>> GetCategories()
		{
			var categories = await _brandsRepo.GetAllAsync();
			return Ok(categories);
		}
	}
}
