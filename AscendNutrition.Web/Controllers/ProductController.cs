using Microsoft.AspNetCore.Mvc;

namespace AscendNutrition.Web.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
