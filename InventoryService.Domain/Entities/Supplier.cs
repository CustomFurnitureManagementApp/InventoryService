using System.ComponentModel.DataAnnotations;

namespace InventoryService.Domain.Entities
{
	public class Supplier
	{
		public int Id { get; set; }

		[Required, MaxLength(200)]
		public string Name { get; set; } = null!;

		[MaxLength(200)]
		public string? ContactName { get; set; }

		[MaxLength(200)]
		public string? Email { get; set; }

		[MaxLength(50)]
		public string? Phone { get; set; }

		[MaxLength(500)]
		public string? Address { get; set; }

		public ICollection<Material> Materials { get; set; } = new List<Material>();
	}
}