using InventoryService.Domain.Enums;

namespace InventoryService.Application.Features.Material.Queries.GetMaterials
{
    public sealed class GetMaterialsResponse
    {
        public int Id { get; init; }
        public string Code { get; init; } = null!;
        public string? Name { get; init; }
        public MaterialType MaterialType { get; set; }
        public string? Specification { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; } = UnitOfMeasure.Piece;
        public DateTime CreatedAt { get; init; }
        public DateTime LastUpdated { get; init; }
    }
}
