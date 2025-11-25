using InventoryService.Domain.DTOs.Product;
using InventoryService.Repositories.Product;
using MediatR;

namespace InventoryService.Application.Product.Queries.GetProductById
{
	public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
	{
		private readonly IProductRepository _repo;

		public GetProductByIdQueryHandler(IProductRepository repo) => _repo = repo;

		public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
		{
			var e = await _repo.GetByIdAsync(request.Id);
			if (e is null) return null;

			return new ProductDto
			{
				Id = e.Id,
				SKU = e.SKU,
				Name = e.Name,
				Description = e.Description,
				Price = e.Price,
				Quantity = e.Quantity,
				IsActive = e.IsActive,
				CreatedAt = e.CreatedAt,
				LastUpdated = e.LastUpdated
			};
		}
	}
}
