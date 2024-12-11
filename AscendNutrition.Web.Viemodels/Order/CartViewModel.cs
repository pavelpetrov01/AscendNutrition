using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AscendNutrition.Common.EntityValidationConstants.Product;
namespace AscendNutrition.Web.ViewModels.Order
{
    public class CartViewModel
    {
        [StringLength(IdMaxRange), MinLength(IdMinRange)]
        public string? OrderId { get; set; }
        public List<CartItemViewModel> CartItems { get; set; } = new List<CartItemViewModel>();
        public decimal TotalSum => CartItems.Sum(item => item.ProductSum);
        public bool HasOrder => CartItems.Any();
        public bool IsEmpty => !CartItems.Any();
    }
}
