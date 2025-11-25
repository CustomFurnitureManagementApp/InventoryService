namespace InventoryService.Services.Invoice
{
    public interface IInvoiceService
    {
        Task<bool> ImportInvoiceAsync(Stream xmlStream);
    }
}
