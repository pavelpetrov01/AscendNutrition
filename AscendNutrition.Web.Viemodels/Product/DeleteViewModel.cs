using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AscendNutrition.Common.ModelValidationMessages.Product;
using static AscendNutrition.Common.EntityValidationConstants.Product;

namespace AscendNutrition.Web.ViewModels.Product
{
    public class DeleteViewModel
    {
        [Required(ErrorMessage = IdRequired)]
        [StringLength(IdMaxRange), MinLength(IdMinRange)]
        public string Id { get; set; } = null!;

        [Required(ErrorMessage = ProductNameRequired)]
        [StringLength(ProductNameMaxLength), MinLength(ProductNameMinLength)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = PriceRequired)]
        [Range(PriceMinValue, PriceMaxValue)]
        public decimal Price { get; set; }

        [StringLength(ImageUrlMaxLength), MinLength(ImageUrlMinLength)]
        public string? ImageUrl { get; set; }
    }
}
