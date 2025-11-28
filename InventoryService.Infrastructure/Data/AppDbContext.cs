using InventoryService.Domain.Entities.Master;
using InventoryService.Domain.Entities.Transactional;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InventoryService.Infrastructure.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Material> Materials => Set<Material>();
        public DbSet<Warehouse> Warehouses => Set<Warehouse>();
        public DbSet<MaterialStock> MaterialStocks => Set<MaterialStock>();
        public DbSet<MaterialPrice> MaterialPrices => Set<MaterialPrice>();
        public DbSet<StockTransaction> StockTransactions => Set<StockTransaction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}