using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.OrderAggregate
{
	public class OrderItem : BaseEntity
	{
        public OrderItem()
        {
            
        }

		public OrderItem(ProductItemOrder product, decimal price, int quantity)
		{
			Product = product;
			Price = price;
			Quantity = quantity;
		}

		public ProductItemOrder Product { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
