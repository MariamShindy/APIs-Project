using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.OrderAggregate
{
	public class Order : BaseEntity
	{
        private Order()
        {}
        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subtotal)
		{
			BuyerEmail = buyerEmail;
			ShippingAddress = shippingAddress;
			DeliveryMethod= deliveryMethod;
			Items = items;
			Subtotal = subtotal;
		}

		public string BuyerEmail { get; set; } = null!;
		public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
		public OrderStatus Status { get; set; } = OrderStatus.Pending;
		public Address ShippingAddress { get; set; } = null!;
        public int? DeliveryMethodId { get; set; } //Foreign key 
        public  DeliveryMethod? DeliveryMethod { get; set; } //Navigational property one
		public ICollection<OrderItem> Items { get; set;} = new HashSet<OrderItem>();
        public decimal Subtotal { get; set; } //Total cost - shipping fees
        public String PaymentIntentId { get; set; } = string.Empty;

        //[NotMapped]
        //public decimal Total => DeliveryMethod.Cost + Subtotal; 

        public decimal GetTotal() => DeliveryMethod.Cost + Subtotal;
	}
}
 