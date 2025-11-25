namespace InventoryService.Domain.DTOs.Invoice
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public string? InvoiceNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public string? InvoiceTypeCode { get; set; }
        public string? Currency { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<InvoiceLineDto> Lines { get; set; } = new List<InvoiceLineDto>();
    }
}
