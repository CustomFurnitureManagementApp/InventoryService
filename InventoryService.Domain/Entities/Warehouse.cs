using System.ComponentModel.DataAnnotations;

namespace InventoryService.Domain.Entities
{
	public class Warehouse
	{
		public int Id { get; set; }

		[Required, MaxLength(150)]
		public string Name { get; set; } = null!;

		[MaxLength(500)]
		public string? Address { get; set; }

		public ICollection<StockItem> StockItems { get; set; } = new List<StockItem>();
	}
}