using InventoryService.Application.Common;
using MediatR;

namespace InventoryService.Application.Features.Materials.Queries.GetMaterials
{
    public sealed record GetMaterialsQuery() : IRequest<Result<IReadOnlyList<GetMaterialsResponse>>>;
}
