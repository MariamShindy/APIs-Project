using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Services.Contract
{
	public interface IResponseCacheService
	{
		Task CacheResponeAsync(string Key, object Response, TimeSpan timeToLive);
		Task <string?> GetCacheResponseAsync(string Key);
	}
}
