using InventoryService.Domain.Interfaces;
using MediatR;

namespace InventoryService.Application.Features.Material.Queries.GetMaterials
{
    public class GetMaterialsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetMaterialsQuery, IReadOnlyList<GetMaterialsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IReadOnlyList<GetMaterialsResponse>> Handle(GetMaterialsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.Materials.ListAsync();
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
