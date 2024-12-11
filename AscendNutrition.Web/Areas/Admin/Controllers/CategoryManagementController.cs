using AscendNutrition.Services.Data;
using AscendNutrition.Services.Data.Interfaces;
using AscendNutrition.Web.ViewModels.AdminArea.CategoryManagement;
using AscendNutrition.Web.ViewModels.AdminArea.UserManagement;
using AscendNutrition.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static AscendNutrition.Common.ApplicationConstants;
namespace AscendNutrition.Web.Areas.Admin.Controllers
{

    [Area(AdminRoleName)]
    [Authorize(Roles = AdminRoleName)]
    public class CategoryManagementController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryManagementController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllCategoriesViewModel> allCategories = await _categoryService.GetAllCategoriesAsync();

            return View(allCategories);
        }

        [HttpGet]
        public async Task<IActionResult> SeeAll(string? category)
        {
            if (String.IsNullOrEmpty(category))
            {
                return RedirectToAction("Index", "Home");
            }

            IEnumerable<AllProductsByCategoryViewModel> model =
                await _categoryService.GetAllProductsByCategoryAsync(category);
            if (model == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ChangeSubcategoriesAsync(string? category)
        {

            AllCategoriesViewModel model = await _categoryService.GetChangeSubcategoryFormAsync(category);
            if (model == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeSubcategoriesAsync(AllCategoriesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("SelectedSubcategoryIds", "Error while updating the category!");
                model = await _categoryService.GetChangeSubcategoryFormAsync(model.Id);
                return View(model);
            }

            bool isUpdateSuccessful = await _categoryService.ChangeSubcategoriesAsync(model);
            if (!isUpdateSuccessful)
            {
                ModelState.AddModelError("SelectedSubcategoryIds", "Error while updating the category!");
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditCategoryAsync(string? id)
        {
            EditCategoryViewModel? model = await _categoryService.GetCategoryForEditByIdAsync(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategoryAsync(EditCategoryViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool isUpdateSuccessful = await _categoryService.EditCategoryAsync(model);
            if (!isUpdateSuccessful)
            {
                ModelState.AddModelError(string.Empty, "Error while updating the category!");
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsync(string? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            DeleteCategoryViewModel? model = await _categoryService.GetCategoryToDeleteByIdAsync(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(DeleteCategoryViewModel? model)
        {
            if (model != null)
            {
                bool isDeleteSuccessful = await _categoryService.SoftDeleteCategoryAsync(model.Id);
                if (!isDeleteSuccessful)
                {
                    ModelState.AddModelError(string.Empty, "An error occured while trying to delete the product!");
                    return RedirectToAction("Index");
                }
            }
            

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddCategoryAsync()
        {
            return View(new AddCategoryViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategoryAsync(AddCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return View(model);
            }

            await _categoryService.AddCategoryAsync(model);
            return RedirectToAction("Index");
        }

    }
}
