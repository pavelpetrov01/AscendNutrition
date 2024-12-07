using AscendNutrition.Data;
using AscendNutrition.Data.Models;
using AscendNutrition.Data.Repository;
using AscendNutrition.Data.Repository.Interfaces;
using AscendNutrition.Services.Data;
using AscendNutrition.Services.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AscendNutrition.Web.Infrastructure.Extensions;
namespace AscendNutrition.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string adminEmail = builder.Configuration.GetValue<string>("Administrator:Email")!;
            string adminUsername = builder.Configuration.GetValue<string>("Administrator:UserName")!;
            string adminPassword = builder.Configuration.GetValue<string>("Administrator:Password")!;
            string adminAddress = builder.Configuration.GetValue<string>("Administrator:Address")!;
            string adminCity = builder.Configuration.GetValue<string>("Administrator:City")!;
            string adminFirstName = builder.Configuration.GetValue<string>("Administrator:FirstName")!;
            string adminLastName = builder.Configuration.GetValue<string>("Administrator:LastName")!;
            int adminPostalCode = builder.Configuration.GetValue<int>("Administrator:PostalCode")!;
            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string 'Default' not found.");
            builder.Services
                .AddDbContext<AscendNutritionDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                })
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<AscendNutritionDbContext>();

            builder.Services.RegisterAllRepositories();

           
            builder.Services.AddScoped<IBaseInterface, BaseService>();
            builder.Services.AddScoped<IProductService, ProductService>();

           
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.SeedAdmin(adminEmail, adminUsername, adminPassword, adminAddress, adminCity, adminFirstName, adminLastName ,adminPostalCode);
            app.ApplyMigrations();
            app.Run();
        }
    }
}
