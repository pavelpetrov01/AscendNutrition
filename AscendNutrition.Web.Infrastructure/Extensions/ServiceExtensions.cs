using AscendNutrition.Data.Models;
using AscendNutrition.Data.Repository;
using AscendNutrition.Data.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AscendNutrition.Web.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterAllRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Category, Guid>, GenericRepository<Category, Guid>>();
            services.AddScoped<IRepository<Inventory, Guid>, GenericRepository<Inventory, Guid>>();
            services.AddScoped<IRepository<Order, Guid>, GenericRepository<Order, Guid>>();
            services.AddScoped<IRepository<OrderItem, object>, GenericRepository<OrderItem, object>>();
            services.AddScoped<IRepository<Product, Guid>, GenericRepository<Product, Guid>>();
            services.AddScoped<IRepository<ProductInventory, object>, GenericRepository<ProductInventory, object>>();
            services.AddScoped<IRepository<Promotion, Guid>, GenericRepository<Promotion, Guid>>();
            services.AddScoped<IRepository<Review, Guid>, GenericRepository<Review, Guid>>();
        }
    }
}
