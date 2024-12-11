using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static AscendNutrition.Common.EntityValidationConstants.Inventory;

namespace AscendNutrition.Data.Models
{
    public class Inventory
    {
        [Key]
        [Comment("Unique identifier for the inventory")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(CityMaxLength)]
        [Comment("The city in which the inventory is")]
        public string City { get; set; } = null!;

        [Comment("Flag which is used for soft deletion")]
        public bool IsDeleted { get; set; } = false;

        public ICollection<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();
    }
}
