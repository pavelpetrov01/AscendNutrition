using AscendNutrition.Web.ViewModels.AdminArea.CategoryManagement;
using AscendNutrition.Web.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AscendNutrition.Services.Data.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<AllCategoriesViewModel>> GetAllCategoriesAsync();
        Task<IEnumerable<AllProductsByCategoryViewModel>> GetAllProductsByCategoryAsync(string? category);

        Task<AllCategoriesViewModel> GetChangeSubcategoryFormAsync(string? category);

        Task<bool> ChangeSubcategoriesAsync(AllCategoriesViewModel model);

        Task<EditCategoryViewModel> GetCategoryForEditByIdAsync(string? id);

        Task<bool> EditCategoryAsync(EditCategoryViewModel? model);

        Task<DeleteCategoryViewModel?> GetCategoryToDeleteByIdAsync(string? categoryId);
        Task<bool> SoftDeleteCategoryAsync(string? categoryId);

        Task AddCategoryAsync(AddCategoryViewModel model);
    }
}
