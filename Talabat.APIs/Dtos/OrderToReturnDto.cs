using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.APIs.Dtos
{
	public class OrderToReturnDto
	{
        public int Id { get; set; }
		public string BuyerEmail { get; set; } = null!;
		public DateTimeOffset OrderDate { get; set; }
		public string Status { get; set; } 
		public Address ShippingAddress { get; set; } = null!;
		public string DeliveryMethod { get; set; }
		public decimal DeliveryMethodCost { get; set; }

		public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();
		public decimal Subtotal { get; set; } //Total cost - Shipping fees
		public decimal Total { get; set; }	
		public String PaymentIntentId { get; set; } = string.Empty;

	}
}
