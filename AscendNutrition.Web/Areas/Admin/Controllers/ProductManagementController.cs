using AscendNutrition.Services.Data.Interfaces;
using AscendNutrition.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AscendNutrition.Data.Models;
using AscendNutrition.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using static AscendNutrition.Common.ApplicationConstants;
using AscendNutrition.Data.Models.Enums.Product;

namespace AscendNutrition.Web.Areas.Admin.Controllers
{
    [Area(AdminRoleName)]
    [Authorize(Roles = AdminRoleName)]

    public class ProductManagementController : BaseController
    {
        private readonly IProductService _productService;

        public ProductManagementController(IProductService productService)
        {
            _productService = productService;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<IndexViewModel> model =
                await _productService.AdminIndexGetAllProductsAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            ProductViewModel model = await _productService.GetNewProductForm();
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await _productService.AddCategoriesAndEnumsIfModelStateIsNotValid(model);
                return View(model);
            }

            await _productService.AddProductAsync(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Product");
            }

            ProductDetailsViewModel? model = await _productService.GetProductDetailsByIdAsync(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);

        }


        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }

            ProductViewModel? model = await _productService
                .GetProductForEditByIdAsync(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await _productService.AddCategoriesAndEnumsIfModelStateIsNotValid(model);
                return View(model);

            }

            bool isUpdateSuccessful = await _productService.EditProductAsync(model);
            if (!isUpdateSuccessful)
            {
                ModelState.AddModelError(string.Empty, "Error while updating the product!");
                return View(model);
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }

            DeleteViewModel? model = await _productService.GetProductToDeleteByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteViewModel model)
        {
            bool isDeleteSuccessful = await _productService.SoftDeleteProductAsync(model.Id);
            if (!isDeleteSuccessful)
            {
                ModelState.AddModelError(string.Empty, "An error occured while trying to delete the product!");
                return RedirectToAction("Index", new { id = model.Id });
            }

            return RedirectToAction("Index");
        }

    }
}