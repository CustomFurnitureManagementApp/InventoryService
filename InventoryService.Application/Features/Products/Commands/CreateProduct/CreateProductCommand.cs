using MediatR;

namespace InventoryService.Application.Features.Products.Commands.CreateProduct
{
	public sealed record CreateProductCommand(CreateProductRequest createProductRequest) : IRequest<CreateProductResponse>;
}
