using InventoryService.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Repositories.Product
{
	public class ProductRepository : IProductRepository
	{
		private readonly AppDbContext _db;
		public ProductRepository(AppDbContext db) => _db = db;

		public async Task<Domain.Entities.Product?> GetByIdAsync(int id) =>
			await _db.Products
				.Include(p => p.Variants)
				.Include(p => p.Category)
				.FirstOrDefaultAsync(p => p.Id == id);

		public async Task<IReadOnlyList<Domain.Entities.Product>> ListAsync() =>
			await _db.Products
				.Include(p => p.Variants)
				.Include(p => p.Category)
				.ToListAsync();

		public async Task AddAsync(Domain.Entities.Product product)
		{
			await _db.Products.AddAsync(product);
		}

		public void Update(Domain.Entities.Product product)
		{
			_db.Products.Update(product);
		}

		public void Remove(Domain.Entities.Product product)
		{
			_db.Products.Remove(product);
		}

		public async Task<int> SaveChangesAsync() => await _db.SaveChangesAsync();
	}
}
