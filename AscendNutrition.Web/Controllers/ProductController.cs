using AscendNutrition.Data;
using AscendNutrition.Data.Models;
using AscendNutrition.Data.Models.Enums.Review;
using AscendNutrition.Data.Repository.Interfaces;
using AscendNutrition.Services.Data.Interfaces;
using AscendNutrition.Web.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AscendNutrition.Data.Models.Enums.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace AscendNutrition.Web.Controllers
{
    public class ProductController : BaseController
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;

        }

        [HttpGet]
        public async Task<IActionResult> ProductsByCategory(string? category)
        {

            if (String.IsNullOrEmpty(category))
            {
                return RedirectToAction("Index", "Home");
            }

            IEnumerable<IndexViewModel>? model =
                await _productService.GetAllProductsByCategoryAsync(category);
            if (model == null)
            {
                return View("Index");
            }

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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<IndexViewModel> model =
                await _productService.IndexGetAllProductsAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Product");
            }
            ProductDetailsViewModel? model = await _productService.GetProductDetailsByIdAsync(id);

            if (User?.Identity?.IsAuthenticated == true)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                await _productService.CheckIfUserHasOrderedProduct(userId, id, model);
            }

            return View(model);

        }

        [Authorize]
        [HttpGet]
        public IActionResult AddReview(string? id)
        {

            if (id == null)
            {
                return RedirectToAction("Details");
            }
            var addReviewViewModel = new AddReviewViewModel
            {
                ProductId = id
            };

            return View(addReviewViewModel);

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddReview(AddReviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("AddReview");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid validUserId = Guid.Empty;
            bool isGuidValid = IsGuidValid(userId, ref validUserId);

            if (!isGuidValid)
            {
                return RedirectToAction("Details", new { id = model.ProductId });
            }

            bool result = await _productService.AddReviewToProductAsync(model, validUserId);
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Error while adding the review!");
                return RedirectToAction("Details", new { id = model.ProductId });
            }
            return RedirectToAction("Details", new { id = model.ProductId });
        }

    }
}
