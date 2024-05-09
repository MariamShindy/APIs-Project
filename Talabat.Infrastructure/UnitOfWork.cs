using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Entities.Product;
using Talabat.Core.Repsitories.Contract;
using Talabat.Infrastructure._Data;
using Talabat.Infrastructure.GenericRepoistory;

namespace Talabat.Infrastructure
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly StoreContext _dbContext;
        private Hashtable _repositories;

        //public IGenericRepository<Product> ProductsRepo {get; set;}
        //public IGenericRepository<ProductBrand> BrandsRepo {get; set;}
        //public IGenericRepository<ProductCategory> CategoriesRepo {get; set;}
        //public IGenericRepository<DeliveryMethod> DeliveryMethodsRepo {get; set;}
        //public IGenericRepository<OrderItem> OrderItemsRepo {get; set;}
        //public IGenericRepository<Order> OrdersRepo {get; set;}
        //      public UnitOfWork(StoreContext dbContext)
        //      {
        //	_dbContext = dbContext;
        //	ProductsRepo = new GenericRepository<Product>(_dbContext);
        //	BrandsRepo = new GenericRepository<ProductBrand>(_dbContext);
        //	CategoriesRepo = new GenericRepository<ProductCategory>(_dbContext);
        //	DeliveryMethodsRepo = new GenericRepository<DeliveryMethod>(_dbContext);
        //	OrderItemsRepo = new GenericRepository<OrderItem>(_dbContext);
        //	OrdersRepo = new GenericRepository<Order>(_dbContext);
        //}

        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
			_repositories = new Hashtable();
        }
		public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
		{
            var key = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(key))
            {
                var repository = new GenericRepository<TEntity>(_dbContext);
                _repositories.Add(key, repository);
            }
            return _repositories[key] as IGenericRepository<TEntity>;
		}
        public async Task<int> CompleteAsync()
	       => await _dbContext.SaveChangesAsync();

		public async ValueTask DisposeAsync()
            => await _dbContext.DisposeAsync();
		

	}
}
