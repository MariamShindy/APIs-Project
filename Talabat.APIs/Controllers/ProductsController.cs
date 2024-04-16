﻿using Microsoft.AspNetCore.Mvc;
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

		public ProductsController(IGenericRepository<Product> productsRepo)
        {
			_productsRepo = productsRepo;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			var spec = new ProductWithBrandAndCategorySpecifications();
			var products = await _productsRepo.GetAllWithSpecAsync(spec);
			return Ok(products);
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var product = await _productsRepo.GetAsync(id);
			if (product is null)
			{
				return NotFound(new {Message = "Not found" , StatusCode = 404});//404
			}
			return Ok(product); //200
		}
    }
}
