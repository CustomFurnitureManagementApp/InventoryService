using InventoryService.Domain.Entities;

namespace InventoryService.Repositories
{
    public interface IInvoiceRepository
    {
        Task AddInvoiceAsync(Invoice invoice);
        Task UpdateInvoiceAsync(Invoice invoice);
    }
}
