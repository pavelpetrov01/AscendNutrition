using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AscendNutrition.Web.ViewModels
{
    public class ProductDetailsViewModel
    {
        [Required]
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public string Suitability { get; set; } = null!;

        public string? Size { get; set; }

        public decimal Price { get; set; }

        public int Servings { get; set; }

        public string? Flavor { get; set; }

        public string? Description { get; set; }
    }
}
