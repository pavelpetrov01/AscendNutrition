using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AscendNutrition.Data.Models.Enums.Order;
using static AscendNutrition.Common.EntityValidationConstants.Order;

namespace AscendNutrition.Data.Models
{
    public class Order
    {
        [Key]
        [Comment("Unique identifier for the order")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Comment("Identifier of the customer who made the order")]
        public Guid CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public ApplicationUser Customer { get; set; } = null!;

        [Required]
        [Comment("The day in which the order was made")]
        public DateTime OrderDate { get; set; }

        [Required]
        [Range(PriceMinValue, PriceMaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        [Comment("The total price of the order")]
        public decimal TotalPrice { get; set; }

        [Required]
        [Comment("Shows the status of the order")]
        public OrderStatus OrderStatus { get; set; }

        [Required]
        [Comment("Shows the way in which the order will be paid")]
        public PaymentMethod PaymentMethod { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
