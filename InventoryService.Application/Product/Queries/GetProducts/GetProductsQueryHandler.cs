using InventoryService.Domain.DTOs.Product;
using InventoryService.Repositories.Product;
using MediatR;

namespace InventoryService.Application.Product.Queries.GetProducts
{
	public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IReadOnlyList<ProductDto>>
	{
		private readonly IProductRepository _repo;

		public GetProductsQueryHandler(IProductRepository repo) => _repo = repo;

		public async Task<IReadOnlyList<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
		{
			var entities = await _repo.ListAsync();
			var dtos = entities.Select(e => new ProductDto
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
			}).ToList();

			return dtos;
		}
	}
}
