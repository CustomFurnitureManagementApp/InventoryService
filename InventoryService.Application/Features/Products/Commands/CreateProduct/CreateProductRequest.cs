namespace InventoryService.Application.Features.Products.Commands.CreateProduct
{
    public sealed class CreateProductRequest
    {
        public string SKU { get; init; } = null!;
        public string Name { get; init; } = null!;
        public string? Description { get; init; }
        public decimal Price { get; init; }
        public int Quantity { get; init; }
    }
}
