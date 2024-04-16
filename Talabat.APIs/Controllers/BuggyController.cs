using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Infrastructure.Data;

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
				return NotFound();
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
			return BadRequest();
		}

		[HttpGet("badrequest/{id}")]
		public ActionResult GetBadRequest(int id)
		{
			return Ok();
		}

	}
}
