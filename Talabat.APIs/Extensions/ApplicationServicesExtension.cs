using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repsitories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Infrastructure;
using Talabat.Infrastructure._Identity;
using Talabat.Infrastructure.BasketRepository;
using Talabat.Infrastructure.GenericRepoistory;
using Talabat.Service.AuthService;
using Talabat.Service.OrderService;
using Talabat.Service.ProductService;

namespace Talabat.APIs.Extensions
{
    public static class ApplicationServicesExtension
	{
		public static IServiceCollection AddApplicationsService(this IServiceCollection services)
		{
			services.AddScoped(typeof(IOrderService),typeof(OrderService));
			services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
			services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddAutoMapper(typeof(MappingProfiles));
			services.AddScoped(typeof(IProductService), typeof(ProductService));
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
		public static IServiceCollection AddAuthServices(this IServiceCollection services ,IConfiguration configuration)
		{
			services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationIdentityDbContext>();
			services.AddScoped(typeof(IAuthService), typeof(AuthService));
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(/*JwtBearerDefaults.AuthenticationScheme,*/ options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidIssuer = configuration["JWT:ValidIssuer"],
					ValidateAudience = true,
					ValidAudience = configuration["JWT:ValidAuidence"],
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:AuthKey"] ?? string.Empty)),
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero
				};
			});
			return services;
		}
	}
}
