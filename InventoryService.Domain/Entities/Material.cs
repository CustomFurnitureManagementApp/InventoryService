using InventoryService.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryService.Domain.Entities
{
	public class Material
	{
		public int Id { get; set; }

		[Required, MaxLength(100)]
		public string Name { get; set; } = null!;

		public MaterialType MaterialType { get; set; }

		[MaxLength(50)]
		public string? Specification { get; set; } // e.g., thickness, finish code

		[Column(TypeName = "decimal(18,4)")]
		public decimal UnitCost { get; set; } // cost per unit (as defined by UoM)

		public UnitOfMeasure UnitOfMeasure { get; set; } = UnitOfMeasure.Piece;

		public int? SupplierId { get; set; }
		public Supplier? Supplier { get; set; }

		public ICollection<ProductMaterial> ProductMaterials { get; set; } = new List<ProductMaterial>();
	}
}