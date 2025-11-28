using InventoryService.Repositories;

namespace InventoryService.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IMaterialRepository Materials { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
