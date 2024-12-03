using AscendNutrition.Data;
using AscendNutrition.Data.Models;
using AscendNutrition.Data.Repository.Interfaces;
using AscendNutrition.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static AscendNutrition.Common.EntityValidationConstants;

namespace AscendNutrition.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly AscendNutritionDbContext _context;
        private IRepository<Data.Models.Product, Guid> _productRepository;
        
        public ProductController(AscendNutritionDbContext context, IRepository<Data.Models.Product,Guid> productRepository)
        {
            _context = context;
            _productRepository = productRepository;
            
        }
        public IActionResult Index()
        {
            var model = _context.Products.Select(p =>
                    new IndexViewModel()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Servings = p.Servings,
                        ImageUrl = p.ImageUrl,
                    })
                .AsNoTracking()
                .ToList();
            return View(model);
        }

        public IActionResult ProductsByCategory(string category)
        {
            if (String.IsNullOrEmpty(category))
            {
                return RedirectToAction("Index", "Home");
            }
            var parentCategory = _context.Categories
                .FirstOrDefault(c => c.Name.ToLower() == category.ToLower());
            var model = _context.Products.Where(p => p.Category.Name == category||
                        p.Category.ParentCategoryId == parentCategory.Id)
                .Include(p => p.Category.ParentCategory).Select(p =>
                    new IndexViewModel()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Servings = p.Servings,
                        ImageUrl = p.ImageUrl,
                    })
                .AsNoTracking()
            .ToList();
            if (category != "Vitamins")
            {
                ViewData["Title"] = $"All {category}s";
                
            }
            else
            {
                ViewData["Title"] = $"All {category}";
            }
            
            return View(model);
            
        }

        public IActionResult Details(string? id)
        {
            Guid productGuid = Guid.Empty;
            IsGuidValid(id, ref productGuid);



            return View();

        }

        public IActionResult Buy(string? id)
        {
            Guid productGuid = Guid.Empty;
            IsGuidValid(id, ref productGuid);



            return View();

        }

        protected bool IsGuidValid(string? id, ref Guid parsedGuid)
        {
            // Non-existing parameter in the URL
            if (String.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            // Invalid parameter in the URL
            bool isGuidValid = Guid.TryParse(id, out parsedGuid);
            if (!isGuidValid)
            {
                return false;
            }

            return true;
        }
    }
}
