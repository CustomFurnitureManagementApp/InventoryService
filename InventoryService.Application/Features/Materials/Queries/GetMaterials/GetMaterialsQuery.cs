using MediatR;

namespace InventoryService.Application.Features.Material.Queries.GetMaterials
{
    public sealed record GetMaterialsQuery() : IRequest<IReadOnlyList<GetMaterialsResponse>>;
}
