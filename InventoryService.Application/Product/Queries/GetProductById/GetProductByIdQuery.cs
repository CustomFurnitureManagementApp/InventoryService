using InventoryService.Domain.DTOs;
using MediatR;

namespace InventoryService.Application.Product.Queries.GetProductById
{
	public sealed record GetProductByIdQuery(int Id) : IRequest<ProductDto?>;
}
