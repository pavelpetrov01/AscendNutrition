using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using AscendNutrition.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AscendNutrition.Data
{
    public class AscendNutritionDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public AscendNutritionDbContext()
        {
            
        }

        public AscendNutritionDbContext(DbContextOptions options)
        : base(options) 
        {
            
        }

        public virtual DbSet<Product> Products { get; set; } = null!;

        public virtual DbSet<Category> Categories { get; set; } = null!;

        public virtual DbSet<Order> Orders { get; set; } = null!;

        public virtual DbSet<Inventory> Inventories { get; set; } = null!;

        public virtual DbSet<ProductInventory> ProductInventories { get; set; } = null!;

        public virtual DbSet<Promotion> Promotions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
        }
    }
}
