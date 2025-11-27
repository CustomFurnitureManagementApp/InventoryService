namespace InventoryService.Repositories.Product
{
    public interface IProductRepository
	{
		Task<Domain.Entities.Product?> GetByIdAsync(int id);
		Task<IReadOnlyList<Domain.Entities.Product>> ListAsync();
		Task AddAsync(Domain.Entities.Product product);
		void Update(Domain.Entities.Product product);
		void Remove(Domain.Entities.Product product);
		Task<int> SaveChangesAsync();
	}
}
