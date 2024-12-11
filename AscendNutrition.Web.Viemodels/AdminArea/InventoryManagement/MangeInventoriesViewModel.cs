using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AscendNutrition.Common.ModelValidationMessages.Inventory;
using static AscendNutrition.Common.EntityValidationConstants.Inventory;

namespace AscendNutrition.Web.ViewModels.AdminArea.InventoryManagement
{
    public class ManageInventoriesViewModel
    {
        [Required(ErrorMessage = InventoryIdRequired)]
        [StringLength(IdMaxRange), MinLength(IdMinRange)]
        public string Id { get; set; } = null!;

        public IEnumerable<AllProductsInInventoryViewModel> ProductList { get; set; } =
            new List<AllProductsInInventoryViewModel>();
    }
}
