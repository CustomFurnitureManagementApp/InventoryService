namespace InventoryService.Application.Features.Products.Queries.GetProducts
{
    public sealed class GetProductsResponse
    {
        public int Id { get; init; }
        public string SKU { get; init; } = null!;
        public string Name { get; init; } = null!;
        public string? Description { get; init; }
        public decimal Price { get; init; }
        public int Quantity { get; init; }
        public bool IsActive { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime LastUpdated { get; init; }
    }
}
