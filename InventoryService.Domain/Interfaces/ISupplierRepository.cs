using InventoryService.Domain.Entities;

namespace InventoryService.Repositories
{
    public interface ISupplierRepository
    {
        Task<Supplier?> GetByNameAsync(string name);
        Task AddAsync(Supplier supplier);
    }
}
