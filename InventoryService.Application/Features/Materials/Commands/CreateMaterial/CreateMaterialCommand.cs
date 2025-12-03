using InventoryService.Application.Common;
using InventoryService.Domain.Enums;
using MediatR;

namespace InventoryService.Application.Features.Materials.Commands.CreateMaterial
{
    public class CreateMaterialCommand : IRequest<Result<CreateMaterialResponse>>
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public MaterialType MaterialType { get; set; }
        public string? Specification { get; set; }
        public int? LengthMm { get; set; }
        public int? WidthMm { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; } = UnitOfMeasure.Piece;
    }
}
