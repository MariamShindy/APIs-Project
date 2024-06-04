using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Basket;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Services.Contract;

namespace Talabat.APIs.Controllers
{
	[Authorize]
	public class PaymentController : BaseApiController
	{
		private readonly IPaymentService _paymentService;
		private readonly ILogger<PaymentController> _logger;
		private const string whSecret = "whsec_46fd657505e08f6e64e1f12b8a000f3c759dc915425987c51ccd7fd5f1162d19";

		public PaymentController(IPaymentService paymentService ,ILogger<PaymentController> logger)
        {
			_paymentService = paymentService;
			_logger = logger;
		}

		[ProducesResponseType(typeof(CustomerBasket),StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

		[HttpPost("{basketId}")]
		public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
		{
			var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
			if (basketId is null)
				return BadRequest(new ApiResponse(400, "An Error With Your Basket"));
			return Ok(basket);
		}
		[HttpPost("webhook")]
		public async Task<IActionResult> WebHook()
		{
			var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
			try
			{
				var stripeEvent = EventUtility.ConstructEvent(json,
					Request.Headers["Stripe-Signature"], whSecret);

				var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
				Talabat.Core.Entities.OrderAggregate.Order? order;
				// Handle the event
				switch (stripeEvent.Type)
				{
					case Events.PaymentIntentSucceeded :
						order = await _paymentService.UpdateOrderStatus(paymentIntent.Id, true);
						_logger.LogInformation("Order is succeeded {0}", order?.PaymentIntentId);
						_logger.LogInformation("Unhandled event type: {0}", stripeEvent.Type);
						break;
					case Events.PaymentIntentPaymentFailed :
						order = await _paymentService.UpdateOrderStatus(paymentIntent.Id, false);
						_logger.LogInformation("Order is failed {0}", order?.PaymentIntentId);
						_logger.LogInformation("Unhandled event type: {0}", stripeEvent.Type);
						break;
				}

				return Ok();
			}
			catch (StripeException e)
			{
				return BadRequest();
			}
		}
    }
}
