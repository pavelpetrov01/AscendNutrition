using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AscendNutrition.Web.ViewModels;

namespace AscendNutrition.Services.Data.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<IndexViewModel>> IndexGetAllProductsAsync();
        Task<IEnumerable<IndexViewModel>> GetAllProductsByCategoryAsync(string category);
        Task<ProductDetailsViewModel?> GetProductDetailsByIdAsync(Guid id);
    }
}
