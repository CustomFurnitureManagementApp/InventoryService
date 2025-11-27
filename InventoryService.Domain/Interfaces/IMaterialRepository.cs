using InventoryService.Domain.Entities;

namespace InventoryService.Repositories
{
    public interface IMaterialRepository
    {
        Task<Material?> GetByNameAndPriceAsync(string name, decimal price);
        Task AddAsync(Material material);
        Task UpdateAsync(Material material);
    }
}
