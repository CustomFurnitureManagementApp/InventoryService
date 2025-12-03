using InventoryService.Application.Common;
using MediatR;

namespace InventoryService.Application.Features.Materials.Queries.GetMaterialById
{
    public sealed record GetMaterialByIdQuery(int Id) : IRequest<Result<GetMaterialByIdResponse>>;
}
