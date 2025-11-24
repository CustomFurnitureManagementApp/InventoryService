using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryService.Domain.Entities
{
	public class ProductVariant
	{
		public int Id { get; set; }

		public int ProductId { get; set; }
		public Product Product { get; set; } = null!;

		[Required, MaxLength(100)]
		public string Name { get; set; } = null!; // e.g., "White Lacquer", "Oak Veneer"

		[MaxLength(50)]
		public string? SKU { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal AdditionalCost { get; set; }

		public ICollection<StockItem> StockItems { get; set; } = new List<StockItem>();
	}
}
