using InventoryService.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryService.Domain.Entities
{
	// Bill of Materials item for a product. Can reference either a material or another product component.
	public class BomItem
	{
		public int Id { get; set; }

		public int ProductId { get; set; } // Parent product
		public Product Product { get; set; } = null!;

		public int? ComponentProductId { get; set; }
		public Product? ComponentProduct { get; set; }

		public int? ComponentMaterialId { get; set; }
		public Material? ComponentMaterial { get; set; }

		public decimal Quantity { get; set; }
		public UnitOfMeasure UnitOfMeasure { get; set; } = UnitOfMeasure.Piece;

		[Column(TypeName = "decimal(18,4)")]
		public decimal? ScrapFactor { get; set; } // percent as decimal
	}
}
