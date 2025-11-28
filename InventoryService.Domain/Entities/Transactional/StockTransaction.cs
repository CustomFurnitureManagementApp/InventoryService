using InventoryService.Domain.Entities.Master;
using InventoryService.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryService.Domain.Entities.Transactional
{
    public class StockTransaction : BaseEntity
    {
        [Required]
        public int MaterialId { get; set; }
        public Material Material { get; set; } = null!;

        [Required]
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; } = null!;

        [Required]
        public StockTransactionType TransactionType { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalValue { get; set; }

        [MaxLength(100)]
        public string? ReferenceDocument { get; set; }

        public int? ProductionOrderId { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [MaxLength(100)]
        public string? PerformedBy { get; set; }
    }
}
