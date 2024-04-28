using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Infrastructure._Data;

namespace Talabat.APIs.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class BuggyController : BaseApiController
	{
		private readonly StoreContext _dbContext;

		public BuggyController(StoreContext dbContext)
		{
			_dbContext = dbContext;
		}
		[HttpGet("notfound")]
		public ActionResult GetNotFoundRequest()
		{
			var product = _dbContext.Products.Find(100);
			if (product is null )
				return NotFound(new ApiResponse(404));
			return Ok(product);
		}

		[HttpGet("servererror")]
		public ActionResult GetServerError()
		{
			var product = _dbContext.Products.Find(100);
			var productToReturn = product.ToString();
			return Ok(productToReturn);
		}

		[HttpGet("badrequest")]
		public ActionResult GetBadRequest()
		{
			return BadRequest(new ApiResponse(400));
		}

		[HttpGet("badrequest/{id}")]
		public ActionResult GetBadRequest(int id)
		{
			return Ok();
		}

		[HttpGet("/unauthorized")]
		public ActionResult GetUnauthorizedError()
		{
			return Unauthorized(new ApiResponse(401));
		}
	}
}
