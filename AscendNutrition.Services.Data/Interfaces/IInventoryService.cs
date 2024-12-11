using AscendNutrition.Web.ViewModels.AdminArea.CategoryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AscendNutrition.Web.ViewModels.AdminArea.InventoryManagement;

namespace AscendNutrition.Services.Data.Interfaces
{
    public interface IInventoryService
    {
        Task<IEnumerable<AllInventoriesViewModel>> GetAllInventoriesAsync();

        Task<ManageInventoriesViewModel> GetAllProductsByInventoryIdAsync(string? id);

        Task<AddProductToInventoryViewModel> GetProductToAddForm(string? id);

        Task<bool> AddProductToInventoryAsync(AddProductToInventoryViewModel model);

        Task<bool> AddInventoryAsync(AddInventoryViewModel model);

        Task<EditInventoryViewModel> GetInventoryForEditByIdAsync(string? id);
        Task<bool> EditInventoryAsync(EditInventoryViewModel model);

        Task<DeleteInventoryViewModel?> GetInventoryToDeleteByIdAsync(string? id);
        Task<bool> SoftDeleteInventoryAsync(string? id);

        Task<List<ProductViewModel>> GetAllProductsAsync();

    }
}
