using InventoryService.Domain.Enums;

namespace InventoryService.Application.Features.Materials.Commands.CreateMaterial
{
    public sealed class CreateMaterialResponse
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public MaterialType MaterialType { get; set; }
        public string? Specification { get; set; }
        public int? LengthMm { get; set; }
        public int? WidthMm { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
    }
}
