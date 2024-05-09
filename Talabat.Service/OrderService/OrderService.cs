using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Entities.Product;
using Talabat.Core.Repsitories.Contract;
using Talabat.Core.Services.Contract;

namespace Talabat.Service.OrderService
{
	public class OrderService : IOrderService
	{
		private readonly IBasketRepository _basketRepo;
		private readonly IGenericRepository<Product> _productRepo;
		private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepo;
		private readonly IGenericRepository<Order> _orderRepo;

		public OrderService(IBasketRepository basketRepo , 
			                IGenericRepository<Product> productRepo , 
			                IGenericRepository<DeliveryMethod> deliveryMethodRepo ,
			                IGenericRepository<Order> orderRepo)
        {
			_basketRepo = basketRepo;
			_productRepo = productRepo;
			_deliveryMethodRepo = deliveryMethodRepo;
			_orderRepo = orderRepo;
		}
        public async Task<Order> CreateOrder(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
		{
			var basket = await _basketRepo.GetBasketAsync(basketId);
			var orderItems = new List<OrderItem>();
			if(basket?.Items?.Count > 0)
			{
				foreach (var item in basket.Items)
				{
					var product = await _productRepo.GetAsync(item.Id);
					var productItemOrdered = new ProductItemOrder(product.Id, product.Name, product.PictureUrl);
					var orderItem = new OrderItem(productItemOrdered , product.Price , item.Quantity);
					orderItems.Add(orderItem);
				}
			}
			var subtotal = orderItems.Sum(item => item.Price * item.Quantity);
			//var deliveryMethod = await _deliveryMethodRepo.GetAsync(deliveryMethodId);
			var order = new Order(
				buyerEmail : buyerEmail,
				shippingAddress : shippingAddress,
				deliveryMethodId : deliveryMethodId,
				items : orderItems,
				subtotal : subtotal
				);
			_orderRepo.Add(order);

		}


		public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
		{
			throw new NotImplementedException();
		}

		public Task<Order> GetOrderByIdForUserAsync(string buyerEmail, int orderId)
		{
			throw new NotImplementedException();
		}


		public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
		{
			throw new NotImplementedException();
		}
	}
}
