using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryService.Domain.Entities
{
	// Stock per variant per warehouse
	public class StockItem
	{
		public int Id { get; set; }

		public int ProductVariantId { get; set; }
		public ProductVariant ProductVariant { get; set; } = null!;

		public int WarehouseId { get; set; }
		public Warehouse Warehouse { get; set; } = null!;

		public decimal Quantity { get; set; }
		public decimal ReservedQuantity { get; set; }

		[Column(TypeName = "decimal(18,4)")]
		public decimal? AverageCost { get; set; }

		public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
	}
}