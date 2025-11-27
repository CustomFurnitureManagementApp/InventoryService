using InventoryService.Repositories.Product;
using MediatR;

namespace InventoryService.Application.Features.Products.Queries.GetProductById
{
	public class GetProductByIdQueryHandler(IProductRepository repo) : IRequestHandler<GetProductByIdQuery, GetProductByIdResponse>
	{
		private readonly IProductRepository _repo = repo;

        public async Task<GetProductByIdResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
		{
			var e = await _repo.GetByIdAsync(request.Id);
			if (e is null) return new GetProductByIdResponse();

			return new GetProductByIdResponse
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
