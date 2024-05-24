using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Entities.Product;
using Talabat.Core.Repsitories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specifications.Order_Specs;

namespace Talabat.Service.OrderService
{
	public class OrderService : IOrderService
	{
		private readonly IBasketRepository _basketRepo;
		private readonly IUnitOfWork _unitOfWork;

		//private readonly IGenericRepository<Product> _productRepo;
		//private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepo;
		//private readonly IGenericRepository<Order> _orderRepo;

		public OrderService(IBasketRepository basketRepo , 
			                //IGenericRepository<Product> productRepo , 
			                //IGenericRepository<DeliveryMethod> deliveryMethodRepo ,
			                //IGenericRepository<Order> orderRepo
							IUnitOfWork unitOfWork)
        {
			_basketRepo = basketRepo;
		    _unitOfWork = unitOfWork;
			//_productRepo = productRepo;
			//_deliveryMethodRepo = deliveryMethodRepo;
			//_orderRepo = orderRepo;
		}
        public async Task<Order?> CreateOrder(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
		{
			var basket = await _basketRepo.GetBasketAsync(basketId);
			var orderItems = new List<OrderItem>();
			if(basket?.Items?.Count > 0)
			{
				var productRepository =  _unitOfWork.Repository<Product>();
				foreach (var item in basket.Items)
				{
					var product = await productRepository.GetByIdAsync(item.Id);
					var productItemOrdered = new ProductItemOrder(product.Id, product.Name, product.PictureUrl);
					var orderItem = new OrderItem(productItemOrdered , product.Price , item.Quantity);
					orderItems.Add(orderItem);
				}
			}
			var subtotal = orderItems.Sum(item => item.Price * item.Quantity);
			var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
			var order = new Order(
				buyerEmail : buyerEmail,
				shippingAddress : shippingAddress,
				deliveryMethod : deliveryMethod,
				items : orderItems,
				subtotal : subtotal
				);
			_unitOfWork.Repository<Order>().Add(order);
			var result = await _unitOfWork.CompleteAsync();
			if (result <= 0)
			{
				return null;
			}
			return order;
		}


		public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
		=> await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
		public Task<Order?> GetOrderByIdForUserAsync(string buyerEmail, int orderId)
		{
			var orderRepo = _unitOfWork.Repository<Order>();
			var orderSpec = new OrderSpecifications(orderId , buyerEmail);
			var order = orderRepo.GetWithSpecAsync(orderSpec);
			return order;
		}


		public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
		{
			var orderRepo = _unitOfWork.Repository<Order>();
			var spec = new OrderSpecifications(buyerEmail);
			var orders = await orderRepo.GetAllWithSpecAsync(spec);
			return orders;
		}
	}
}
