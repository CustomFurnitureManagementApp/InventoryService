using InventoryService.Domain.Entities.Transactional;
using InventoryService.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace InventoryService.Domain.Entities.Master
{
    public class Material : BaseEntity
    {
        public string? Code { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        public MaterialType MaterialType { get; set; }

        [MaxLength(50)]
        public string? Specification { get; set; }

        public int? LengthMm { get; set; }
        public int? WidthMm { get; set; }

        public UnitOfMeasure UnitOfMeasure { get; set; } = UnitOfMeasure.Piece;

        public List<MaterialStock>? MaterialStocks { get; set; }
        public List<MaterialPrice>? MaterialPrices { get; set; }
        public List<StockTransaction>? StockTransactions { get; set; }
    }
}
