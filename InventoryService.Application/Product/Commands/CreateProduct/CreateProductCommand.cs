using InventoryService.Domain.DTOs.Product;
using MediatR;

namespace InventoryService.Application.Product.Commands.CreateProduct
{
	public sealed record CreateProductCommand(string SKU, string Name, string? Description, decimal Price, int Quantity) : IRequest<ProductDto>;
}
