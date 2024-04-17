using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Repsitories.Contract;
using Talabat.Infrastructure;

namespace Talabat.APIs.Extensions
{
	public static class ApplicationServicesExtension
	{
		public static IServiceCollection AddApplicationsService(this IServiceCollection services)
		{
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddAutoMapper(typeof(MappingProfiles));

			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{
					var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count > 0)
														 .SelectMany(p => p.Value.Errors)
														 .Select(e => e.ErrorMessage).ToList();
					var response = new ApiValidationErrorResponse()
					{
						Errors = errors
					};
					return new BadRequestObjectResult(response);
				};
			}
			);
			return services;
		}
	}
}
