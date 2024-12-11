using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AscendNutrition.Common.EntityValidationConstants.Review;
using static AscendNutrition.Common.ModelValidationMessages.Review;

namespace AscendNutrition.Web.ViewModels.Product
{
    public class AddReviewViewModel
    {
        [Required(ErrorMessage = ProductIdRequired)]
        [StringLength(IdMaxRange), MinLength(IdMinRange)]
        public string ProductId { get; set; } = null!;

        [StringLength(CommentMaxLength), MinLength(CommentMinLength)]
        public string? Comment { get; set; }

        [Required]
        [Range(ReviewMinRange, ReviewMaxRange, ErrorMessage = ReviewRequired)]
        public int? Rating { get; set; }
    }
}
