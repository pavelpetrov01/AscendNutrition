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
