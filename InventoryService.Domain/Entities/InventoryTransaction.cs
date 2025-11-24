using InventoryService.Domain.Enums;

namespace InventoryService.Domain.Entities
{
	public class InventoryTransaction
	{
		public int Id { get; set; }

		public int? StockItemId { get; set; }
		public StockItem? StockItem { get; set; }

		public decimal Quantity { get; set; }

		public TransactionType TransactionType { get; set; }

		public string? Reference { get; set; } // e.g., production order, purchase invoice

		public string? CreatedBy { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
