using InventoryService.Repositories;
using MediatR;

namespace InventoryService.Application.Features.Material.Queries.GetMaterials
{
    public class GetMaterialsQueryHandler(IMaterialRepository repo) : IRequestHandler<GetMaterialsQuery, IReadOnlyList<GetMaterialsResponse>>
    {
        private readonly IMaterialRepository _repo = repo;

        public async Task<IReadOnlyList<GetMaterialsResponse>> Handle(GetMaterialsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repo.ListAsync();
            var dtos = entities.Select(e => new GetMaterialsResponse
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                MaterialType = e.MaterialType,
                Specification = e.Specification,
                UnitOfMeasure = e.UnitOfMeasure
            }).ToList();

            return dtos;
        }
    }
}
