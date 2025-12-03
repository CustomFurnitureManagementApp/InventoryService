using InventoryService.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace InventoryService.Application.Features.Materials.Commands.CreateMaterial
{
    public sealed class CreateMaterialRequest
    {
        [Required]
        [MaxLength(50)]
        public string Code { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        public MaterialType MaterialType { get; set; }

        [MaxLength(50)]
        public string? Specification { get; set; }

        public int? LengthMm { get; set; }
        public int? WidthMm { get; set; }

        [Required]
        public UnitOfMeasure UnitOfMeasure { get; set; } = UnitOfMeasure.Piece;
    }
}
