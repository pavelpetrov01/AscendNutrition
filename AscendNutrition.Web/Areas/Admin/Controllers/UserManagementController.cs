using AscendNutrition.Data.Models;
using AscendNutrition.Services.Data.Interfaces;
using AscendNutrition.Web.ViewModels.AdminArea.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static AscendNutrition.Common.ApplicationConstants;

namespace AscendNutrition.Web.Areas.Admin.Controllers
{
    [Area(AdminRoleName)]
    [Authorize(Roles = AdminRoleName)]
    public class UserManagementController : BaseController
    {

        private readonly IUserService _userService;
        public UserManagementController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllUsersViewModel> allUsers = await _userService.GetAllUsersAsync();

            return View(allUsers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            Guid userIdGuid = Guid.Empty;
            if (!IsGuidValid(userId, ref userIdGuid))
            {
                return RedirectToAction("Index");
            }

            bool userExists = await _userService.UserExistsByIdAsync(userIdGuid);

            if (!userExists)
            {
                return RedirectToAction("Index");
            }

            bool isUserDeleted = await _userService.DeleteUserAsync(userIdGuid);
            if (!isUserDeleted)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}