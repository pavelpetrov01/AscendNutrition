using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static AscendNutrition.Common.EntityValidationConstants.Category;

namespace AscendNutrition.Data.Models
{
    public class Category
    {

        [Key]
        [Comment("Unique identifier for the categories")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Comment("The name of the category")]
        [StringLength(CategoryNameMaxLength)]
        public string Name { get; set; } = null!;


        [Comment("Identifier of a possible parent category")]
        public Guid? ParentCategoryId { get; set; }

        [ForeignKey(nameof(ParentCategoryId))]
        public Category? ParentCategory { get; set; }

        [Comment("Flag which is used for soft deletion")]
        public bool IsDeleted { get; set; } = false;
        public ICollection<Category> SubCategories { get; set; } = new List<Category>();
    }
}
