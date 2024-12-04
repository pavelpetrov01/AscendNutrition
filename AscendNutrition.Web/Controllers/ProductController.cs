using AscendNutrition.Data;
using AscendNutrition.Data.Models;
using AscendNutrition.Data.Repository.Interfaces;
using AscendNutrition.Services.Data.Interfaces;
using AscendNutrition.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace AscendNutrition.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly AscendNutritionDbContext _context;
        private IRepository<Product, Guid> _productRepository;
        private readonly IProductService _productService;
        
        public ProductController(AscendNutritionDbContext context, IRepository<Product,Guid> productRepository, IProductService productService)
        {
            _context = context;
            _productRepository = productRepository;
            _productService = productService;
            
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<IndexViewModel> model = 
                await _productService.IndexGetAllProductsAsync();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> ProductsByCategory(string category)
        {
            if (String.IsNullOrEmpty(category))
            {
                return RedirectToAction("Index", "Home");
            }

            IEnumerable<IndexViewModel> model = 
                await _productService.GetAllProductsByCategoryAsync(category);
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

        public async Task<IActionResult> Details(string? id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Product");
            }

            Guid productGuid = Guid.Empty;
            IsGuidValid(id, ref productGuid);
            if (productGuid == Guid.Empty)
            {
                return RedirectToAction("Index", "Product");
            }

            ProductDetailsViewModel? model = await _productService.GetProductDetailsByIdAsync(productGuid);
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);

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
