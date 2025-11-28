using InventoryService.Domain.Entities.Master;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryService.Domain.Entities.Transactional
{
    public class MaterialPrice
    {
        public int Id { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; } = null!;

        [Column(TypeName = "decimal(18,4)")]
        public decimal PriceWithoutVAT { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal PriceWithVAT { get; set; }

        [MaxLength(50)]
        public string? InvoiceNumber { get; set; }
    }
}
