using AscendNutrition.Data.Models;
using AscendNutrition.Data.Repository.Interfaces;
using AscendNutrition.Services.Data.Interfaces;
using AscendNutrition.Web.ViewModels.AdminArea.CategoryManagement;
using AscendNutrition.Web.ViewModels.AdminArea.InventoryManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static AscendNutrition.Common.ApplicationConstants;
namespace AscendNutrition.Web.Areas.Admin.Controllers
{
    [Area(AdminRoleName)]
    [Authorize(Roles = AdminRoleName)]
    public class InventoryManagementController : Controller
    {
        private readonly IInventoryService _inventoryService;
        private readonly IRepository<Product, Guid> _productRepository;

        public InventoryManagementController(IInventoryService inventoryService, IRepository<Product, Guid> productRepository)
        {
            _inventoryService = inventoryService;
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllInventoriesViewModel> models = await _inventoryService.GetAllInventoriesAsync();
            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Manage(string? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var model = await _inventoryService.GetAllProductsByInventoryIdAsync(id);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct(string? id)
        {
            AddProductToInventoryViewModel model = await _inventoryService.GetProductToAddForm(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductToInventoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Products = await _inventoryService.GetAllProductsAsync();
                return View(model);
            }

            bool result = await _inventoryService.AddProductToInventoryAsync(model);
            if (!result)
            {
                return View(model);
            }

            return RedirectToAction("Manage");
        }

        [HttpGet]
        public IActionResult AddInventory()
        {

            return View(new AddInventoryViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddInventory(AddInventoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _inventoryService.AddInventoryAsync(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            EditInventoryViewModel model = await _inventoryService.GetInventoryForEditByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditInventoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool isEditSuccessful = await _inventoryService.EditInventoryAsync(model);
            if (isEditSuccessful)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return View("Index");
            }

            DeleteInventoryViewModel? model = await _inventoryService.GetInventoryToDeleteByIdAsync(id);
            if (model == null)
            {
                return View("Index");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteInventoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool deleteSuccessful = await _inventoryService.SoftDeleteInventoryAsync(model.Id);
            if (!deleteSuccessful)
            {
                ModelState.AddModelError(string.Empty, "An error occured while trying to delete the inventory!");
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
