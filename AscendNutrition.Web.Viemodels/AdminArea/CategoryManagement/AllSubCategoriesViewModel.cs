using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AscendNutrition.Common.ModelValidationMessages.Category;
using static AscendNutrition.Common.EntityValidationConstants.Category;

namespace AscendNutrition.Web.ViewModels.AdminArea.CategoryManagement
{
    public class AllSubCategoriesViewModel
    {
        [Required(ErrorMessage = CategoryIdRequired)]
        [StringLength(IdMaxRange), MinLength(IdMinRange)]
        public string Id { get; set; } = null!;

        [StringLength(CategoryNameMaxLength), MinLength(CategoryNameMinLength)]
        public string? Name { get; set; }

        public string? ParentCategoryId { get; set; }

        public bool IsBound { get; set; } = false;
    }
}
