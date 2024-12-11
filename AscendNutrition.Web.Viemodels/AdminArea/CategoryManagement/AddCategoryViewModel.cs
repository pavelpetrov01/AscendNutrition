﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AscendNutrition.Common.ModelValidationMessages.Category;
using static AscendNutrition.Common.EntityValidationConstants.Category;

namespace AscendNutrition.Web.ViewModels.AdminArea.CategoryManagement
{
    public class AddCategoryViewModel
    {
        [Required(ErrorMessage = CategoryNameRequired)]
        [StringLength(CategoryNameMaxLength), MinLength(CategoryNameMinLength)]
        public string Name { get; set; } = null!;
    }
}
