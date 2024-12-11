using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AscendNutrition.Common.EntityValidationConstants.Product;
using static AscendNutrition.Common.ModelValidationMessages.Product;
using static AscendNutrition.Common.ModelValidationMessages.Category;

namespace AscendNutrition.Web.ViewModels.Product
{
    public class ProductViewModel
    {
        [Required(ErrorMessage = IdRequired)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = ProductNameRequired)]
        [StringLength(ProductNameMaxLength), MinLength(ProductNameMinLength)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = BrandNameRequired)]
        [StringLength(BrandMaxLength), MinLength(BrandMinLength)]
        public string Brand { get; set; } = null!;

        [StringLength(ImageUrlMaxLength), MinLength(ImageUrlMinLength)]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = PriceRequired)]
        [Range(PriceMinValue, PriceMaxValue)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = SuitabilityRequired)]
        public int Suitability { get; set; }

        public int? Size { get; set; }

        [Required(ErrorMessage = ServingsRequired)]
        public int Servings { get; set; }

        [Required(ErrorMessage = QuantityRequired)]
        public int Quantity { get; set; }

        [StringLength(DescriptionMaxLength), MinLength(DescriptionMinLength)]
        public string? Description { get; set; }

        public int? Flavor { get; set; }

        [Required(ErrorMessage = CategoryIdRequired)]
        public Guid CategoryId { get; set; }

        public List<KeyValuePair<int, string>> Flavors { get; set; } = new List<KeyValuePair<int, string>>();

        public List<KeyValuePair<int, string>> Sizes { get; set; } = new List<KeyValuePair<int, string>>();

        public List<KeyValuePair<int, string>> Suitabilities { get; set; } = new List<KeyValuePair<int, string>>();

        public ICollection<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
