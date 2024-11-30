using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AscendNutrition.Data.Models.Enums.Review;
using Microsoft.EntityFrameworkCore;
using static AscendNutrition.Common.EntityValidationConstants.Review;

namespace AscendNutrition.Data.Models
{
    public class Review
    {
        [Key]
        [Comment("Unique identifier for the review")]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ProductId { get; set; }

        [ForeignKey(nameof(ProductId))] 
        public Product Product { get; set; } = null!;

        public Guid CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public ApplicationUser Customer { get; set; } = null!;

        [Comment("Enum for the rating of the product")]
        public Rating Rating { get; set; }

        [StringLength(CommentMaxLength)]
        [Comment("Additional comment to the review")]
        public string? Comment { get; set; }

        [Required]
        [Comment("The day on which the review was made")]
        public DateTime ReviewDate { get; set; }

    }
}
