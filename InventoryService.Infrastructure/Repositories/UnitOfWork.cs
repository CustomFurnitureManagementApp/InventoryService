using InventoryService.Domain.Interfaces;
using InventoryService.Infrastructure.Data;
using InventoryService.Repositories;

namespace InventoryService.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            Materials = new MaterialRepository(_db);
        }

        public IMaterialRepository Materials { get; }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _db.SaveChangesAsync(cancellationToken);
        }
    }

}
