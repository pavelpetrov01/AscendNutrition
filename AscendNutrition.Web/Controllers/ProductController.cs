﻿using AscendNutrition.Data;
using AscendNutrition.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AscendNutrition.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly AscendNutritionDbContext _context;

        public ProductController(AscendNutritionDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = _context.Products.Select(p =>
                    new IndexViewModel()
                    {
                        Name = p.Name,
                        Price = p.Price,
                        Servings = p.Servings
                    })
                .AsNoTracking()
                .ToList();
            return View(model);
        }
    }
}
