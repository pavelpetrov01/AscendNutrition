using AscendNutrition.Data.Models;
using AscendNutrition.Services.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AscendNutrition.Data.Repository.Interfaces;
using Guid = System.Guid;
using MockQueryable;
using System.Linq.Expressions;
using AscendNutrition.Web.ViewModels.AdminArea.InventoryManagement;

namespace AscendNutrition.Services.Tests
{
    [TestFixture]
    public class InventoryServiceTests
    {
        private Mock<IRepository<Product, Guid>> _mockProductRepo;
        private Mock<IRepository<Inventory, Guid>> _mockInventoryRepo;
        private Mock<IRepository<ProductInventory, object>> _mockProductInventoryRepo;
        private InventoryService _inventoryService;

        [SetUp]
        public void Setup()
        {
            _mockInventoryRepo = new Mock<IRepository<Inventory, Guid>>();
            _mockProductInventoryRepo = new Mock<IRepository<ProductInventory, object>>();
            _mockProductRepo = new Mock<IRepository<Product, Guid>>();
            _inventoryService = new InventoryService(_mockInventoryRepo.Object, _mockProductRepo.Object, _mockProductInventoryRepo.Object);
        }

        [Test]
        public async Task SoftDeleteInventoryAsync_ShouldReturnFalse_WhenInvalidIdIsProvided()
        {
            var invalidId = "invalid-guid";
          
            var result = await _inventoryService.SoftDeleteInventoryAsync(invalidId);

            Assert.IsFalse(result, "Result should be false for invalid GUID");
            _mockInventoryRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Inventory>()), Times.Never);
            _mockProductInventoryRepo.Verify(repo => repo.DeleteAsync(It.IsAny<object>()), Times.Never);
        }

        [Test]
        public async Task SoftDeleteInventoryAsync_ShouldReturnFalse_WhenInventoryDoesNotExist()
        {
            var invalidId = Guid.NewGuid();

            var inventories = new List<Inventory>().AsQueryable().BuildMock();

            _mockInventoryRepo.Setup(repo => repo.GetAllAttached()).Returns(inventories);
            _mockInventoryRepo.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<Expression<Func<Inventory, bool>>>()))
                .ReturnsAsync((Inventory)null);

            var result = await _inventoryService.SoftDeleteInventoryAsync(invalidId.ToString());

            Assert.IsFalse(result, "Result should be false for non-existent inventory");
            _mockInventoryRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Inventory>()), Times.Never);
            _mockProductInventoryRepo.Verify(repo => repo.DeleteAsync(It.IsAny<object>()), Times.Never);
        }

        [Test]
        public async Task GetProductToAddForm_ShouldReturnViewModelWithProducts_WhenInventoryIdIsValid()
        {
            var inventoryId = Guid.NewGuid();
            var inventories = new List<Inventory>
            {
                new Inventory { Id = inventoryId, IsDeleted = false }
            }.AsQueryable().BuildMock();

            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Product 1", IsDeleted = false },
                new Product { Id = Guid.NewGuid(), Name = "Product 2", IsDeleted = false }
            }.AsQueryable().BuildMock();

            var mockInventoryRepository = new Mock<IRepository<Inventory, Guid>>();
            var mockProductRepository = new Mock<IRepository<Product, Guid>>();
            var mockProductInventoryRepository = new Mock<IRepository<ProductInventory, object>>();
            mockInventoryRepository.Setup(repo => repo.GetAllAttached()).Returns(inventories);
            mockProductRepository.Setup(repo => repo.GetAllAttached()).Returns(products);

            var inventoryService = new InventoryService(mockInventoryRepository.Object, mockProductRepository.Object, mockProductInventoryRepository.Object);

            var result = await inventoryService.GetProductToAddForm(inventoryId.ToString());
          
            Assert.IsNotNull(result);
            Assert.AreEqual(inventoryId.ToString(), result.InventoryId);
            Assert.IsNotNull(result.Products);
            Assert.AreEqual(2, result.Products.Count());
            Assert.AreEqual("Product 1", result.Products.First().Name);
        }

        [Test]
        public async Task GetProductForEditByIdAsync_ShouldReturnViewModel_WhenInventoryIdIsValid()
        {
            var inventoryId = Guid.NewGuid();
            var inventories = new List<Inventory>
            {
                new Inventory { Id = inventoryId, City = "Test City", IsDeleted = false }
            }.AsQueryable().BuildMock();

            var mockInventoryRepository = new Mock<IRepository<Inventory, Guid>>();
            var mockProductRepository = new Mock<IRepository<Product, Guid>>();
            var mockProductInventoryRepository = new Mock<IRepository<ProductInventory, object>>();
            mockInventoryRepository.Setup(repo => repo.GetByIdAsync(inventoryId))
                .ReturnsAsync(inventories.FirstOrDefault(i => i.Id == inventoryId));

            var inventoryService = new InventoryService(mockInventoryRepository.Object, mockProductRepository.Object, mockProductInventoryRepository.Object);

            var result = await inventoryService.GetProductForEditByIdAsync(inventoryId.ToString());
           
            Assert.IsNotNull(result);
            Assert.AreEqual(inventoryId.ToString(), result.Id);
            Assert.AreEqual("Test City", result.Name);
        }

        [Test]
        public async Task GetInventoryToDeleteByIdAsync_ShouldReturnViewModel_WhenInventoryIdIsValid()
        {
            var inventoryId = Guid.NewGuid();
            var inventories = new List<Inventory>
            {
                new Inventory { Id = inventoryId, City = "Test City", IsDeleted = false }
            }.AsQueryable().BuildMock();

            var mockInventoryRepository = new Mock<IRepository<Inventory, Guid>>();
            var mockProductRepository = new Mock<IRepository<Product, Guid>>();
            var mockProductInventoryRepository = new Mock<IRepository<ProductInventory, object>>();
            mockInventoryRepository.Setup(repo => repo.GetAllAttached()).Returns(inventories);

            var inventoryService = new InventoryService(mockInventoryRepository.Object, mockProductRepository.Object, mockProductInventoryRepository.Object);

            var result = await inventoryService.GetInventoryToDeleteByIdAsync(inventoryId.ToString());
         
            Assert.IsNotNull(result);
            Assert.AreEqual(inventoryId.ToString(), result.Id);
            Assert.AreEqual("Test City", result.Name);
        }

        [Test]
        public async Task GetInventoryToDeleteByIdAsync_ShouldReturnNull_WhenInventoryIdIsInvalid()
        {
            var invalidId = Guid.NewGuid();
            var inventories = new List<Inventory>
            {
                new Inventory { Id = Guid.NewGuid(), City = "Test City", IsDeleted = false }
            }.AsQueryable().BuildMock();

            var mockInventoryRepository = new Mock<IRepository<Inventory, Guid>>();
            var mockProductRepository = new Mock<IRepository<Product, Guid>>();
            var mockProductInventoryRepository = new Mock<IRepository<ProductInventory, object>>();
            mockInventoryRepository.Setup(repo => repo.GetAllAttached()).Returns(inventories);

            var inventoryService = new InventoryService(mockInventoryRepository.Object, mockProductRepository.Object, mockProductInventoryRepository.Object);

            var result = await inventoryService.GetInventoryToDeleteByIdAsync(invalidId.ToString());

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAllProductsByInventoryIdAsync_ShouldReturnViewModel_WhenInventoryIdIsValid()
        {
            var inventoryId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var inventories = new List<Inventory>
            {
                new Inventory
                {
                    Id = inventoryId,
                    IsDeleted = false,
                    ProductInventories = new List<ProductInventory>
                    {
                        new ProductInventory
                        {
                            ProductId = productId,
                            Quantity = 10,
                            Product = new Product { Id = productId, Name = "Test Product", Quantity = 100 }
                        }
                    }
                }
            }.AsQueryable().BuildMock();

            var mockInventoryRepository = new Mock<IRepository<Inventory, Guid>>();
            var mockProductRepository = new Mock<IRepository<Product, Guid>>();
            var mockProductInventoryRepository = new Mock<IRepository<ProductInventory, object>>();
            mockInventoryRepository.Setup(repo => repo.GetAllAttached()).Returns(inventories);

            var service = new InventoryService(mockInventoryRepository.Object, mockProductRepository.Object, mockProductInventoryRepository.Object);

            var result = await service.GetAllProductsByInventoryIdAsync(inventoryId.ToString());

            Assert.IsNotNull(result);
            Assert.AreEqual(inventoryId.ToString(), result.Id);
            Assert.AreEqual(1, result.ProductList.Count());
            Assert.AreEqual("Test Product", result.ProductList.First().Name);
            Assert.AreEqual(10, result.ProductList.First().Quantity);
            Assert.AreEqual(100, result.ProductList.First().TotalQuantity);
        }

        [Test]
        public async Task GetAllInventoriesAsync_ShouldReturnEmpty_WhenNoInventoriesExist()
        {
            var inventories = new List<Inventory>().AsQueryable().BuildMock();

            var mockInventoryRepository = new Mock<IRepository<Inventory, Guid>>();
            var mockProductRepository = new Mock<IRepository<Product, Guid>>();
            var mockProductInventoryRepository = new Mock<IRepository<ProductInventory, object>>();
            mockInventoryRepository.Setup(repo => repo.GetAllAttached()).Returns(inventories);

            var service = new InventoryService(mockInventoryRepository.Object, mockProductRepository.Object, mockProductInventoryRepository.Object);

            var result = await service.GetAllInventoriesAsync();

            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task GetAllInventoriesAsync_ShouldReturnCorrectInventories_WhenNotDeleted()
        {
            var inventoryId1 = Guid.NewGuid();
            var inventoryId2 = Guid.NewGuid();

            var inventories = new List<Inventory>
            {
                new Inventory { Id = inventoryId1, IsDeleted = false, City = "City A" },
                new Inventory { Id = inventoryId2, IsDeleted = false, City = "City B" }
            }.AsQueryable().BuildMock();

            var mockInventoryRepository = new Mock<IRepository<Inventory, Guid>>();
            var mockProductRepository = new Mock<IRepository<Product, Guid>>();
            var mockProductInventoryRepository = new Mock<IRepository<ProductInventory, object>>();
            mockInventoryRepository.Setup(repo => repo.GetAllAttached()).Returns(inventories);

            var service = new InventoryService(mockInventoryRepository.Object, mockProductRepository.Object, mockProductInventoryRepository.Object);

            var result = await service.GetAllInventoriesAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("City A", result.First().Name);
            Assert.AreEqual(inventoryId1.ToString(), result.First().Id);
            Assert.AreEqual("City B", result.Last().Name);
            Assert.AreEqual(inventoryId2.ToString(), result.Last().Id);
        }

        [Test]
        public async Task EditInventoryAsync_ShouldReturnFalse_WhenModelIsInvalid()
        {
            var model = new EditInventoryViewModel
            {
                Id = null,
                Name = "New City"
            };
            var service = new InventoryService(null, null, null);

            var result = await service.EditInventoryAsync(model);

            Assert.IsFalse(result);
        }


        [Test]
        public async Task AddProductToInventoryAsync_ShouldReturnTrue_WhenProductAndInventoryExist()
        {
            var inventoryId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var model = new AddProductToInventoryViewModel
            {
                InventoryId = inventoryId.ToString(),
                ProductId = productId.ToString(),
                Quantity = 5
            };

            var productInventoryData = new List<ProductInventory>
            {
                new ProductInventory { InventoryId = inventoryId, ProductId = productId, Quantity = 10 }
            }.AsQueryable().BuildMock();

            var productData = new List<Product>
            {
                new Product { Id = productId, Name = "Product A", Quantity = 20, IsDeleted = false }
            }.AsQueryable().BuildMock();

            var mockProductInventoryRepo = new Mock<IRepository<ProductInventory, object>>();
            var mockProductRepo = new Mock<IRepository<Product, Guid>>();
            var mockInventoryRepo = new Mock<IRepository<Inventory, Guid>>();
            mockProductInventoryRepo.Setup(repo => repo.GetAllAttached()).Returns(productInventoryData);
            mockProductRepo.Setup(repo => repo.GetAllAttached()).Returns(productData);
            mockProductRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(true);
            mockProductInventoryRepo.Setup(repo => repo.UpdateAsync(It.IsAny<ProductInventory>())).ReturnsAsync(true);

            var service = new InventoryService(mockInventoryRepo.Object, mockProductRepo.Object, mockProductInventoryRepo.Object);
            
            var result = await service.AddProductToInventoryAsync(model);

            Assert.IsTrue(result);
            mockProductRepo.Verify(repo => repo.UpdateAsync(It.Is<Product>(p => p.Id == productId && p.Quantity == 25)), Times.Once); // Product quantity should increase
            mockProductInventoryRepo.Verify(repo => repo.UpdateAsync(It.Is<ProductInventory>(pi => pi.InventoryId == inventoryId && pi.ProductId == productId && pi.Quantity == 15)), Times.Once); // ProductInventory quantity should increase
        }

        [Test]
        public async Task AddProductToInventoryAsync_ShouldReturnTrue_WhenProductIsNotInInventoryYet()
        {
            var inventoryId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var model = new AddProductToInventoryViewModel
            {
                InventoryId = inventoryId.ToString(),
                ProductId = productId.ToString(),
                Quantity = 5
            };

            var productInventoryData = new List<ProductInventory>().AsQueryable().BuildMock();
            var productData = new List<Product>
            {
                new Product { Id = productId, Name = "Product A", Quantity = 20, IsDeleted = false }
            }.AsQueryable().BuildMock();

            var mockProductInventoryRepo = new Mock<IRepository<ProductInventory, object>>();
            var mockProductRepo = new Mock<IRepository<Product, Guid>>();
            var mockInventoryRepo = new Mock<IRepository<Inventory, Guid>>();
            mockProductInventoryRepo.Setup(repo => repo.GetAllAttached()).Returns(productInventoryData);
            mockProductRepo.Setup(repo => repo.GetAllAttached()).Returns(productData);
            mockProductRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(true);
            mockProductInventoryRepo.Setup(repo => repo.AddAsync(It.IsAny<ProductInventory>()));

            var service = new InventoryService(mockInventoryRepo.Object, mockProductRepo.Object, mockProductInventoryRepo.Object);
            
            var result = await service.AddProductToInventoryAsync(model);

            Assert.IsTrue(result);
            mockProductRepo.Verify(repo => repo.UpdateAsync(It.Is<Product>(p => p.Id == productId && p.Quantity == 25)), Times.Once); // Product quantity should increase
            mockProductInventoryRepo.Verify(repo => repo.AddAsync(It.Is<ProductInventory>(pi => pi.InventoryId == inventoryId && pi.ProductId == productId && pi.Quantity == 5)), Times.Once); // New ProductInventory should be added
        }

        [Test]
        public async Task AddProductToInventoryAsync_ShouldReturnFalse_WhenProductDoesNotExist()
        {
            var inventoryId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var model = new AddProductToInventoryViewModel
            {
                InventoryId = inventoryId.ToString(),
                ProductId = productId.ToString(),
                Quantity = 5
            };

            var productInventoryData = new List<ProductInventory>().AsQueryable().BuildMock();
            var productData = new List<Product>().AsQueryable().BuildMock();

            var mockProductInventoryRepo = new Mock<IRepository<ProductInventory, object>>();
            var mockProductRepo = new Mock<IRepository<Product, Guid>>();
            var mockInventoryRepo = new Mock<IRepository<Inventory, Guid>>();

            mockProductInventoryRepo.Setup(repo => repo.GetAllAttached()).Returns(productInventoryData);
            mockProductRepo.Setup(repo => repo.GetAllAttached()).Returns(productData); 

            var service = new InventoryService(mockInventoryRepo.Object, mockProductRepo.Object, mockProductInventoryRepo.Object);

            var result = await service.AddProductToInventoryAsync(model);

            Assert.IsFalse(result);
            mockProductRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Product>()), Times.Never);
            mockProductInventoryRepo.Verify(repo => repo.AddAsync(It.IsAny<ProductInventory>()), Times.Never);
        }

        [Test]
        public async Task AddInventoryAsync_ShouldReturnFalse_WhenModelIsNull()
        {
            AddInventoryViewModel model = null;

            var mockProductInventoryRepo = new Mock<IRepository<ProductInventory, object>>();
            var mockProductRepo = new Mock<IRepository<Product, Guid>>();
            var mockInventoryRepo = new Mock<IRepository<Inventory, Guid>>();

            var service = new InventoryService(mockInventoryRepo.Object, mockProductRepo.Object, mockProductInventoryRepo.Object);

            var result = await service.AddInventoryAsync(model);

            Assert.IsFalse(result);
            mockInventoryRepo.Verify(repo => repo.AddAsync(It.IsAny<Inventory>()), Times.Never);
        }

        [Test]
        public async Task AddInventoryAsync_ShouldReturnTrue_WhenModelIsValid()
        {
            var model = new AddInventoryViewModel
            {
                Name = "New Inventory"
            };

            var mockProductInventoryRepo = new Mock<IRepository<ProductInventory, object>>();
            var mockProductRepo = new Mock<IRepository<Product, Guid>>();
            var mockInventoryRepo = new Mock<IRepository<Inventory, Guid>>();
           
            var service = new InventoryService(mockInventoryRepo.Object, mockProductRepo.Object, mockProductInventoryRepo.Object);
            
            var result = await service.AddInventoryAsync(model);
         
            Assert.IsTrue(result);
            mockInventoryRepo.Verify(repo => repo.AddAsync(It.Is<Inventory>(i => i.City == "New Inventory")), Times.Once); // Ensure AddAsync was called with the correct inventory
        }
    }
}
