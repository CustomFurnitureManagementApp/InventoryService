using InventoryService.Domain.Entities.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryService.Infrastructure.Data.Configurations.Master
{
    public class WarehousesConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> b)
        {
            b.ToTable("M_Warehouses");

            b.HasKey(x => x.Id);

            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            b.Property(x => x.Description)
                .HasMaxLength(250);

            b.Property(x => x.LocationType)
                .HasConversion<int>();

            // Relationships
            b.HasMany(x => x.MaterialStocks)
                .WithOne(ms => ms.Warehouse)
                .HasForeignKey(ms => ms.WarehouseId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.StockTransactions)
                .WithOne(ms => ms.Warehouse)
                .HasForeignKey(ms => ms.WarehouseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
