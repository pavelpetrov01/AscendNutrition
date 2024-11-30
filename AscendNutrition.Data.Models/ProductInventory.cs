using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AscendNutrition.Data.Models
{
    public class ProductInventory
    {
        public Guid ProductId { get; set; }

        [ForeignKey(nameof(ProductId))] 
        public Product Product { get; set; } = null!;

        public Guid InventoryId { get; set; }

        [ForeignKey(nameof(InventoryId))] 
        public Inventory Inventory { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }
    }
}
