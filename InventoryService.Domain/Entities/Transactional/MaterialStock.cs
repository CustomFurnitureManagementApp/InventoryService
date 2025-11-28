using InventoryService.Domain.Entities.Master;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryService.Domain.Entities.Transactional
{
    public class MaterialStock
    {
        public int Id { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; } = null!;

        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; } = null!;

        [Column(TypeName = "decimal(18,4)")]
        public decimal QuantityOnHand { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal QuantityReserved { get; set; }
    }
}
