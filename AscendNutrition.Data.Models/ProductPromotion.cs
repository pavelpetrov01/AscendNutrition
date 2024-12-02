using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AscendNutrition.Data.Models
{
    public class ProductPromotion
    {
        public Guid ProductId { get; set; }
        [ForeignKey(nameof(ProductId))] 
        public Product Product { get; set; } = null!;

        public Guid PromotionId { get; set; }
        [ForeignKey(nameof(PromotionId))]
        public Promotion Promotion { get; set; } = null!;
    }
}
