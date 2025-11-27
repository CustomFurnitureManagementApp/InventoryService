using InventoryService.Repositories.Product;
using MediatR;

namespace InventoryService.Application.Features.Products.Queries.GetProducts
{
	public class GetProductsQueryHandler(IProductRepository repo) : IRequestHandler<GetProductsQuery, IReadOnlyList<GetProductsResponse>>
	{
		private readonly IProductRepository _repo = repo;

        public async Task<IReadOnlyList<GetProductsResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
		{
			var entities = await _repo.ListAsync();
			var dtos = entities.Select(e => new GetProductsResponse
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
