using AscendNutrition.Data.Models;
using AscendNutrition.Services.Data.Interfaces;
using AscendNutrition.Web.ViewModels.AdminArea.UserManagement;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AscendNutrition.Services.Data
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            ApplicationUser? user = await _userManager
                .FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return false;
            }

            IdentityResult? result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync()
        {
            IEnumerable<ApplicationUser> allUsers = await _userManager.Users.
                ToListAsync();
            ICollection<AllUsersViewModel> allUsersViewModel = new List<AllUsersViewModel>();
            foreach (ApplicationUser user in allUsers)
            {
                IEnumerable<string> roles = await _userManager
                    .GetRolesAsync(user);

#pragma warning disable CS8601 // Possible null reference assignment.
                allUsersViewModel
                    .Add(new AllUsersViewModel()
                {
                    Email = user.Email,
                    Id = user.Id.ToString(),
                    Name = user.FirstName,
                    Roles = roles
                });
#pragma warning restore CS8601 // Possible null reference assignment.
            }
            return allUsersViewModel;
        }

        public async Task<bool> UserExistsByIdAsync(Guid userId)
        {
            ApplicationUser? user = await _userManager
                .FindByIdAsync(userId.ToString());

            return user != null;
        }
    }
}
