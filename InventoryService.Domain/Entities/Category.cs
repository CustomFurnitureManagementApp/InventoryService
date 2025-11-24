using System.ComponentModel.DataAnnotations;

namespace InventoryService.Domain.Entities
{
	public class Category
	{
		public int Id { get; set; }

		[Required, MaxLength(100)]
		public string Name { get; set; } = null!;

		public int? ParentCategoryId { get; set; }
		public Category? ParentCategory { get; set; }

		public ICollection<Category> Children { get; set; } = new List<Category>();
		public ICollection<Product> Products { get; set; } = new List<Product>();
	}
}