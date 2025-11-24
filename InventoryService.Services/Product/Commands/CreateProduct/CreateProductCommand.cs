using InventoryService.Domain.DTOs;
using MediatR;

namespace InventoryService.Services.Product.Commands.CreateProduct
{
	public sealed record CreateProductCommand(string SKU, string Name, string? Description, decimal Price, int Quantity) : IRequest<ProductDto>;
}
