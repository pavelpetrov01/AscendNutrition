using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static AscendNutrition.Common.EntityValidationConstants.OrderItem;

namespace AscendNutrition.Data.Models
{
    public class OrderItem
    {
        public Guid OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; } = null!;

        public Guid ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

        [Required]
        [Comment("The quantity of the product")]
        public int Quantity { get; set; }

        [Required]
        [Range(PriceMinValue, PriceMaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        [Comment("Price per single unit")]
        public decimal Price { get; set; }

    }
}