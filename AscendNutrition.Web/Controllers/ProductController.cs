using AscendNutrition.Data;
using AscendNutrition.Data.Models;
using AscendNutrition.Data.Repository.Interfaces;
using AscendNutrition.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AscendNutrition.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly AscendNutritionDbContext _context;
        private IRepository<Product, Guid> _productRepository;
        public ProductController(AscendNutritionDbContext context, IRepository<Product,Guid> productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }
        public IActionResult Index()
        {
            var model = _context.Products.Select(p =>
                    new IndexViewModel()
                    {
                        Name = p.Name,
                        Price = p.Price,
                        Servings = p.Servings,
                        ImageUrl = p.ImageUrl,
                    })
                .AsNoTracking()
                .ToList();
            return View(model);
        }
    }
}
