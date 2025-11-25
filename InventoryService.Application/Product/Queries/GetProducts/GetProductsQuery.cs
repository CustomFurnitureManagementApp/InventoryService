using InventoryService.Domain.DTOs.Product;
using MediatR;

namespace InventoryService.Application.Product.Queries.GetProducts
{
	public sealed record GetProductsQuery() : IRequest<IReadOnlyList<ProductDto>>;
}
