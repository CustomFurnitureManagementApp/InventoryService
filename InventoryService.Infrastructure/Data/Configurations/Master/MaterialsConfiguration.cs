using InventoryService.Domain.Entities.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryService.Infrastructure.Data.Configurations.Master
{
    public class MaterialsConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> b)
        {
            b.ToTable("M_Materials");

            b.HasKey(x => x.Id);

            b.Property(x => x.Code)
                .HasMaxLength(50);

            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            b.Property(x => x.Specification)
                .HasMaxLength(50);

            b.Property(x => x.MaterialType)
                .HasConversion<int>();

            b.Property(x => x.UnitOfMeasure)
                .HasConversion<int>();

            // Relationships
            b.HasMany(x => x.MaterialStocks)
                .WithOne(ms => ms.Material)
                .HasForeignKey(ms => ms.MaterialId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.MaterialPrices)
                .WithOne(mp => mp.Material)
                .HasForeignKey(mp => mp.MaterialId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.StockTransactions)
                .WithOne(st => st.Material)
                .HasForeignKey(st => st.MaterialId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
