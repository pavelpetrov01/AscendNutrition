using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AscendNutrition.Data;
using AscendNutrition.Data.Models;
using AscendNutrition.Data.Models.Enums.Product;
using AscendNutrition.Data.Models.Enums.Review;
using AscendNutrition.Data.Repository.Interfaces;
using AscendNutrition.Services.Data.Interfaces;
using AscendNutrition.Web.ViewModels.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AscendNutrition.Services.Data
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IRepository<Category, Guid> _categoryRepository;
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly IRepository<Review, Guid> _reviewRepository;
        private readonly IRepository<Order, Guid> _orderRepository;
        public ProductService(IRepository<Product, Guid> productRepository, IRepository<Category, Guid> categoryRepository, IRepository<Review, Guid> reviewRepository, IRepository<Order, Guid> orderRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _reviewRepository = reviewRepository;
            _orderRepository = orderRepository;
        }

        public async Task<ProductViewModel> AddCategoriesAndEnumsIfModelStateIsNotValid(ProductViewModel model)
        {
            model.Flavors = Enum.GetValues(typeof(Flavour))
                .Cast<Flavour>()
                .Select(f => new KeyValuePair<int, string>((int)f, f.ToString()))
                .ToList();

            model.Sizes = Enum.GetValues(typeof(Size))
                .Cast<Size>()
                .Select(s => new KeyValuePair<int, string>((int)s, s.ToString()))
                .ToList();

            model.Suitabilities = Enum.GetValues(typeof(Suitability))
                    .Cast<Suitability>()
                    .Select(s => new KeyValuePair<int, string>((int)s, s.ToString()))
                    .ToList();
            var categories = await _categoryRepository.GetAllAttached().Where(c => c.IsDeleted == false).ToListAsync();
            model.Categories = categories.Select(c => new CategoryViewModel()
                {
                    CategoryId = c.Id.ToString(),
                    Name = c.Name,
                })
                .Distinct()
                .ToList();
            return model;
        }

        public async Task AddProductAsync(ProductViewModel model)
        {
            Product product = new Product()
            {
                Name = model.Name,
                Brand = model.Brand,
                Price = model.Price,
                Description = model.Description,
                Flavour = (Flavour)model.Flavor,
                ImageUrl = model.ImageUrl,
                Suitability = (Suitability)model.Suitability,
                Size = (Size)model.Size,
                Servings = model.Servings,
                Quantity = model.Quantity,
                CategoryId = model.CategoryId

            };
            await _productRepository.AddAsync(product);
        }

        public async Task<bool> AddReviewToProductAsync(AddReviewViewModel model, Guid userId)
        {
            Guid validId = Guid.Empty;
            bool isValid = IsGuidValid(model.ProductId, ref validId);
            if (isValid)
            {
                var review = new Review
                {
                    ProductId = validId,
                    CustomerId = userId,
                    Comment = model.Comment,
                    Rating = (Rating)model.Rating,
                    ReviewDate = DateTime.Now
                };

                await _reviewRepository.AddAsync(review);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<ProductDetailsViewModel> CheckIfUserHasOrderedProduct(string userId, string productId, ProductDetailsViewModel? model)
        {
            
            Guid validUserId = Guid.Empty;
            IsGuidValid(userId, ref validUserId);
            Guid productGuid = Guid.Empty;
            IsGuidValid(productId, ref productGuid);

            if (model != null)
            {
                model.HasUserOrderedProduct = await _orderRepository.GetAllAttached().Where(o => o.CustomerId == validUserId && o.IsDeleted == false)
                    .AnyAsync(o => o.OrderItems.Any(oi => oi.ProductId == productGuid));
                model.HasUserReviewedProduct = await _reviewRepository.GetAllAttached()
                    .Where(r => r.CustomerId == validUserId && r.IsDeleted == false)
                    .AnyAsync(r => r.ProductId.ToString().ToLower() == model.Id.ToLower());
            }
            return model;
        }

        public async Task<bool> EditProductAsync(ProductViewModel model)
        {
            bool result = false;
            Product product = await _productRepository.GetByIdAsync(model.Id);
            if (product.IsDeleted == true)
            {
                return result;
            }
            product.Brand = model.Brand;
            product.Price = model.Price;
            product.Description = model.Description;
            product.ImageUrl = model.ImageUrl;
            product.Name = model.Name;
            product.Suitability = (Suitability)model.Suitability;
            product.Size = (Size?)model.Size;
            product.Servings = model.Servings;
            product.Quantity = model.Quantity;
            product.CategoryId = model.CategoryId;
            product.Flavour = (Flavour?)model.Flavor;
            result = await _productRepository.UpdateAsync(product);

            return result;
        }
        public async Task<IEnumerable<IndexViewModel>> GetAllProductsByCategoryAsync(string category)
        {
           
            var parentCategory = _categoryRepository.GetAllAttached().Where(c => c.IsDeleted == false)
                .FirstOrDefault(c => c.Name.ToLower() == category.ToLower());
            IEnumerable<IndexViewModel> model = await _productRepository
                .GetAllAttached()
                .Where(p => p.Category.Name == category || p.Category.ParentCategoryId == parentCategory.Id && p.Category.IsDeleted == false)
                .Where(p => p.IsDeleted == false)
                .Include(p => p.Category.ParentCategory)
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
                .OrderBy(p => p.Name)
                .ToListAsync();
            return model;
        }

        public async Task<ProductViewModel> GetNewProductForm()
        {
            var model = new ProductViewModel()
            {
                Flavors = Enum.GetValues(typeof(Flavour))
                    .Cast<Flavour>()
                    .Select(f => new KeyValuePair<int, string>((int)f, f.ToString()))
                    .ToList(),

                Sizes = Enum.GetValues(typeof(Size))
                    .Cast<Size>()
                    .Select(s => new KeyValuePair<int, string>((int)s, s.ToString()))
                    .ToList(),

                Suitabilities = Enum.GetValues(typeof(Suitability))
                    .Cast<Suitability>()
                    .Select(s => new KeyValuePair<int, string>((int)s, s.ToString()))
                    .ToList()
            };

            var categories = await _categoryRepository
                .GetAllAttached()
                .Where(c => c.IsDeleted == false)
                .ToListAsync();
            model.Categories = categories
                .Select(c => new CategoryViewModel()
                {
                    CategoryId = c.Id.ToString(),
                    Name = c.Name,
                })
                .Distinct()
                .ToList();
            return model;
        }

       

        public async Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(string? productId)
        {
            Guid validId = Guid.Empty;
            IsGuidValid(productId, ref validId);
            Product? product = await _productRepository.GetAllAttached().Where(p => p.IsDeleted == false).FirstOrDefaultAsync(p => p.Id == validId);
            product.Reviews =
                await _reviewRepository.GetAllAttached()
                    .Where(r => r.ProductId == product.Id)
                    .Where(r => r.IsDeleted == false)
                    .Include(r => r.Customer)
                    .ToListAsync();
           
            ProductDetailsViewModel? model = null;
           
                model = new ProductDetailsViewModel()
                {
                    Id = product.Id.ToString(),
                    Name = product.Name,
                    Price = product.Price,
                    Brand = product.Brand,
                    Description = product.Description,
                    ImageUrl = product.ImageUrl,
                    Suitability = product.Suitability.ToString(),
                    Size = product.Size.ToString(),
                    Flavor = product.Flavour.ToString(),
                    Servings = product.Servings,

                    Reviews = product.Reviews.Select(r => new ProductReviewViewModel()
                    
                    {
                        Name = r.Customer.FirstName,
                        Comment = r.Comment,
                        Rating = (int)r.Rating,
                        ReviewDate = r.ReviewDate.ToString("dd/MM/yyyy"),
                    })
                    .ToList()
                    
                };
            
            
            return model;
        }

        public async Task<ProductViewModel?> GetProductForEditByIdAsync(string? id)
        {
            Guid validId = Guid.Empty;
            IsGuidValid(id, ref validId);
            ProductViewModel? model = await _productRepository.GetAllAttached()
                .Where(p => p.IsDeleted == false && p.Id == validId)
                .Select(p => new ProductViewModel()
                {
                    Name = p.Name,
                    Brand = p.Brand,
                    Price = p.Price,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Servings = p.Servings,
                    Quantity = p.Quantity,
                    CategoryId = p.CategoryId,
                })
                .FirstOrDefaultAsync();
            if (model != null)
            {
                model.Flavors = Enum.GetValues(typeof(Flavour))
                    .Cast<Flavour>()
                    .Select(f => new KeyValuePair<int, string>((int)f, f.ToString()))
                    .ToList();
                model.Sizes = Enum.GetValues(typeof(Size))
                    .Cast<Size>()
                    .Select(s => new KeyValuePair<int, string>((int)s, s.ToString()))
                    .ToList();
                model.Suitabilities = Enum.GetValues(typeof(Suitability))
                    .Cast<Suitability>()
                    .Select(s => new KeyValuePair<int, string>((int)s, s.ToString()))
                    .ToList();
                var categories = _categoryRepository
                    .GetAllAttached()
                    .Where(c => c.IsDeleted == false);
                model.Categories = categories.Select(c => new CategoryViewModel()
                    {
                        CategoryId = c.Id.ToString(),
                        Name = c.Name,
                    })
                    .Distinct()
                    .ToList();
            }
           
            return model;
        }

        public async Task<DeleteViewModel?> GetProductToDeleteByIdAsync(string? productId)
        {
            Guid validId = Guid.Empty;
            IsGuidValid(productId, ref validId);
            DeleteViewModel? model = await _productRepository.GetAllAttached().Where(p => p.Id == validId && p.IsDeleted == false)
                .Select(p => new DeleteViewModel()
                {
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price
                })
                .FirstOrDefaultAsync();
           
                return model;
        }

        
        public async Task<IEnumerable<IndexViewModel>> IndexGetAllProductsAsync()
        {
            IEnumerable<IndexViewModel> model = await _productRepository
                .GetAllAttached()
                .Where(p => p.IsDeleted == false)
                .Select(p =>
                    new IndexViewModel()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Servings = p.Servings,
                        ImageUrl = p.ImageUrl,
                        Category = p.Category.Name,
                        Quantity = p.Quantity
                    })
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .ToListAsync();
            return model;
        }

        
        public async Task<bool> SoftDeleteProductAsync(string productId)
        {
            Guid validId = Guid.Empty;
            IsGuidValid(productId, ref validId);
            Product? product = await _productRepository.FirstOrDefaultAsync(p => p.Id == validId && p.IsDeleted == false);
            if (product == null)
            {
                return false;
            }

            product.IsDeleted = true;
            return await _productRepository.UpdateAsync(product);
        }
    }
}
