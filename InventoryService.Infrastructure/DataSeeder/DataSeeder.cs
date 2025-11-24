using InventoryService.Domain.Entities;
using InventoryService.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryService.Infrastructure.Persistence
{
	public static class DataSeeder
	{
		public static async Task SeedAsync(IServiceProvider serviceProvider)
		{
			using var scope = serviceProvider.CreateScope();
			var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

			// ensure database is created (migrations will be applied later)
			await db.Database.MigrateAsync();

			if (await db.Categories.AnyAsync()) return; // simple guard to avoid reseed

			// Categories
			var kitchen = new Category { Name = "Kitchen" };
			var living = new Category { Name = "Living Room" };
			var bathroom = new Category { Name = "Bathroom" };
			db.Categories.AddRange(kitchen, living, bathroom);

			// Suppliers
			var supplier1 = new Supplier { Name = "Main Supplies Co.", ContactName = "Maria", Email = "maria@supplies.local" };
			db.Suppliers.Add(supplier1);

			// Materials
			var pal = new Material { Name = "PAL Board 18mm", MaterialType = MaterialType.PAL, Specification = "18mm raw", UnitCost = 15.00m, UnitOfMeasure = UnitOfMeasure.SquareMeter, Supplier = supplier1 };
			var mdf = new Material { Name = "MDF 18mm", MaterialType = MaterialType.MDF, Specification = "18mm raw", UnitCost = 12.50m, UnitOfMeasure = UnitOfMeasure.SquareMeter, Supplier = supplier1 };
			db.Materials.AddRange(pal, mdf);

			// Warehouse
			var mainWh = new Warehouse { Name = "Main Warehouse", Address = "Factory - Zone A" };
			db.Warehouses.Add(mainWh);

			await db.SaveChangesAsync();

			// Example product
			var baseProduct = new Product
			{
				SKU = "KIT-BASE-001",
				Name = "Base Kitchen Cabinet (600mm)",
				Description = "Standard base cabinet used in kitchens.",
				Price = 250.00m,
				Cost = 120.00m,
				Quantity = 10,
				Category = kitchen,
				IsCustom = true,
				IsActive = true
			};
			db.Products.Add(baseProduct);
			await db.SaveChangesAsync();

			// Variant
			var variant = new ProductVariant
			{
				ProductId = baseProduct.Id,
				Name = "White Lacquer Finish",
				SKU = "KIT-BASE-001-WH",
				AdditionalCost = 25.00m
			};
			db.ProductVariants.Add(variant);
			await db.SaveChangesAsync();

			// ProductMaterial linking product -> materials
			db.ProductMaterials.Add(new ProductMaterial
			{
				ProductId = baseProduct.Id,
				MaterialId = pal.Id,
				Quantity = 1.5m,
				UnitOfMeasure = UnitOfMeasure.SquareMeter,
				WasteFactor = 0.10m
			});
			db.ProductMaterials.Add(new ProductMaterial
			{
				ProductId = baseProduct.Id,
				MaterialId = mdf.Id,
				Quantity = 0.5m,
				UnitOfMeasure = UnitOfMeasure.SquareMeter,
				WasteFactor = 0.08m
			});

			// Initial stock item
			db.StockItems.Add(new StockItem
			{
				ProductVariantId = variant.Id,
				WarehouseId = mainWh.Id,
				Quantity = 10m,
				ReservedQuantity = 0m,
				AverageCost = 120.00m
			});

			await db.SaveChangesAsync();
		}
	}
}