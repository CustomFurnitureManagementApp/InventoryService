using InventoryService.Repositories.Product;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryService.Repositories.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IProductRepository, ProductRepository>();

			return services;
		}
	}
}
