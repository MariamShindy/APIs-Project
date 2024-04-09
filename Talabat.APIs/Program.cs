
namespace Talabat.APIs
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var webApplicationBuilder = WebApplication.CreateBuilder(args);

			#region Configure services
			// Add services to the dependency injection container.

			//Register required web API's services to the dependency injection container.
			webApplicationBuilder.Services.AddControllers();
			
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			webApplicationBuilder.Services.AddEndpointsApiExplorer();
			webApplicationBuilder.Services.AddSwaggerGen(); 
			#endregion

			var app = webApplicationBuilder.Build();

			#region Configure kestrel middleware
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.MapControllers(); 
			#endregion

			app.Run();
		}
	}
}
