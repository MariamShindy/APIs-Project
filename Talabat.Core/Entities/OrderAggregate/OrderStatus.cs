using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.OrderAggregate
{
	public enum OrderStatus
	{
		Pending,
		PaymentRecieved,
		PaymentFailed
	}
}
