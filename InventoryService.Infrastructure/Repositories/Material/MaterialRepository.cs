using InventoryService.Domain.Entities.Master;
using InventoryService.Infrastructure.Data;
using InventoryService.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly AppDbContext _db;
        public MaterialRepository(AppDbContext db) => _db = db;

        public async Task<Material?> GetByIdAsync(int id)
        {
            return await _db.Materials
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Material>> ListAsync() =>
            await _db.Materials
                .ToListAsync();

        public async Task AddAsync(Material material)
        {
            await _db.Materials.AddAsync(material);
        }

        public void Update(Material material)
        {
            _db.Materials.Update(material);
        }

        public void Remove(Material material)
        {
            _db.Materials.Remove(material);
        }

        public async Task<int> SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}
