using InventoryService.Domain.Entities.Master;
using InventoryService.Domain.Entities.Transactional;
using InventoryService.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryService.Infrastructure.Data.DataSeeder
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var provider = scope.ServiceProvider;

            var context = provider.GetRequiredService<AppDbContext>();

            // --- Check if database already has data ---
            bool hasData = await context.Materials.AnyAsync()
                            || await context.Warehouses.AnyAsync()
                            || await context.MaterialStocks.AnyAsync()
                            || await context.MaterialPrices.AnyAsync()
                            || await context.StockTransactions.AnyAsync();

            if (hasData)
                return; // Data already exists, do not seed again

            try
            {
                // --- MATERIALS ---
                var materials = new List<Material>
            {
                new Material { Name = "White MDF 18mm", Code = "MDF18W", UnitOfMeasure = UnitOfMeasure.Piece },
                new Material { Name = "Oak Pal 18mm", Code = "PAL18OAK", UnitOfMeasure = UnitOfMeasure.Piece },
                new Material { Name = "Edge Band 0.8mm", Code = "EB08", UnitOfMeasure = UnitOfMeasure.Piece },
            };
                await context.Materials.AddRangeAsync(materials);
                await context.SaveChangesAsync();

                // --- WAREHOUSES ---
                var warehouses = new List<Warehouse>
            {
                new Warehouse { Name = "Main Warehouse", Description = "Factory" },
                new Warehouse { Name = "Secondary Warehouse", Description = "Showroom" }
            };
                await context.Warehouses.AddRangeAsync(warehouses);
                await context.SaveChangesAsync();

                // Fetch IDs for relations
                var material1 = await context.Materials.FirstAsync();
                var warehouse1 = await context.Warehouses.FirstAsync();

                // --- MATERIAL PRICES ---
                var prices = new List<MaterialPrice>
            {
                new MaterialPrice
                {
                    MaterialId = material1.Id,
                    PriceWithoutVAT = 100,
                    PriceWithVAT = 119,
                    InvoiceNumber = "INV-001"
                }
            };
                await context.MaterialPrices.AddRangeAsync(prices);
                await context.SaveChangesAsync();

                // --- MATERIAL STOCK ---
                var stocks = new List<MaterialStock>
            {
                new MaterialStock
                {
                    MaterialId = material1.Id,
                    WarehouseId = warehouse1.Id,
                    QuantityOnHand = 50,
                    QuantityReserved = 5
                }
            };
                await context.MaterialStocks.AddRangeAsync(stocks);
                await context.SaveChangesAsync();

                // --- STOCK TRANSACTIONS ---
                var transaction = new StockTransaction
                {
                    MaterialId = material1.Id,
                    WarehouseId = warehouse1.Id,
                    TransactionType = StockTransactionType.PurchaseIn,
                    Quantity = 50,
                    UnitPrice = 100,
                    TotalValue = 5000,
                    ReferenceDocument = "PO-001",
                    PerformedBy = "system"
                };
                await context.StockTransactions.AddAsync(transaction);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while seeding the Inventory database.", ex);
            }
        }
    }
}
