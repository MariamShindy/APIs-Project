﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Core.Services.Contract
{
	public interface IOrderService
	{
		Task <Order?> CreateOrder(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress);
		Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
		Task<Order?>GetOrderByIdForUserAsync (string buyerEmail,int orderId);
		Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync(); 
	}
}
