using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AscendNutrition.Common.EntityValidationConstants.Product;
using static AscendNutrition.Common.ModelValidationMessages.Product;

namespace AscendNutrition.Web.ViewModels.Product
{
    public class ProductDetailsViewModel
    {
        [Required(ErrorMessage = IdRequired)]
        [StringLength(IdMaxRange), MinLength(IdMinRange)]
        public string Id { get; set; } = null!;

        public List<string> UserIds { get; set; } = new List<string>();

        [Required(ErrorMessage = ProductNameRequired)]
        [StringLength(ProductNameMaxLength), MinLength(ProductNameMinLength)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = BrandNameRequired)]
        [StringLength(BrandMaxLength), MinLength(BrandMinLength)]
        public string Brand { get; set; } = null!;

        [StringLength(ImageUrlMaxLength), MinLength(ImageUrlMinLength)]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = SuitabilityRequired)]
        public string Suitability { get; set; } = null!;

        public string? Size { get; set; }

        [Required(ErrorMessage = PriceRequired)]
        [Range(PriceMinValue, PriceMaxValue)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = ServingsRequired)]
        public int Servings { get; set; }

        public string? Flavor { get; set; }

        [StringLength(DescriptionMaxLength), MinLength(DescriptionMinLength)]
        public string? Description { get; set; }

        [Required(ErrorMessage = QuantityRequired)]
        public int Quantity { get; set; }

        public IEnumerable<ProductReviewViewModel> Reviews = new List<ProductReviewViewModel>();

        public bool HasUserOrderedProduct { get; set; } = false;

        public bool HasUserReviewedProduct { get; set; } = false;
    }
}
