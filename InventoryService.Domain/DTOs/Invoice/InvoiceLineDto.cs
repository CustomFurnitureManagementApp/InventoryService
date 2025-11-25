namespace InventoryService.Domain.DTOs.Invoice
{
    public class InvoiceLineDto
    {
        public int Id { get; set; }
        public string? ItemName { get; set; }
        public string? ItemCode { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal LineTotal { get; set; }
        public int InvoiceId { get; set; }
        public InvoiceDto? Invoice { get; set; }
    }
}
