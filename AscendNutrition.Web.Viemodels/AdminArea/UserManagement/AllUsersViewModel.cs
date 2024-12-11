using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AscendNutrition.Common.EntityValidationConstants.ApplicationUser;
using static AscendNutrition.Common.ModelValidationMessages.ApplicationUser;
using static AscendNutrition.Common.ApplicationConstants;
namespace AscendNutrition.Web.ViewModels.AdminArea.UserManagement
{
    public class AllUsersViewModel
    {
        [Required(ErrorMessage = NameRequired)]
        [StringLength(IdMaxRange), MinLength(IdMinRange)]
        public string Id { get; set; } = null!;

        [Required(ErrorMessage = NameRequired)]
        [StringLength(FirstNameMaxLength), MinLength(FirstNameMinLength)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = EmailRequired)]
        [RegularExpression(EmailRegex)]
        public string Email { get; set; } = null!;

        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }
}
