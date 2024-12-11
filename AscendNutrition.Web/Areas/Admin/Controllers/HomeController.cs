﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static AscendNutrition.Common.ApplicationConstants;
namespace AscendNutrition.Web.Areas.Admin.Controllers
{
    [Area(AdminRoleName)]
    [Authorize(Roles = AdminRoleName)]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Error(int? statusCode)
        {
            if (!statusCode.HasValue)
            {
                return View();
            }

            if (statusCode == 404)
            {
                return View("Error404");
            }


            return View("Error500");
        }
    }
}