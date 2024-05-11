using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Core.Specifications.Order_Specs
{
	public class OrderSpecifications :  BaseSpecifications<Order>
	{
        public OrderSpecifications(string buyerEmail) : base(O => O.BuyerEmail == buyerEmail)
		{
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);

            AddOrderByDesc(O => O.OrderDate);
        }
        public OrderSpecifications(int orderId , string buyerEmail) : base(O => O.Id == orderId && O.BuyerEmail == buyerEmail) 
        {
			Includes.Add(O => O.DeliveryMethod);
			Includes.Add(O => O.Items);
		}

    }
}
