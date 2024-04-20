﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repsitories.Contract
{
	public interface IGenericRepository<T> where T:BaseEntity
	{
		Task<T?> GetAsync(int id);
		Task<IReadOnlyList<T>> GetAllAsync();

		Task<T?> GetWithSpecAsync(ISpecifications<T> spec);
		Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);

		Task<int> GetCountAsync(ISpecifications<T> spec);

	}
}
