using InventoryService.Domain.Entities.Transactional;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryService.Infrastructure.Data.Configurations.Transactional
{
    public class MaterialPricesConfiguration : IEntityTypeConfiguration<MaterialPrice>
    {
        public void Configure(EntityTypeBuilder<MaterialPrice> b)
        {
            b.ToTable("T_MaterialPrices");

            b.HasKey(x => x.Id);

            b.Property(x => x.PriceWithoutVAT)
                .HasColumnType("decimal(18,4)");

            b.Property(x => x.PriceWithVAT)
                .HasColumnType("decimal(18,4)");

            b.Property(x => x.InvoiceNumber)
                .HasMaxLength(50);
        }
    }
}
