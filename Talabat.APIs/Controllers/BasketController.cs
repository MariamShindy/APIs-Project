using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repsitories.Contract;

namespace Talabat.APIs.Controllers
{

	public class BasketController : BaseApiController
	{
		private readonly IBasketRepository _basketRepository;

		public BasketController(IBasketRepository basketRepository)
        {
			_basketRepository = basketRepository;
		}

		[HttpGet] //GET : /api/basket?id=
		public async Task<ActionResult<CustomerBasket>> GetBasket (string id)
		{
			var basket = await _basketRepository.GetBasketAsync(id);
			return Ok(basket ?? new CustomerBasket(id));
		}

		[HttpPost] //POST : /api/basket
		public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
		{
			var createdOrUpdatedBasket = await _basketRepository.UpdateBasketAsync(basket);
			if(createdOrUpdatedBasket is null) { return BadRequest(new ApiResponse(400)); }
			return Ok(createdOrUpdatedBasket);
		}
		[HttpDelete] //DELETE : /api/basket?id
		 public async Task DeleteBasket (string id)
		{
			await _basketRepository.DeleteBasketAsync(id);
		}

    }
}
