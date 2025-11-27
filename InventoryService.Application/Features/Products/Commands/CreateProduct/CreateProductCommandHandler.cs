using InventoryService.Repositories.Product;
using MediatR;

namespace InventoryService.Application.Features.Products.Commands.CreateProduct
{
	public class CreateProductCommandHandler(IProductRepository repo) : IRequestHandler<CreateProductCommand, CreateProductResponse>
	{
		private readonly IProductRepository _repo = repo;

        public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
		{
			var entity = new Domain.Entities.Product
			{
				SKU = request.createProductRequest.SKU,
				Name = request.createProductRequest.Name,
				Description = request.createProductRequest.Description,
				Price = request.createProductRequest.Price,
				Quantity = request.createProductRequest.Quantity
			};

			await _repo.AddAsync(entity);
			await _repo.SaveChangesAsync();

			return new CreateProductResponse
			{
				Id = entity.Id,
				SKU = entity.SKU,
				Name = entity.Name,
				Description = entity.Description,
				Price = entity.Price,
				Quantity = entity.Quantity,
				IsActive = entity.IsActive,
				CreatedAt = entity.CreatedAt,
				LastUpdated = entity.LastUpdated
			};
		}
	}
}
