using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static AscendNutrition.Common.EntityValidationConstants.ApplicationUser;

namespace AscendNutrition.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
        }

        [Required]
        [StringLength(FirstNameMaxLength)]
        [Comment("First name of the user")]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(LastNameMaxLength)]
        [Comment("Last name of the user")]
        public string LastName { get; set; } = null!;


        [Required]
        [StringLength(AddressMaxLength)]
        [Comment("The address of the user")]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(CityMaxLength)]
        [Comment("The city of the user")]
        public string City { get; set; }

        [Required]
        [Comment("The post code for the city")]
        public int PostalCode { get; set; }
    }
}
