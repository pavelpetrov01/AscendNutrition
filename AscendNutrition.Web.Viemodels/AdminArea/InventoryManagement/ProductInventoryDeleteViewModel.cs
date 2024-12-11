using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AscendNutrition.Common.ModelValidationMessages.ProductInventory;
namespace AscendNutrition.Web.ViewModels.AdminArea.InventoryManagement
{
    public class ProductInventoryDeleteViewModel
    {
        [Required(ErrorMessage = InventoryIdRequired)]
        public Guid InventoryId { get; set; }

        [Required(ErrorMessage = ProductIdRequired)]
        public Guid ProductId { get; set; }
    }
}
