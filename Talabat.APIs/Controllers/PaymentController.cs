using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Basket;
using Talabat.Core.Services.Contract;

namespace Talabat.APIs.Controllers
{
	[Authorize]
	public class PaymentController : BaseApiController
	{
		private readonly IPaymentService _paymentService;

		public PaymentController(IPaymentService paymentService)
        {
			_paymentService = paymentService;
		}

		[ProducesResponseType(typeof(CustomerBasket),StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

		[HttpGet("{basketId}")]
		public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
		{
			var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
			if (basketId is null)
				return BadRequest(new ApiResponse(400, "An Error With Your Basket"));
			return Ok(basket);
		}
    }
}
