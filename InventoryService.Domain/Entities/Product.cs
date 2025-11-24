using InventoryService.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryService.Domain.Entities
{
	public class Product
	{
		public int Id { get; set; }

		[Required, MaxLength(50)]
		public string SKU { get; set; } = null!;

		[Required, MaxLength(200)]
		public string Name { get; set; } = null!;

		[MaxLength(1000)]
		public string? Description { get; set; }

		public ProductType Type { get; set; } = ProductType.Finished;

		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Cost { get; set; }

		public bool IsCustom { get; set; } = true;
		public bool IsActive { get; set; } = true;

		// Inventory control
		public int Quantity { get; set; }
		public int ReorderLevel { get; set; }

		// Physical dimensions (optional)
		public decimal? WeightKg { get; set; }
		public decimal? LengthCm { get; set; }
		public decimal? WidthCm { get; set; }
		public decimal? HeightCm { get; set; }

		// Category
		public int? CategoryId { get; set; }
		public Category? Category { get; set; }

		// Audit
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
		public string? UpdatedBy { get; set; }

		// Navigation
		public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
		public ICollection<ProductMaterial> Materials { get; set; } = new List<ProductMaterial>();
		public ICollection<BomItem> BomItems { get; set; } = new List<BomItem>();
	}
}