using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AscendNutrition.Common.EntityValidationConstants.Product;
using static AscendNutrition.Common.ModelValidationMessages.Product;
namespace AscendNutrition.Web.ViewModels.Order
{
    public  class CartItemViewModel
    {
        [Required(ErrorMessage = IdRequired)]
        [StringLength(IdMaxRange), MinLength(IdMinRange)]
        public string ProductId { get; set; } = null!;

        [Required(ErrorMessage = ProductNameRequired)]
        [StringLength(ProductNameMaxLength), MinLength(ProductNameMinLength)]
        public string Name { get; set; } = null!;

        [StringLength(ImageUrlMaxLength), MinLength(ImageUrlMinLength)]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = PriceRequired)]
        [Range(PriceMinValue, PriceMaxValue)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = QuantityRequired)]
        public int Quantity { get; set; }
        public decimal ProductSum => Price * Quantity;
    }
}
