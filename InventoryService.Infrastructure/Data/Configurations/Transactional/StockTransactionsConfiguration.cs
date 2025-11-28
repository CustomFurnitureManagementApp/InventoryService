using InventoryService.Domain.Entities.Transactional;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryService.Infrastructure.Data.Configurations.Transactional
{
    public class StockTransactionsConfiguration : IEntityTypeConfiguration<StockTransaction>
    {
        public void Configure(EntityTypeBuilder<StockTransaction> b)
        {
            b.ToTable("T_StockTransactions");

            b.HasKey(x => x.Id);

            b.Property(x => x.TransactionType)
                .HasConversion<int>();

            b.Property(x => x.Quantity)
                .HasColumnType("decimal(18,4)");

            b.Property(x => x.UnitPrice)
                .HasColumnType("decimal(18,4)");

            b.Property(x => x.TotalValue)
                .HasColumnType("decimal(18,4)");

            b.Property(x => x.ReferenceDocument)
                .HasMaxLength(100);

            b.Property(x => x.PerformedBy)
                .HasMaxLength(100);

            b.Property(x => x.Timestamp)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
