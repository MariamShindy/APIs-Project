using Talabat.Core.Entities;
using Talabat.Core.Repsitories.Contract;
using Talabat.Infrastructure;

namespace Talabat.APIs.Controllers
{
	public class ProductController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productsRepo;

		public ProductController(IGenericRepository<Product> productsRepo)
        {
			_productsRepo = productsRepo;
		}
    }
}
