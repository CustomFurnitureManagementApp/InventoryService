using InventoryService.Infrastructure.Data;
using InventoryService.Repositories;
using InventoryService.Repositories.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryService.Infrastructure.DependencyInjection
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

			services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IInvoiceRepository, InvoiceRepository>();
            return services;
		}
	}
}