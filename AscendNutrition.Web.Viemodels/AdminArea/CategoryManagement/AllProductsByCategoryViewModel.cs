using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AscendNutrition.Common.ModelValidationMessages.Product;
using static AscendNutrition.Common.EntityValidationConstants.Category;
using static AscendNutrition.Common.EntityValidationConstants.Product;
namespace AscendNutrition.Web.ViewModels.AdminArea.CategoryManagement
{
    public class AllProductsByCategoryViewModel
    {
        [Required(ErrorMessage = IdRequired)]

        public Guid Id { get; set; }

        [Required(ErrorMessage = ProductNameRequired)]
        [StringLength(ProductNameMaxLength), MinLength(ProductNameMinLength)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = BrandNameRequired)]
        [StringLength(BrandMaxLength), MinLength(BrandMinLength)]
        public string Brand { get; set; } = null!;

        [Required(ErrorMessage = PriceRequired)]
        [Range(PriceMinValue, PriceMaxValue)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = ServingsRequired)]
        public int Servings { get; set; }

        [StringLength(ImageUrlMaxLength), MinLength(ImageUrlMinLength)]
        public string? ImageUrl { get; set; }

        [StringLength(CategoryNameMaxLength), MinLength(CategoryNameMinLength)]
        public string? Category { get; set; }


        [Required(ErrorMessage = SuitabilityRequired)]
        public string Suitability { get; set; } = null!;

        public string? Size { get; set; }

        [Required(ErrorMessage = QuantityRequired)]
        public int Quantity { get; set; }

        public string? Flavor { get; set; }
    }
}
