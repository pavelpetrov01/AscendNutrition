using AscendNutrition.Data;
using AscendNutrition.Data.Models;
using AscendNutrition.Data.Models.Enums.Product;
using AscendNutrition.Data.Repository.Interfaces;
using AscendNutrition.Services.Data.Interfaces;
using AscendNutrition.Web.ViewModels.AdminArea.CategoryManagement;
using AscendNutrition.Web.ViewModels.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AscendNutrition.Services.Data
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly IRepository<Category, Guid> _categoryRepository;
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly AscendNutritionDbContext _context;

        public CategoryService(IRepository<Category, Guid> categoryRepository, IRepository<Product, Guid> productRepository, AscendNutritionDbContext context)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _context = context;
        }

        public async Task<bool> ChangeSubcategoriesAsync(AllCategoriesViewModel model)
        {
            Guid productId = Guid.Empty;
            bool isValid = IsGuidValid(model.Id, ref productId);
            if (isValid)
            {

                Category? category = await _context.Categories.Include(c => c.SubCategories).Where(c => c.IsDeleted == false).FirstOrDefaultAsync(c => c.Id == productId);

                List<Guid> subCategoryIds = new List<Guid>();
                foreach (var subCategory in model.SelectedSubcategoryIds)
                {
                    Guid subCategoryId = Guid.Empty;
                    IsGuidValid(subCategory, ref subCategoryId);
                    subCategoryIds.Add(subCategoryId);
                }

                List<Guid> existingSubcategoryIds = category.SubCategories
                    .Where(sc => sc.IsDeleted == false)
                    .Select(sc => sc.Id)
                    .ToList();
                foreach (var id in subCategoryIds)
                {
                    if (!existingSubcategoryIds.Contains(id))
                    {
                        Category? subCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                        if (subCategory != null && !subCategory.IsDeleted)
                        {
                            category.SubCategories.Add(subCategory);
                        }
                    }
                }

                if (existingSubcategoryIds.Count > subCategoryIds.Count)
                {
                    foreach (var subCategoryId in existingSubcategoryIds)
                    {
                        if (!subCategoryIds.Contains(subCategoryId))
                        {
                            Category? subCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == subCategoryId);
                            if (subCategory != null && !subCategory.IsDeleted)
                            {
                                category.SubCategories.Remove(subCategory);
                            }
                        }
                    }
                }

                await _categoryRepository.UpdateAsync(category);


                return true;
            }

            return false;
        }

        public async Task<AllCategoriesViewModel> GetChangeSubcategoryFormAsync(string? category)
        {
            AllCategoriesViewModel model = null;
            Guid validGuid = Guid.Empty;
            bool isCategoryIdValid = IsGuidValid(category, ref validGuid);
            if (isCategoryIdValid)
            {
                var mainCategory = await _categoryRepository.GetAllAttached().Where(c => c.IsDeleted == false).FirstOrDefaultAsync(c => c.Id == validGuid);
                model = new AllCategoriesViewModel()
                {

                    Id = mainCategory.Id.ToString(),
                    Name = mainCategory.Name,
                    SubCategories = new List<AllSubCategoriesViewModel>()

                };
                IEnumerable<AllSubCategoriesViewModel> allSubCategories = await _categoryRepository.GetAllAttached().Where(c => c.IsDeleted == false && c.Id.ToString().ToLower() != model.Id.ToLower()).Select(c => new AllSubCategoriesViewModel()
                {
                    Id = c.Id.ToString(),
                    Name = c.Name,
                    ParentCategoryId = c.ParentCategoryId.ToString(),

                }).ToListAsync();

                model.SubCategories = allSubCategories;
                foreach (var subCategory in model.SubCategories)
                {
                    if (subCategory.ParentCategoryId != null)
                    {
                        subCategory.IsBound = subCategory.ParentCategoryId.ToLower() == mainCategory.Id.ToString().ToLower();
                    }

                }


            }
            return model;
        }

        public async Task<IEnumerable<AllCategoriesViewModel>> GetAllCategoriesAsync()
        {
            IEnumerable<AllCategoriesViewModel> allCategories = await _categoryRepository.GetAllAttached().Where(c => c.IsDeleted == false).Select(c => new AllCategoriesViewModel()
            {
                Id = c.Id.ToString(),
                Name = c.Name,
                SubCategories = new List<AllSubCategoriesViewModel>()
            }).OrderBy(c => c.Name).ToListAsync();

            IEnumerable<AllSubCategoriesViewModel> allSubCategories = await _categoryRepository.GetAllAttached().Where(c => c.IsDeleted == false && c.ParentCategory.Id != null).Select(c => new AllSubCategoriesViewModel()
            {
                Id = c.Id.ToString(),
                Name = c.Name,
                ParentCategoryId = c.ParentCategoryId.ToString(),

            }).ToListAsync();

            foreach (var category in allCategories)
            {
                category.SubCategories = allSubCategories.Where(sc => sc.ParentCategoryId == category.Id).ToList();
                foreach (var subCategory in allSubCategories)
                {
                    subCategory.IsBound = subCategory.Id == category.Id;
                }
            }
            return allCategories;
        }

        public async Task<IEnumerable<AllProductsByCategoryViewModel>> GetAllProductsByCategoryAsync(string? category)
        {
            IEnumerable<AllProductsByCategoryViewModel> model = null;
            Guid validGuid = Guid.Empty;
            bool isCategoryIdValid = IsGuidValid(category, ref validGuid);
            if (isCategoryIdValid)
            {
                var parentCategory = await _categoryRepository.GetAllAttached().Where(c => c.IsDeleted == false)
                    .FirstOrDefaultAsync(c => c.Id.ToString().ToLower() == category.ToLower());
                model = await _productRepository
                   .GetAllAttached()
                   .Where(p => p.Category.Id.ToString().ToLower() == category.ToLower() || p.Category.ParentCategoryId == parentCategory.Id)
                   .Where(p => p.IsDeleted == false)
                   .Include(p => p.Category.ParentCategory)
                   .Select(p =>
                       new AllProductsByCategoryViewModel()
                       {
                           Id = p.Id,
                           Name = p.Name,
                           Price = p.Price,
                           Servings = p.Servings,
                           ImageUrl = p.ImageUrl,
                           Flavor = p.Flavour.ToString(),
                           Brand = p.Brand,
                           Quantity = p.Quantity,
                           Size = p.Size.ToString(),
                           Suitability = p.Suitability.ToString(),
                       })
                   .AsNoTracking()
                   .ToListAsync();
            }
            return model;
        }

        public async Task<EditCategoryViewModel> GetCategoryForEditByIdAsync(string? id)
        {
            Guid categoryId = Guid.Empty;
            EditCategoryViewModel? model = null;
            bool isValid = IsGuidValid(id, ref categoryId);
            if (isValid)
            {
                Category? category = await _categoryRepository.GetAllAttached().Where(c => c.IsDeleted == false).FirstOrDefaultAsync(c => c.Id ==  categoryId);
                model = new EditCategoryViewModel()
                {
                    Id = category.Id.ToString(),
                    Name = category.Name,
                };
            }
            return model;
        }

        public async Task<bool> EditCategoryAsync(EditCategoryViewModel? model)
        {
            if(model != null)
            {
                Guid productId = Guid.Empty;
                bool isValid = IsGuidValid(model.Id, ref productId);
                if (isValid)
                {
                    Category category = await _categoryRepository.GetByIdAsync(productId);
                    category.Name = model.Name;
                    bool isEditSuccessful = await _categoryRepository.UpdateAsync(category);
                    if(isEditSuccessful)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }

        public async Task<DeleteCategoryViewModel?> GetCategoryToDeleteByIdAsync(string? categoryId)
        {
            DeleteCategoryViewModel? model = null;
           if(categoryId != null)
            {
                Guid id = Guid.Empty;
                bool isValid = IsGuidValid(categoryId, ref id);
                if(isValid)
                {
                    model = await _categoryRepository.GetAllAttached().Where(c => c.Id == id && c.IsDeleted == false).Select(c => new DeleteCategoryViewModel()
                    {
                        Id = c.Id.ToString(),
                        Name = c.Name,
                    }).FirstOrDefaultAsync();
                    if (model != null)
                    {
                        return model;
                    }
                }
            }
            return model;
        }

        public async Task<bool> SoftDeleteCategoryAsync(string? categoryId)
        {
            if(categoryId == null)
            {
                return false;
            }
            Guid id = Guid.Empty;
            bool isValid = IsGuidValid(categoryId, ref id);
            if(isValid)
            {
                Category? category = await _categoryRepository.GetAllAttached().Where(c => c.IsDeleted == false).FirstOrDefaultAsync(p => p.Id == id);
                if (category == null)
                {
                    return false;
                }

                category.IsDeleted = true;
                return await _categoryRepository.UpdateAsync(category);
            }
            else
            {
                return false;
            }
        }

        public async Task AddCategoryAsync(AddCategoryViewModel model)
        {
            Category product = new Category()
            {
                Name = model.Name
            };
            await _categoryRepository.AddAsync(product);
        }
    }
}
