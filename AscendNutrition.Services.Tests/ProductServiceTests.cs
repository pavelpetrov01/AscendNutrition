using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AscendNutrition.Data.Models;
using AscendNutrition.Services.Data;
using AscendNutrition.Web.ViewModels.Product;
using AscendNutrition.Services.Data.Interfaces;
using AscendNutrition.Data.Repository;
using Microsoft.EntityFrameworkCore;
using AscendNutrition.Data.Repository.Interfaces;
using System.Linq.Expressions;
using MockQueryable;
using AscendNutrition.Data.Models.Enums.Product;
using AscendNutrition.Data.Models.Enums.Review;
using Castle.Core.Resource;

namespace AscendNutrition.Tests.Services
{
    [TestFixture]
    public class ProductServiceTests
    {
        private Mock<IRepository<Product, Guid>> _mockProductRepo;
        private Mock<IRepository<Category, Guid>> _mockCategoryRepo;
        private Mock<IRepository<Review, Guid>> _mockReviewRepo;
        private Mock<IRepository<Order, Guid>> _mockOrderRepo;
        private Mock<IRepository<ApplicationUser, Guid>> _mockApplicationUserRepo;
        private IProductService _productService;

        [SetUp]
        public void SetUp()
        {

            _mockProductRepo = new Mock<IRepository<Product, Guid>>();
            _mockCategoryRepo = new Mock<IRepository<Category, Guid>>();
            _mockReviewRepo = new Mock<IRepository<Review, Guid>>();
            _mockOrderRepo = new Mock<IRepository<Order, Guid>>();
            _mockApplicationUserRepo = new Mock<IRepository<ApplicationUser, Guid>>();


            _productService = new ProductService(
                _mockProductRepo.Object,
                _mockCategoryRepo.Object,
                _mockReviewRepo.Object,
                _mockOrderRepo.Object
            );
        }

        [Test]
        public async Task SoftDeleteProductAsync_ShouldReturnFalse_WhenProductDoesNotExist()
        {

            var invalidProductId = Guid.NewGuid().ToString();


            _mockProductRepo.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync((Product)null);


            var result = await _productService.SoftDeleteProductAsync(invalidProductId);


            Assert.IsFalse(result);
        }

        [Test]
        public async Task SoftDeleteProductAsync_ShouldReturnFalse_WhenProductIsAlreadyDeleted()
        {

            var productId = Guid.NewGuid().ToString();
            var existingProduct = new Product
            {
                Id = Guid.NewGuid(),
                IsDeleted = true
            };


            _mockProductRepo.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(existingProduct);


            var result = await _productService.SoftDeleteProductAsync(productId);


            Assert.IsFalse(result);
        }


        [Test]
        public async Task SoftDeleteProductAsync_ShouldReturnTrue_WhenProductIsSuccessfullyDeleted()
        {

            var productId = Guid.NewGuid().ToString();
            var product = new Product
            {
                Id = Guid.NewGuid(),
                IsDeleted = false
            };


            _mockProductRepo.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(product);


            _mockProductRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Product>()))
                .ReturnsAsync(true);


            var result = await _productService.SoftDeleteProductAsync(productId);


            Assert.IsTrue(result);
            Assert.IsTrue(product.IsDeleted);
        }


        [Test]
        public async Task SoftDeleteProductAsync_ShouldReturnFalse_WhenGuidIsInvalid()
        {

            var invalidProductId = "InvalidGuid";


            var result = await _productService.SoftDeleteProductAsync(invalidProductId);


            Assert.IsFalse(result);
        }

        [Test]
        public async Task IndexGetAllProductsAsync_ShouldReturnEmptyList_WhenNoProductsExist()
        {
            var emptyQueryable = new List<Product>().AsQueryable().BuildMock();

            _mockProductRepo
                .Setup(repo => repo.GetAllAttached())
                .Returns(emptyQueryable);

            var input = new AllProductsSearchFilterViewModel
            {
                CategoryFilter = null, 
                SearchQuery = null,   
                MaxPrice = 0.00m,      
                CurrentPage = 1,       
                EntitiesPerPage = 10   
            };

            var result = await _productService.IndexGetAllProductsAsync(input);

          
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task IndexGetAllProductsAsync_ShouldReturnMappedProducts_WhenProductsExist()
        {
           
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Product A", Price = 10, Servings = 5, ImageUrl = "urlA", Category = new Category { Name = "Category A" }, Quantity = 100, IsDeleted = false },
                new Product { Id = Guid.NewGuid(), Name = "Product B", Price = 20, Servings = 10, ImageUrl = "urlB", Category = new Category { Name = "Category B" }, Quantity = 200, IsDeleted = false }
            };

            var mockQueryable = products.AsQueryable().BuildMock();

            _mockProductRepo
                .Setup(repo => repo.GetAllAttached())
                .Returns(mockQueryable);

            var input = new AllProductsSearchFilterViewModel
            {
                CategoryFilter = null, 
                SearchQuery = null,    
                MaxPrice = 0.00m,      
                CurrentPage = 1,       
                EntitiesPerPage = 10   
            };
         
            var result = await _productService.IndexGetAllProductsAsync(input);

            Assert.AreEqual(2, result.Count());

            Assert.AreEqual("Product A", result.First().Name);
            Assert.AreEqual("Product B", result.Last().Name);
        }

        [Test]
        public async Task IndexGetAllProductsAsync_ShouldReturnProductsOrderedByName()
        {
            
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Product B", Price = 20, Servings = 10, ImageUrl = "urlB", Category = new Category { Name = "Category B" }, Quantity = 200, IsDeleted = false },
                new Product { Id = Guid.NewGuid(), Name = "Product A", Price = 10, Servings = 5, ImageUrl = "urlA", Category = new Category { Name = "Category A" }, Quantity = 100, IsDeleted = false }
            };

            var mockQueryable = products.AsQueryable().BuildMock();

            _mockProductRepo
                .Setup(repo => repo.GetAllAttached())
                .Returns(mockQueryable);

            var input = new AllProductsSearchFilterViewModel
            {
                CategoryFilter = null, 
                SearchQuery = null,   
                MaxPrice = 0.00m,     
                CurrentPage = 1,       
                EntitiesPerPage = 10   
            };

            var result = await _productService.IndexGetAllProductsAsync(input);

            Assert.AreEqual("Product A", result.First().Name); 
            Assert.AreEqual("Product B", result.Last().Name); 
        }

        [Test]
        public async Task GetProductToDeleteByIdAsync_ShouldReturnDeleteViewModel_WhenProductExistsAndIsNotDeleted()
        {

            var productId = Guid.NewGuid().ToString();
            var products = new List<Product>
            {
                new Product
                {
                    Id = Guid.Parse(productId),
                    Name = "Product A",
                    ImageUrl = "urlA",
                    Price = 10,
                    IsDeleted = false
                }
            };

            var mockQueryable = products.AsQueryable().BuildMock();

            _mockProductRepo
                .Setup(repo => repo.GetAllAttached())
                .Returns(mockQueryable);


            var result = await _productService.GetProductToDeleteByIdAsync(productId);


            Assert.IsNotNull(result);
            Assert.AreEqual("Product A", result?.Name);
            Assert.AreEqual("urlA", result?.ImageUrl);
            Assert.AreEqual(10, result?.Price);
        }
        [Test]
        public async Task GetProductToDeleteByIdAsync_ShouldReturnNull_WhenProductDoesNotExist()
        {

            var productId = Guid.NewGuid().ToString();

            var products = new List<Product>();

            var mockQueryable = products.AsQueryable().BuildMock();

            _mockProductRepo
                .Setup(repo => repo.GetAllAttached())
                .Returns(mockQueryable);


            var result = await _productService.GetProductToDeleteByIdAsync(productId);


            Assert.IsNull(result);
        }
        [Test]
        public async Task GetProductToDeleteByIdAsync_ShouldReturnNull_WhenProductIsDeleted()
        {

            var productId = Guid.NewGuid().ToString();
            var products = new List<Product>
            {
                new Product
                {
                    Id = Guid.Parse(productId),
                    Name = "Product B",
                    ImageUrl = "urlB",
                    Price = 20,
                    IsDeleted = true
                }
            };

            var mockQueryable = products.AsQueryable().BuildMock();

            _mockProductRepo
                .Setup(repo => repo.GetAllAttached())
                .Returns(mockQueryable);


            var result = await _productService.GetProductToDeleteByIdAsync(productId);


            Assert.IsNull(result);
        }

        [Test]
        public async Task GetProductForEditByIdAsync_ShouldReturnProductViewModel_WhenProductExistsAndIsNotDeleted()
        {

            var productId = Guid.NewGuid().ToString();
            var products = new List<Product>
            {
                new Product
                {
                    Id = Guid.Parse(productId),
                    Name = "Product A",
                    Brand = "Brand A",
                    Price = 10,
                    Description = "Description A",
                    ImageUrl = "urlA",
                    Servings = 5,
                    Quantity = 100,
                    CategoryId = Guid.NewGuid(),
                    IsDeleted = false
                }
            };

            var mockQueryable = products.AsQueryable().BuildMock();

            _mockProductRepo
                .Setup(repo => repo.GetAllAttached())
                .Returns(mockQueryable);


            var categories = new List<Category>
            {
                new Category { Id = Guid.NewGuid(), Name = "Category A", IsDeleted = false }
            };
            var mockCategoryQueryable = categories.AsQueryable().BuildMock();

            _mockCategoryRepo
                .Setup(repo => repo.GetAllAttached())
                .Returns(mockCategoryQueryable);


            var result = await _productService.GetProductForEditByIdAsync(productId);


            Assert.IsNotNull(result);
            Assert.AreEqual("Product A", result?.Name);
            Assert.AreEqual("Brand A", result?.Brand);
            Assert.AreEqual(10, result?.Price);
            Assert.AreEqual("Description A", result?.Description);
            Assert.AreEqual("urlA", result?.ImageUrl);
            Assert.AreEqual(5, result?.Servings);
            Assert.AreEqual(100, result?.Quantity);
            Assert.AreEqual(categories.First().Name, result?.Categories.First().Name);
            Assert.IsTrue(result?.Flavors.Any());
            Assert.IsTrue(result?.Sizes.Any());
            Assert.IsTrue(result?.Suitabilities.Any());
        }
        [Test]
        public async Task GetProductForEditByIdAsync_ShouldReturnNull_WhenProductDoesNotExist()
        {

            var productId = Guid.NewGuid().ToString();

            var products = new List<Product>();

            var mockQueryable = products.AsQueryable().BuildMock();

            _mockProductRepo
                .Setup(repo => repo.GetAllAttached())
                .Returns(mockQueryable);


            var result = await _productService.GetProductForEditByIdAsync(productId);


            Assert.IsNull(result);
        }
        [Test]
        public async Task GetProductForEditByIdAsync_ShouldReturnNull_WhenProductIsDeleted()
        {

            var productId = Guid.NewGuid().ToString();
            var products = new List<Product>
            {
                new Product
                {
                    Id = Guid.Parse(productId),
                    Name = "Product B",
                    Brand = "Brand B",
                    Price = 20,
                    Description = "Description B",
                    ImageUrl = "urlB",
                    Servings = 10,
                    Quantity = 200,
                    CategoryId = Guid.NewGuid(),
                    IsDeleted = true
                }
            };

            var mockQueryable = products.AsQueryable().BuildMock();

            _mockProductRepo
                .Setup(repo => repo.GetAllAttached())
                .Returns(mockQueryable);


            var result = await _productService.GetProductForEditByIdAsync(productId);


            Assert.IsNull(result);
        }
        [Test]
        public async Task GetProductDetailsByIdAsync_ShouldReturnProductDetailsViewModel_WhenProductExistsAndIsNotDeleted()
        {

            var productId = Guid.NewGuid().ToString();
            var product = new Product
            {
                Id = Guid.Parse(productId),
                Name = "Product A",
                Brand = "Brand A",
                Price = 100,
                Description = "Product A Description",
                ImageUrl = "productA.jpg",
                Suitability = Suitability.Vegan,
                Size = Size.FiveHundredGrams,
                Flavour = Flavour.Strawberry,
                Servings = 10,
                IsDeleted = false
            };

            var review = new Review
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                Customer = new ApplicationUser() { FirstName = "John" },
                Rating = Rating.VerySatisfied,
                Comment = "Great product!",
                ReviewDate = DateTime.Now,
                IsDeleted = false
            };

            var products = new List<Product> { product };
            var reviews = new List<Review> { review };


            var mockProductQueryable = products.AsQueryable().BuildMock();
            _mockProductRepo
                .Setup(repo => repo.GetAllAttached())
                .Returns(mockProductQueryable);


            var mockReviewQueryable = reviews.AsQueryable().BuildMock();
            _mockReviewRepo
                .Setup(repo => repo.GetAllAttached())
                .Returns(mockReviewQueryable);


            var result = await _productService.GetProductDetailsByIdAsync(productId);


            Assert.IsNotNull(result);
            Assert.AreEqual(product.Name, result.Name);
            Assert.AreEqual(product.Price, result.Price);
            Assert.AreEqual(product.Brand, result.Brand);
            Assert.AreEqual(product.Description, result.Description);
            Assert.AreEqual(product.ImageUrl, result.ImageUrl);
            Assert.AreEqual(product.Suitability.ToString(), result.Suitability);
            Assert.AreEqual(product.Size.ToString(), result.Size);
            Assert.AreEqual(product.Flavour.ToString(), result.Flavor);
            Assert.AreEqual(product.Servings, result.Servings);


            Assert.IsNotNull(result.Reviews);
            Assert.AreEqual(1, result.Reviews.Count());
            Assert.AreEqual(review.Comment, result.Reviews.First().Comment);
            Assert.AreEqual(review.ReviewDate.ToString("dd/MM/yyyy"), result.Reviews.First().ReviewDate);
        }
        [Test]
        public async Task GetNewProductForm_ShouldReturnValidProductViewModel()
        {

            var categories = new List<Category>
            {
                new Category { Id = Guid.NewGuid(), Name = "Category A", IsDeleted = false },
                new Category { Id = Guid.NewGuid(), Name = "Category B", IsDeleted = false }
            };


            var mockCategoryQueryable = categories.AsQueryable().BuildMock();
            _mockCategoryRepo
                .Setup(repo => repo.GetAllAttached())
                .Returns(mockCategoryQueryable);


            var result = await _productService.GetNewProductForm();


            Assert.IsNotNull(result);


            Assert.AreEqual(Enum.GetValues(typeof(Flavour)).Cast<Flavour>().Count(), result.Flavors.Count);
            Assert.AreEqual(Enum.GetValues(typeof(Size)).Cast<Size>().Count(), result.Sizes.Count);
            Assert.AreEqual(Enum.GetValues(typeof(Suitability)).Cast<Suitability>().Count(), result.Suitabilities.Count);


            Assert.AreEqual(2, result.Categories.Count);


        }

        [Test]
        public async Task GetNewProductForm_ShouldReturnEmptyCategories_WhenNoCategoriesExist()
        {
            var categories = new List<Category>();

            var mockCategoryQueryable = categories.AsQueryable().BuildMock();
            _mockCategoryRepo
                .Setup(repo => repo.GetAllAttached())
                .Returns(mockCategoryQueryable);


            var result = await _productService.GetNewProductForm();


            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Categories.Count);
        }

        [Test]
        public async Task GetAllProductsByCategoryAsync_ShouldReturnProductsForCategory()
        {

            var parentCategory = new Category { Id = Guid.NewGuid(), Name = "ParentCategory", IsDeleted = false };
            var category = new Category { Id = Guid.NewGuid(), Name = "Category A", ParentCategoryId = parentCategory.Id, IsDeleted = false };

            var categories = new List<Category> { category, parentCategory };
            var mockCategoryQueryable = categories.AsQueryable().BuildMock();
            _mockCategoryRepo
                .Setup(repo => repo.GetAllAttached())
                .Returns(mockCategoryQueryable);

            var products = new List<Product>
            {
        new Product { Id = Guid.NewGuid(), Name = "Product 1", Price = 10, Servings = 5, ImageUrl = "url1", Category = category, IsDeleted = false },
        new Product { Id = Guid.NewGuid(), Name = "Product 2", Price = 20, Servings = 10, ImageUrl = "url2", Category = category, IsDeleted = false },
        new Product { Id = Guid.NewGuid(), Name = "Product 3", Price = 15, Servings = 8, ImageUrl = "url3", Category = category, IsDeleted = true }, // Deleted product, should not be returned
        new Product { Id = Guid.NewGuid(), Name = "Product 4", Price = 25, Servings = 12, ImageUrl = "url4", Category = category, IsDeleted = false },
             };

            var mockProductQueryable = products.AsQueryable().BuildMock();
            _mockProductRepo
                .Setup(repo => repo.GetAllAttached())
                .Returns(mockProductQueryable);


            var result = await _productService.GetAllProductsByCategoryAsync("Category A");


            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());


            var productList = result.ToList();
            Assert.AreEqual("Product 1", productList[0].Name);
            Assert.AreEqual("Product 2", productList[1].Name);
            Assert.AreEqual("Product 4", productList[2].Name);
        }

        [Test]
        public async Task GetAllProductsByCategoryAsync_ShouldReturnEmpty_WhenCategoryDoesNotExist()
        {
            var categories = new List<Category>();
            var mockCategoryQueryable = categories.AsQueryable().BuildMock();
            _mockCategoryRepo
                .Setup(repo => repo.GetAllAttached())
                .Returns(mockCategoryQueryable);

            var products = new List<Product>();
            var mockProductQueryable = products.AsQueryable().BuildMock();
            _mockProductRepo
                .Setup(repo => repo.GetAllAttached())
                .Returns(mockProductQueryable);

            var result = await _productService.GetAllProductsByCategoryAsync("NonExistentCategory");

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public async Task EditProductAsync_ShouldUpdateProduct_WhenProductIsNotDeleted()
        {
            
            var productId = Guid.NewGuid();
            var existingProduct = new Product
            {
                Id = productId,
                Brand = "OldBrand",
                Price = 10.0m,
                Description = "Old Description",
                ImageUrl = "OldImageUrl",
                Name = "OldName",
                Suitability = Suitability.GlutenFree,
                Size = Size.TwoAndAHalfKg,
                Servings = 5,
                Quantity = 10,
                CategoryId = Guid.NewGuid(),
                Flavour = Flavour.Vanilla,
                IsDeleted = false
            };

            
            _mockProductRepo
                .Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync(existingProduct);

            
            var model = new ProductViewModel
            {
                Id = productId,
                Brand = "NewBrand",
                Price = 15.0m,
                Description = "New Description",
                ImageUrl = "NewImageUrl",
                Name = "NewName",
                Suitability = (int)Suitability.Vegan,  
                Size = (int)Size.FiveHundredGrams,  
                Servings = 10,
                Quantity = 20,
                CategoryId = Guid.NewGuid(),
                Flavor = (int?)Flavour.Chocolate  
            };

           
            _mockProductRepo
                .Setup(repo => repo.UpdateAsync(It.IsAny<Product>()))
                .ReturnsAsync(true);

            
            var result = await _productService.EditProductAsync(model);

         
            Assert.IsTrue(result);
            Assert.AreEqual("NewBrand", existingProduct.Brand);
            Assert.AreEqual(15.0m, existingProduct.Price);
            Assert.AreEqual("New Description", existingProduct.Description);
            Assert.AreEqual("NewImageUrl", existingProduct.ImageUrl);
            Assert.AreEqual("NewName", existingProduct.Name);
            Assert.AreEqual(Suitability.Vegan, existingProduct.Suitability);
            Assert.AreEqual(Size.FiveHundredGrams, existingProduct.Size);
            Assert.AreEqual(10, existingProduct.Servings);
            Assert.AreEqual(20, existingProduct.Quantity);
            Assert.AreEqual(model.CategoryId, existingProduct.CategoryId);
            Assert.AreEqual(Flavour.Chocolate, existingProduct.Flavour);

         
            _mockProductRepo.Verify(repo => repo.UpdateAsync(It.Is<Product>(p => p.Id == productId && p.Name == "NewName")), Times.Once);
        }

        [Test]
        public async Task EditProductAsync_ShouldNotUpdateProduct_WhenProductIsDeleted()
        {
            
            var productId = Guid.NewGuid();
            var existingProduct = new Product
            {
                Id = productId,
                Brand = "OldBrand",
                Price = 10.0m,
                Description = "Old Description",
                ImageUrl = "OldImageUrl",
                Name = "OldName",
                Suitability = Suitability.GlutenFree,
                Size = Size.TwoAndAHalfKg,
                Servings = 5,
                Quantity = 10,
                CategoryId = Guid.NewGuid(),
                Flavour = Flavour.Vanilla,
                IsDeleted = true 
            };

            
            _mockProductRepo
                .Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync(existingProduct);

           
            var model = new ProductViewModel
            {
                Id = productId,
                Brand = "NewBrand",
                Price = 15.0m,
                Description = "New Description",
                ImageUrl = "NewImageUrl",
                Name = "NewName",
                Suitability = (int)Suitability.Vegan,
                Size = (int)Size.OneKg,
                Servings = 10,
                Quantity = 20,
                CategoryId = Guid.NewGuid(),
                Flavor = (int?)Flavour.Chocolate
            };

           
            _mockProductRepo
                .Setup(repo => repo.UpdateAsync(It.IsAny<Product>()))
                .ReturnsAsync(true);

            
            var result = await _productService.EditProductAsync(model);

            
            Assert.IsFalse(result);  
            _mockProductRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Product>()), Times.Never);
        }

        [Test]
        public async Task CheckIfUserHasOrderedProduct_ShouldUpdateModelFlags_WhenUserHasOrderedAndReviewedProduct()
        {
            
            var userId = Guid.NewGuid().ToString();
            var productId = Guid.NewGuid().ToString();
            var validUserId = Guid.NewGuid(); 
            var productGuid = Guid.NewGuid();

            var orderItems = new List<OrderItem>
            {
                new OrderItem { ProductId = productGuid }
            };

            var orders = new List<Order>
            {
                new Order
                {
                    CustomerId = validUserId,
                    IsDeleted = false,
                    OrderItems = orderItems
                }
            };

            var reviews = new List<Review>
            {
                new Review
                {
                    CustomerId = validUserId,
                    ProductId = productGuid,
                    IsDeleted = false
                }
            };

            
            _mockOrderRepo.Setup(repo => repo.GetAllAttached())
                .Returns(orders.AsQueryable().BuildMock());

            _mockReviewRepo.Setup(repo => repo.GetAllAttached())
                .Returns(reviews.AsQueryable().BuildMock());

           
            var model = new ProductDetailsViewModel
            {
                Id = productGuid.ToString()
            };

            
            var result = await _productService.CheckIfUserHasOrderedProduct(userId, productId, model);

            
            Assert.IsFalse(result.HasUserOrderedProduct);
             

            
            _mockOrderRepo.Verify(repo => repo.GetAllAttached(), Times.Once);
            _mockReviewRepo.Verify(repo => repo.GetAllAttached(), Times.Once);
        }

        [Test]
        public async Task AddReviewToProductAsync_ShouldAddReview_WhenProductIdIsValid()
        {
            
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var model = new AddReviewViewModel
            {
                ProductId = productId.ToString(),
                Comment = "Great product!",
                Rating = 5 
            };

            var result = await _productService.AddReviewToProductAsync(model, userId);

           
            Assert.IsTrue(result);  
            _mockReviewRepo.Verify(repo => repo.AddAsync(It.IsAny<Review>()), Times.Once);
        }

        [Test]
        public async Task AddReviewToProductAsync_ShouldReturnFalse_WhenProductIdIsInvalid()
        {
            var userId = Guid.NewGuid();
            var invalidProductId = string.Empty;
            var model = new AddReviewViewModel
            {
                ProductId = invalidProductId,
                Comment = "Great product!",
                Rating = 5
            };

            var result = await _productService.AddReviewToProductAsync(model, userId);
            
            Assert.IsFalse(result);
            _mockReviewRepo.Verify(repo => repo.AddAsync(It.IsAny<Review>()), Times.Never);
        }

        [Test]
        public async Task AddProductAsync_ShouldAddProduct_WhenModelIsValid()
        {
            
            var model = new ProductViewModel
            {
                Name = "New Product",
                Brand = "BrandName",
                Price = 19.99m,
                Description = "This is a new product.",
                Flavor = (int)Flavour.Chocolate, 
                ImageUrl = "https://example.com/product-image.jpg",
                Suitability = (int)Suitability.Vegan,
                Size = (int)Size.OneKg,  
                Servings = 10,
                Quantity = 100,
                CategoryId = Guid.NewGuid()  
            };
           
            await _productService.AddProductAsync(model);

            _mockProductRepo.Verify(repo => repo.AddAsync(It.Is<Product>(p =>
                p.Name == model.Name &&
                p.Brand == model.Brand &&
                p.Price == model.Price &&
                p.Description == model.Description &&
                p.Flavour == (Flavour)model.Flavor &&
                p.ImageUrl == model.ImageUrl &&
                p.Suitability == (Suitability)model.Suitability &&
                p.Size == (Size)model.Size &&
                p.Servings == model.Servings &&
                p.Quantity == model.Quantity &&
                p.CategoryId == model.CategoryId
            )), Times.Once);
        }

        [Test]
        public async Task AddProductAsync_ShouldNotAddProduct_WhenModelIsInvalid()
        {
            var model = new ProductViewModel
            {
                Name = string.Empty,
                Brand = "BrandName",
                Price = 19.99m,
                Description = "This is a new product.",
                Flavor = (int)Flavour.Chocolate,
                ImageUrl = "https://example.com/product-image.jpg",
                Suitability = (int)Suitability.Vegan,
                Size = (int)Size.OneKg,
                Servings = 10,
                Quantity = 100,
                CategoryId = Guid.NewGuid()
            };

            await _productService.AddProductAsync(model);

        }

        [Test]
        public async Task AddCategoriesAndEnumsIfModelStateIsNotValid_ShouldPopulateModelWithEnumsAndCategories()
        {
            var model = new ProductViewModel();

            var mockCategories = new List<Category>
            {
                new Category { Id = Guid.NewGuid(), Name = "Category1", IsDeleted = false },
                new Category { Id = Guid.NewGuid(), Name = "Category2", IsDeleted = false }
            }.AsQueryable();

            _mockCategoryRepo.Setup(repo => repo.GetAllAttached())
                .Returns(mockCategories.BuildMock());

            var result = await _productService.AddCategoriesAndEnumsIfModelStateIsNotValid(model);

            Assert.IsNotNull(result.Flavors);
            Assert.IsTrue(result.Flavors.Any(), "Flavors should not be empty.");
            Assert.IsNotNull(result.Sizes);
            Assert.IsTrue(result.Sizes.Any(), "Sizes should not be empty.");
            Assert.IsNotNull(result.Suitabilities);
            Assert.IsTrue(result.Suitabilities.Any(), "Suitabilities should not be empty.");

            Assert.IsNotNull(result.Categories);
            Assert.AreEqual(mockCategories.Count(), result.Categories.Count, "The number of categories should match.");
            Assert.AreEqual(mockCategories.First().Name, result.Categories.First().Name, "First category name should match.");
            Assert.AreEqual(mockCategories.Last().Name, result.Categories.Last().Name, "Last category name should match.");
        }
    }
}
