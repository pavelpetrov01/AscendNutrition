using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static AscendNutrition.Common.EntityValidationConstants.Promotion;


namespace AscendNutrition.Data.Models
{
    public class Promotion
    {
        [Key]
        [Comment("Unique identifier for the promotion")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(PromotionNameMaxLength)]
        [Comment("The name of the promotion")]
        public string Name { get; set; } = null!;

        [Required]
        [Comment("The percentage that will be applied to the products")]
        public int DiscountPercentage { get; set; }

        [Required]
        [Comment("The beginning of the promotion")]
        public DateTime StartDate { get; set; }

        [Required]
        [Comment("The end of the promotion")]
        public DateTime EndDate { get; set; }

        public ICollection<Product> ApplicableProducts { get; set; } = new List<Product>();
    }
}
