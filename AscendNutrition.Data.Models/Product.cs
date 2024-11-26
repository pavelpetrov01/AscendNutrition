﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AscendNutrition.Data.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using static AscendNutrition.Common.EntityValidationConstants.Product;

namespace AscendNutrition.Data.Models
{
    public class Product
    {
        public Product()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        [Comment("Unique identifier for the product in the database")]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        [Comment("The name of the product")]
        public string Name { get; set; } = null!;

        [Required]
        [Comment("Enum for the different products that will be offered")]
        public Category Category { get; set; }

        [Required]
        [StringLength(100)]
        [Comment("The name of the product's brand")]
        public string Brand { get; set; } = null!;

        [Comment("An url to the image illustrating the product")]
        public string? ImageUrl { get; set; }

        [Required]
        [Range(PriceMinValue, PriceMaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        [Comment("The price of a single product")]
        public decimal Price { get; set; }

        [Required]
        [Comment("Enum for the different dietary suitabilities of the products")]
        public Suitability Suitability { get; set; }

        
        [Comment("Enum for the product sizes")]
        public Size? Size { get; set; }

        [Comment("Quantity of servings per container")]
        public int? Servings { get; set; }

        [Required]
        [Comment("Available quantity of the product")]
        public int Quantity { get; set; }

        [StringLength(500)]
        [Comment("A description about the product")]
        public string? Description { get; set; }

        
        [Comment("Enum for the product's flavour")]
        public Flavour? Flavour { get; set; }
    }
}
