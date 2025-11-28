using InventoryService.Domain.Entities.Master;

namespace InventoryService.Repositories
{
    public interface IMaterialRepository
    {
        Task<Material?> GetByIdAsync(int id);
        Task<IReadOnlyList<Material>> ListAsync();
        Task AddAsync(Material material);
        void Update(Material material);
        void Remove(Material material);
        Task<int> SaveChangesAsync();
    }
}
