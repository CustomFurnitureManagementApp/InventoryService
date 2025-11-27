using MediatR;

namespace InventoryService.Application.Features.Products.Queries.GetProducts
{
	public sealed record GetProductsQuery() : IRequest<IReadOnlyList<GetProductsResponse>>;
}
