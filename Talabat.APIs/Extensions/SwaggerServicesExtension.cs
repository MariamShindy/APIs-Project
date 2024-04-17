using Microsoft.AspNetCore.Builder;

namespace Talabat.APIs.Extensions
{
	public static class SwaggerServicesExtension
	{
		public static IServiceCollection AddSwaggerService (this IServiceCollection services)
		{
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			return services;
		}
		public static WebApplication UseSwaggerMiddleware (this WebApplication app)
		{
			app.UseSwagger();
			app.UseSwaggerUI();
			return app;
		}
	}
}
