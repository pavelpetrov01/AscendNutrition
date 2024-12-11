using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static AscendNutrition.Common.ApplicationConstants;

namespace AscendNutrition.Web.Areas.Admin.Controllers
{
    [Area(AdminRoleName)]
    [Authorize(Roles = AdminRoleName)]
    public class PromotionManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
