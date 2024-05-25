using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities.Basket;
using Talabat.Core.Repsitories.Contract;
using Product = Talabat.Core.Entities.Product.Product; 
using Talabat.Core.Services.Contract;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Service.PaymentService
{
	public class PaymentService : IPaymentService
	{
		private readonly IConfiguration _configuration;
		private readonly IBasketRepository _basketRepository;
		private readonly IUnitOfWork _unitOfWork;

		public PaymentService(IConfiguration configuration ,IBasketRepository basketRepository ,IUnitOfWork unitOfWork)
        {
			_configuration = configuration;
			_basketRepository = basketRepository;
			_unitOfWork = unitOfWork;
		}
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId)
		{
			StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];
			var basket = await _basketRepository.GetBasketAsync(basketId);
			if (basket is null)
			{
				return null;
			}

			var shippingPrice = 0m;

			if (basket.DeliveryMethodId.HasValue)
			{
				var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value) ;
				shippingPrice = deliveryMethod.Cost;
				basket.ShippingPrice = shippingPrice;
			}
			if (basket.Items?.Count > 0)
			{
				var productRepo = _unitOfWork.Repository<Product>();
				foreach (var item in basket.Items)
				{
					var product = await productRepo.GetByIdAsync(item.Id);	
					if (item.Price != product.Price)
					{
						item.Price = product.Price;
					}
				}
			}
			PaymentIntent paymentIntent;
			PaymentIntentService paymentIntentService = new();
			if (string.IsNullOrEmpty(basket.PaymentIntentId)) // create new paymentIntent
			{
				var options = new PaymentIntentCreateOptions()
				{
					Amount = (long)basket.Items.Sum(Item => Item.Price * 100 * Item.Quantity) + (long)shippingPrice,
					Currency = "usd",
					PaymentMethodTypes = new List<string>() { "card" }
				};
				paymentIntent = await paymentIntentService.CreateAsync(options); //Inegration with stripe 
				basket.PaymentIntentId = paymentIntent.Id;
				basket.ClientSecret = paymentIntent.ClientSecret;
			}
			else // update existing paymentIntent
			{
				var options = new PaymentIntentUpdateOptions()
				{
					Amount = (long)basket.Items.Sum(Item => Item.Price * 100 * Item.Quantity) + (long)shippingPrice
				};
				await paymentIntentService.UpdateAsync(basket.PaymentIntentId, options);
			}
			await _basketRepository.UpdateBasketAsync(basket);
			return basket;
		}

	}
}
