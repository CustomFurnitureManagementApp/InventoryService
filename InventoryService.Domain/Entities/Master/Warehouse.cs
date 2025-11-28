using InventoryService.Domain.Entities.Transactional;
using InventoryService.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace InventoryService.Domain.Entities.Master
{
    public class Warehouse
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(250)]
        public string? Description { get; set; }

        public WarehouseType LocationType { get; set; } = WarehouseType.Generic;

        public bool IsActive { get; set; } = true;

        public List<MaterialStock>? MaterialStocks { get; set; }
        public List<StockTransaction>? StockTransactions { get; set; }
    }
}
