using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AscendNutrition.Data;
using AscendNutrition.Data.Models;
using AscendNutrition.Data.Repository.Interfaces;
using AscendNutrition.Services.Data.Interfaces;
using AscendNutrition.Web.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AscendNutrition.Services.Data
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Category, Guid> _categoryRepository;
        private readonly IRepository<Product, Guid> _productRepository;

        public ProductService(IRepository<Product, Guid> productRepository, IRepository<Category, Guid> categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IEnumerable<IndexViewModel>> GetAllProductsByCategoryAsync(string category)
        {
           
            var parentCategory = _categoryRepository
                .FirstOrDefault(c => c.Name.ToLower() == category.ToLower());
            IEnumerable<IndexViewModel> model = await _productRepository.GetAllAttached().Where(p => p.Category.Name == category ||
                                                       p.Category.ParentCategoryId == parentCategory.Id)
                .Include(p => p.Category.ParentCategory).Select(p =>
                    new IndexViewModel()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Servings = p.Servings,
                        ImageUrl = p.ImageUrl,
                    })
                .AsNoTracking()
                .ToListAsync();
            return model;
        }

        public async Task<ProductDetailsViewModel?> GetProductDetailsByIdAsync(Guid id)
        {

            Product? product = await _productRepository.FirstOrDefaultAsync(p => p.Id == id);
            ProductDetailsViewModel? model = null;
            if (product != null)
            {
                model = new ProductDetailsViewModel()
                {
                    Name = product.Name,
                    Price = product.Price,
                    Brand = product.Brand,
                    Description = product.Description,
                    ImageUrl = product.ImageUrl,
                    Suitability = product.Suitability.ToString(),
                    Size = product.Size.ToString(),
                    Flavor = product.Flavour.ToString(),
                    Servings = product.Servings
                };
            }

            return model;
        }

        public async Task<IEnumerable<IndexViewModel>> IndexGetAllProductsAsync()
        {
            IEnumerable<IndexViewModel> model = await _productRepository
                .GetAllAttached()
                .Select(p =>
                    new IndexViewModel()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Servings = p.Servings,
                        ImageUrl = p.ImageUrl,
                    })
                .AsNoTracking()
                .ToListAsync();
            return model;
        }
    }
}
