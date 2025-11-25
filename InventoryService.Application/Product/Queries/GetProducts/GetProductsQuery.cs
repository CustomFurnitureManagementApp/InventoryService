using InventoryService.Domain.DTOs;
using MediatR;

namespace InventoryService.Application.Product.Queries.GetProducts
{
	public sealed record GetProductsQuery() : IRequest<IReadOnlyList<ProductDto>>;
}
