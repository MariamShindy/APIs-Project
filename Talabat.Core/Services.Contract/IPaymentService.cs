using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Basket;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Core.Services.Contract
{
	public interface IPaymentService
	{
		Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId);
		Task<Order?> UpdateOrderStatus(string paymentIntentId , bool isPaid);
	}
}
