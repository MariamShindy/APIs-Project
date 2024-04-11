using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repsitories.Contract;
using Talabat.Infrastructure.Data;

namespace Talabat.Infrastructure
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly StoreContext _dbContext;

		public GenericRepository(StoreContext dbContext)
        {
			_dbContext = dbContext;
		}
        public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
		}

		public async Task<T?> GetAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}
	}
}
