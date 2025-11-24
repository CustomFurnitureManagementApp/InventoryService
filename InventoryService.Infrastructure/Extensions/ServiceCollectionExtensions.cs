using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryService.Infrastructure.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection")
				?? configuration["ConnectionStrings:DefaultConnection"];

			if (string.IsNullOrWhiteSpace(connectionString))
				throw new InvalidOperationException("Connection string 'DefaultConnection' not found. Add it to appsettings.json or user secrets.");

			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(connectionString));

			// Register other infrastructure services (repositories, etc.) here as needed.
			return services;
		}
	}
}