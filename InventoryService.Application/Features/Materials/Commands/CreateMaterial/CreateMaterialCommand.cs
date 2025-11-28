using InventoryService.Domain.Enums;
using MediatR;

namespace InventoryService.Application.Features.Material.Commands.CreateMaterial
{
    public class CreateMaterialCommand : IRequest<CreateMaterialResponse>
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
