using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AscendNutrition.Web.ViewModels.Product;

namespace AscendNutrition.Services.Data.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<IndexViewModel>> IndexGetAllProductsAsync(AllProductsSearchFilterViewModel input);
        Task<IEnumerable<IndexViewModel>> GetAllProductsByCategoryAsync(string category);
        Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(string id);
        Task<ProductViewModel> GetNewProductForm();
        Task<ProductViewModel?> GetProductForEditByIdAsync(string? id);
        Task<bool> EditProductAsync(ProductViewModel model);
        Task AddProductAsync(ProductViewModel model);
        Task<bool> AddReviewToProductAsync(AddReviewViewModel model, Guid userId);
        Task<DeleteViewModel?> GetProductToDeleteByIdAsync(string? productId);
        Task<bool> SoftDeleteProductAsync(string productId);

        Task<ProductViewModel> AddCategoriesAndEnumsIfModelStateIsNotValid(ProductViewModel model);
        Task<ProductDetailsViewModel> CheckIfUserHasOrderedProduct(string userId, string productId, ProductDetailsViewModel? model);
        Task<IEnumerable<IndexViewModel>> AdminIndexGetAllProductsAsync();
        Task<IEnumerable<string>> GetAllCategoriesAsync();

        Task<int> GetProductCountAsync(AllProductsSearchFilterViewModel model);

    }
}
