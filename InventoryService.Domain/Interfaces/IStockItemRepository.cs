using InventoryService.Domain.Entities;

namespace InventoryService.Repositories
{
    public interface IStockItemRepository
    {
        Task<StockItem?> GetByMaterialIdAsync(int materialId, int warehouseId);
        Task AddAsync(StockItem stockItem);
        Task UpdateAsync(StockItem stockItem);
    }
}
