using InventoryService.Domain.DTOs;
using InventoryService.Repositories.Product;
using MediatR;

namespace InventoryService.Application.Product.Commands.CreateProduct
{
	public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
	{
		private readonly IProductRepository _repo;

		public CreateProductCommandHandler(IProductRepository repo) => _repo = repo;

		public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
		{
			var entity = new Domain.Entities.Product
			{
				SKU = request.SKU,
				Name = request.Name,
				Description = request.Description,
				Price = request.Price,
				Quantity = request.Quantity
			};

			await _repo.AddAsync(entity);
			await _repo.SaveChangesAsync();

			return new ProductDto
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
