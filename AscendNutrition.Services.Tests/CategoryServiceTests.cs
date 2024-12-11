using AscendNutrition.Services.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AscendNutrition.Data;
using AscendNutrition.Data.Models;
using AscendNutrition.Data.Repository.Interfaces;
using AscendNutrition.Web.ViewModels.AdminArea.CategoryManagement;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using MockQueryable;
using AscendNutrition.Data.Models.Enums.Product;
using AscendNutrition.Services.Data.Interfaces;

namespace AscendNutrition.Services.Tests
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private Mock<IRepository<Category, Guid>> _mockCategoryRepo;
        private Mock<IRepository<Product, Guid>> _mockProductRepo;
        private Mock<AscendNutritionDbContext> _mockContext;
        private CategoryService _categoryService;
        private ProductService _productService;
        [SetUp]
        public void SetUp()
        {
            _mockCategoryRepo = new Mock<IRepository<Category, Guid>>();
            _mockProductRepo = new Mock<IRepository<Product, Guid>>();

            _mockContext = new Mock<AscendNutritionDbContext>();

            _categoryService = new CategoryService(_mockCategoryRepo.Object, _mockProductRepo.Object, _mockContext.Object);
        }
        private IQueryable<Category> GetMockCategoriesQueryable(List<Category> categories)
        {
            var categoryQueryable = categories.AsQueryable();
            return categoryQueryable.BuildMock();
        }

        
        private bool IsGuidValid(string input, ref string output)
        {
            output = input;
            return !string.IsNullOrEmpty(input);
        }

        [Test]
        public async Task ChangeSubcategoriesAsync_ShouldReturnFalse_WhenCategoryNotFound()
        {
            var categoryId = "invalid-category-id";
            var model = new AllCategoriesViewModel
            {
                Id = categoryId,
                SelectedSubcategoryIds = new List<string> { "subcategory-id-1" }
            };
            
            var result = await _categoryService.ChangeSubcategoriesAsync(model);

            
            Assert.IsFalse(result);
        }

        [Test]
        public async Task ChangeSubcategoriesAsync_ShouldAddSubcategories_WhenSubcategoriesAreNew()
        {
           
            var categoryId = "category-id-1";
            var subCategoryId1 = "subcategory-id-1";
            var subCategoryId2 = "subcategory-id-2";

           
            Guid categoryGuid = Guid.NewGuid();
            Guid subCategoryGuid1 = Guid.NewGuid();
            Guid subCategoryGuid2 = Guid.NewGuid();

            var category = new Category
            {
                Id = categoryGuid,
                SubCategories = new List<Category>(),
                IsDeleted = false
            };

            var subCategory1 = new Category
            {
                Id = subCategoryGuid1,
                IsDeleted = false
            };
            var subCategory2 = new Category
            {
                Id = subCategoryGuid2,
                IsDeleted = false
            };

            var model = new AllCategoriesViewModel
            {
                Id = categoryId,
                SelectedSubcategoryIds = new List<string> { subCategoryId1, subCategoryId2 }
            };

            var result = await _categoryService.ChangeSubcategoriesAsync(model);

            Assert.IsFalse(result);
            Assert.AreEqual(0, category.SubCategories.Count);
           
        }

        [Test]
        public async Task GetChangeSubcategoryFormAsync_ShouldReturnModelWithSubcategories_WhenCategoryIdIsValid()
        {
            Guid categoryGuid = Guid.NewGuid();

            var mainCategory = new Category
            {
                Id = categoryGuid,
                Name = "Main Category",
                IsDeleted = false
            };

            var subCategory1 = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Subcategory 1",
                ParentCategoryId = categoryGuid,
                IsDeleted = false
            };

            var subCategory2 = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Subcategory 2",
                ParentCategoryId = Guid.NewGuid(),
                IsDeleted = false
            };

            
            _mockCategoryRepo.Setup(m => m.GetAllAttached())
                .Returns(GetMockCategoriesQueryable(new List<Category> { mainCategory, subCategory1, subCategory2 }));
            
            var result = await _categoryService.GetChangeSubcategoryFormAsync(categoryGuid.ToString());
           
            Assert.IsNotNull(result);
            Assert.AreEqual(categoryGuid.ToString(), result.Id); 
            Assert.AreEqual("Main Category", result.Name); 

            
            Assert.AreEqual(2, result.SubCategories.Count()); 
            var subcategory1 = result.SubCategories.First(sc => sc.Id == subCategory1.Id.ToString());
            Assert.IsTrue(subcategory1.IsBound);

            var subcategory2 = result.SubCategories.First(sc => sc.Id == subCategory2.Id.ToString());
            Assert.IsFalse(subcategory2.IsBound);
        }

        [Test]
        public async Task GetChangeSubcategoryFormAsync_ShouldReturnNull_WhenCategoryIdIsInvalid()
        {
            var invalidCategoryId = "invalid-category-id";

            _mockCategoryRepo.Setup(m => m.GetAllAttached())
                .Returns(GetMockCategoriesQueryable(new List<Category>()));
            var result = await _categoryService.GetChangeSubcategoryFormAsync(invalidCategoryId);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetChangeSubcategoryFormAsync_ShouldReturnValidModel_WhenCategoryIsValid()
        {
            var categoryId = Guid.NewGuid();
            var categoryName = "Main Category";

            var mainCategory = new Category
            {
                Id = categoryId,
                Name = categoryName,
                IsDeleted = false,
                ParentCategoryId = null
            };

            var subCategory1 = new Category
            {
                Id = Guid.NewGuid(),
                Name = "SubCategory 1",
                IsDeleted = false,
                ParentCategoryId = categoryId
            };

            var subCategory2 = new Category
            {
                Id = Guid.NewGuid(),
                Name = "SubCategory 2",
                IsDeleted = false,
                ParentCategoryId = categoryId
            };

            var mockCategoryRepo = new Mock<IRepository<Category, Guid>>();
            var mockProductRepo = new Mock<IRepository<Product, Guid>>();
            var mockDbContext = new Mock<AscendNutritionDbContext>();

            var categories = new List<Category> { mainCategory, subCategory1, subCategory2 }.AsQueryable();
            mockCategoryRepo.Setup(repo => repo.GetAllAttached()).Returns(categories.BuildMock());

            var service = new CategoryService(mockCategoryRepo.Object, mockProductRepo.Object, mockDbContext.Object);

            var result = await service.GetChangeSubcategoryFormAsync(categoryId.ToString());

            Assert.IsNotNull(result);
            Assert.AreEqual(categoryId.ToString(), result.Id);
            Assert.AreEqual(categoryName, result.Name);
            Assert.AreEqual(2, result.SubCategories.Count());
            Assert.IsTrue(result.SubCategories.Any(sc => sc.Name == "SubCategory 1"));
            Assert.IsTrue(result.SubCategories.Any(sc => sc.Name == "SubCategory 2"));

            Assert.IsTrue(result.SubCategories.All(sc => sc.IsBound == true));
        }

    }
}
