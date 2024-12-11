using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AscendNutrition.Common.EntityValidationConstants.Review;
using static AscendNutrition.Common.ModelValidationMessages.Review;
using static AscendNutrition.Common.EntityValidationConstants.ApplicationUser;
using AscendNutrition.Common;

namespace AscendNutrition.Web.ViewModels.Product
{
    public class ProductReviewViewModel
    {
        [Required]
        [StringLength(EntityValidationConstants.Review.IdMaxRange), MinLength(EntityValidationConstants.Review.IdMinRange)]
        public string Id { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;
        [Required(ErrorMessage = UserNameRequired)]
        [StringLength(FirstNameMaxLength), MinLength(FirstNameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [Range(ReviewMinRange, ReviewMaxRange, ErrorMessage = ReviewRequired)]
        public int? Rating { get; set; }

        [StringLength(CommentMaxLength), MinLength(CommentMinLength)]
        public string? Comment { get; set; }


        [Required(ErrorMessage = ReviewDateRequired)]
        public string ReviewDate { get; set; } = null!;

    }
}
