using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AscendNutrition.Data;
using AscendNutrition.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Build.ObjectModelRemoting;
using Microsoft.Identity.Client;
using static AscendNutrition.Common.ApplicationConstants;

namespace AscendNutrition.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices
                .CreateScope();
            AscendNutritionDbContext context = serviceScope
                .ServiceProvider
                .GetRequiredService<AscendNutritionDbContext>();

            context.Database.Migrate();

            return app;
        }

        public static IApplicationBuilder SeedAdmin(this IApplicationBuilder app, string email, string username, string password, string address,
            string city, string firstName, string lastName, int postalCode)
        {
            using IServiceScope serviceScope = app.ApplicationServices
                .CreateAsyncScope();

            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            RoleManager<IdentityRole<Guid>>? roleManager = serviceProvider
                .GetService<RoleManager<IdentityRole<Guid>>>();

            IUserStore<ApplicationUser>? userStore = serviceProvider
                .GetService<IUserStore<ApplicationUser>>();

            UserManager<ApplicationUser>? userManager = serviceProvider
                .GetService<UserManager<ApplicationUser>>();

            if (roleManager == null)
            {
                throw new ArgumentNullException(nameof(roleManager), $"Cannot get the service for{typeof(RoleManager<IdentityRole<Guid>>)}!");
            }

            if (userStore == null)
            {
                throw new ArgumentNullException(nameof(userStore), $"Cannot get the service for{typeof(IUserStore<ApplicationUser>)}!");
            }

            if (userManager == null)
            {
                throw new ArgumentNullException(nameof(userManager), $"Cannot get the service for{typeof(UserManager<ApplicationUser>)}!");
            }

            Task.Run(async () =>
            {
                bool doesRoleExist = await roleManager
                    .RoleExistsAsync(AdminRoleName);

                IdentityRole<Guid>? adminRole = null;
                if (!doesRoleExist)
                {
                    adminRole = new IdentityRole<Guid>(AdminRoleName);

                    var result = await roleManager
                        .CreateAsync(adminRole);

                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException($"Error occured! Cannot create {adminRole}!");
                    }
                }

                else
                {
                    adminRole = await roleManager
                        .FindByNameAsync(AdminRoleName);
                }

                ApplicationUser? admin = await userManager
                    .FindByEmailAsync(email);

                if (admin == null)
                {
                    admin = await CreateAdminAsync(email, username, password, address, city, firstName, lastName, postalCode,
                        userStore, userManager);
                }

                if (await userManager.IsInRoleAsync(admin, AdminRoleName))
                {
                    return app;
                }
                var userResult = await userManager
                    .AddToRoleAsync(admin, AdminRoleName);

                if (!userResult.Succeeded)
                {
                    throw new InvalidOperationException($"Error occured! Cannot add {username} to the {AdminRoleName} role!");
                }
                return app;
            })
                .GetAwaiter()
            .GetResult();
            return app;
        }

        private static async Task<ApplicationUser> CreateAdminAsync(string email, string username, string password, string address,
            string city, string firstName, string lastName, int postalCode, IUserStore<ApplicationUser> userStore, UserManager<ApplicationUser> userManager)
        {
            ApplicationUser user = new ApplicationUser
            {
                Email = email
            };

            await userStore
                .SetUserNameAsync(user, username, CancellationToken.None);

            user.FirstName = firstName;

            user.LastName = lastName;

            user.Address = address;

            user.City = city;

            user.PostalCode = postalCode;

            var result = await userManager
                .CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error occured! Cannot create {AdminRoleName}!");
            }

            return user;
        }
    }
}
