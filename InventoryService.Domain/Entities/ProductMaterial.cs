using InventoryService.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryService.Domain.Entities
{
	// Join entity: how much of a material is needed for one unit of product
	public class ProductMaterial
	{
		public int Id { get; set; }

		public int ProductId { get; set; }
		public Product Product { get; set; } = null!;

		public int MaterialId { get; set; }
		public Material Material { get; set; } = null!;

		public decimal Quantity { get; set; } // e.g., square meters, pieces, meters
		public UnitOfMeasure UnitOfMeasure { get; set; } = UnitOfMeasure.Piece;

		[Column(TypeName = "decimal(18,4)")]
		public decimal? WasteFactor { get; set; } // percent as decimal e.g. 0.10 = 10%
	}
}
