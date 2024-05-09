using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Services.Contract;

namespace Talabat.APIs.Controllers
{
	[ApiExplorerSettings(IgnoreApi = true)]

	public class OrdersController : BaseApiController
	{
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public OrdersController(IOrderService orderService ,IMapper mapper)
        {
			_orderService = orderService;
			_mapper = mapper;
		}
		//[ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
		//[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

		[HttpPost]
		public async Task<ActionResult<Order>> CreateOrder (OrderDto orderDto)
		{
			var address = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
			var order = await _orderService.CreateOrder(orderDto.BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, address);
		    if(order is null)
			{
				return BadRequest(new ApiResponse(400));
			}
			return Ok(order);
		}
    }
}
