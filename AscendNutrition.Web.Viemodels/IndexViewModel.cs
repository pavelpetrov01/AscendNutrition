using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AscendNutrition.Common.EntityValidationConstants.Product;
namespace AscendNutrition.Web.ViewModels
{
    public class IndexViewModel
    {
        [Required]
        [StringLength(ProductNameMaxLength),MinLength(ProductNameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [Range(PriceMinValue, PriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int Servings { get; set; }

        public string? ImageUrl { get; set; }

    }
}
