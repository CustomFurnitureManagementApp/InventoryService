using InventoryService.Domain.Entities.Transactional;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryService.Infrastructure.Data.Configurations.Transactional
{
    public class MaterialStocksConfiguration : IEntityTypeConfiguration<MaterialStock>
    {
        public void Configure(EntityTypeBuilder<MaterialStock> b)
        {
            b.ToTable("T_MaterialStocks");

            b.HasKey(x => x.Id);

            b.Property(x => x.QuantityOnHand)
                .HasColumnType("decimal(18,4)");

            b.Property(x => x.QuantityReserved)
                .HasColumnType("decimal(18,4)");

            // Foreign keys are configured in Material and Warehouse configs
        }
    }
}
