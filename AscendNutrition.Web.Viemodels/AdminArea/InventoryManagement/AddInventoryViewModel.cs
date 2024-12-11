﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AscendNutrition.Common.ModelValidationMessages.Inventory;
using static AscendNutrition.Common.EntityValidationConstants.Inventory;

namespace AscendNutrition.Web.ViewModels.AdminArea.InventoryManagement
{
    public class AddInventoryViewModel
    {
        [Required(ErrorMessage = InventoryNameRequired)]
        [StringLength(CityMaxLength), MinLength(CityMinLength)]
        public string Name { get; set; } = null!;
    }
}
