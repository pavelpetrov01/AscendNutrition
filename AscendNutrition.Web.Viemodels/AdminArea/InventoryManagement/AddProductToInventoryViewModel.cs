using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AscendNutrition.Common.EntityValidationConstants.Inventory;
using static AscendNutrition.Common.ModelValidationMessages.Inventory;
namespace AscendNutrition.Web.ViewModels.AdminArea.InventoryManagement
{
    public class AddProductToInventoryViewModel
    {
        [Required(ErrorMessage = InventoryIdRequired)]
        [StringLength(IdMaxRange), MinLength(IdMinRange)]
        public string InventoryId { get; set; } = null!;
        [Required(ErrorMessage = ProductIdRequired)]
        [StringLength(IdMaxRange), MinLength(IdMinRange)]
        public string ProductId { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
    }
}
