using InventoryService.Application.Features.Products.Commands.CreateProduct;
using MediatR;

namespace InventoryService.Application.Features.Products.Queries.GetProductById
{
	public sealed record GetProductByIdQuery(int Id) : IRequest<GetProductByIdResponse>;
}
