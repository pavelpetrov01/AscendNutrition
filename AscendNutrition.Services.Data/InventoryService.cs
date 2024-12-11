using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AscendNutrition.Data.Models;
using AscendNutrition.Data.Repository.Interfaces;
using AscendNutrition.Services.Data.Interfaces;
using AscendNutrition.Web.ViewModels.AdminArea.CategoryManagement;
using AscendNutrition.Web.ViewModels.AdminArea.InventoryManagement;
using Microsoft.EntityFrameworkCore;

namespace AscendNutrition.Services.Data
{
    public class InventoryService : BaseService, IInventoryService
    {
        private readonly IRepository<Inventory, Guid> _inventoryRepository;
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly IRepository<ProductInventory, object> _productInventoryRepository;
        public InventoryService(IRepository<Inventory, Guid> inventoryRepository, IRepository<Product, Guid> productRepository, IRepository<ProductInventory, object> productInventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
            _productRepository = productRepository;
            _productInventoryRepository = productInventoryRepository;
        }

        public async Task<bool> AddInventoryAsync(AddInventoryViewModel model)
        {
            if (model != null)
            {
                Inventory inventory = new Inventory()
                {
                    City = model.Name,

                };
                await _inventoryRepository.AddAsync(inventory);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> AddProductToInventoryAsync(AddProductToInventoryViewModel model)
        {
            bool result = false;
            ProductInventory? productInInventory = await _productInventoryRepository.GetAllAttached().Where(pi =>
                    pi.InventoryId.ToString() == model.InventoryId && pi.ProductId.ToString() == model.ProductId)
                .FirstOrDefaultAsync();
            Product? product = await _productRepository.GetAllAttached().Where(p => p.IsDeleted == false && p.Id.ToString().ToLower() == model.ProductId)
                .FirstOrDefaultAsync();
            if (product != null && productInInventory != null)
            {
                productInInventory.Quantity += model.Quantity;
                product.Quantity += model.Quantity;
                bool updateProduct = await _productRepository.UpdateAsync(product);
                bool updateProductInventory = await _productInventoryRepository.UpdateAsync(productInInventory);
                if (updateProduct && updateProductInventory)
                {
                    result = true;
                }
            }

            if (product != null && productInInventory == null)
            {
                Guid inventoryId = Guid.Empty;
                IsGuidValid(model.InventoryId, ref inventoryId);
                ProductInventory inventory = new ProductInventory()
                {
                    InventoryId = inventoryId,
                    ProductId = product.Id,
                    Quantity = model.Quantity
                };
                product.Quantity += model.Quantity;
                bool updateProduct = await _productRepository.UpdateAsync(product);
                await _productInventoryRepository.AddAsync(inventory);
                result = true;
            }
            return result;
        }

        public async Task<bool> EditInventoryAsync(EditInventoryViewModel model)
        {
            if (model != null)
            {
                Guid productId = Guid.Empty;
                bool isValid = IsGuidValid(model.Id, ref productId);
                if (isValid)
                {
                    Inventory inventory = await _inventoryRepository.GetByIdAsync(productId);
                    inventory.City = model.Name;
                    bool isEditSuccessful = await _inventoryRepository.UpdateAsync(inventory);
                    if (isEditSuccessful)
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

        public async Task<IEnumerable<AllInventoriesViewModel>> GetAllInventoriesAsync()
        {
            IEnumerable<AllInventoriesViewModel> models = await _inventoryRepository.GetAllAttached()
                .Where(i => i.IsDeleted == false).Select(i => new AllInventoriesViewModel()
                {
                    Id = i.Id.ToString(),
                    Name = i.City
                }).OrderBy(i => i.Name).ToListAsync();
            return models;
        }

        public async Task<ManageInventoriesViewModel> GetAllProductsByInventoryIdAsync(string? id)
        {
            ManageInventoriesViewModel model = null;
            Guid validGuid = Guid.Empty;
            bool isInventoryIdValid = IsGuidValid(id, ref validGuid);
            if (isInventoryIdValid)
            {
                model = await _inventoryRepository.GetAllAttached().Where(i => i.IsDeleted == false).Select(i => new ManageInventoriesViewModel()
                {
                    Id = id,
                    ProductList = new List<AllProductsInInventoryViewModel>()
                }).FirstOrDefaultAsync();
                model.ProductList = await _inventoryRepository.GetAllAttached().Where(i => i.IsDeleted == false && i.Id == validGuid).SelectMany(i => i.ProductInventories.Select(pi => new AllProductsInInventoryViewModel()
                {
                    Id = pi.ProductId.ToString(),
                    Name = pi.Product.Name,
                    Quantity = pi.Quantity,
                    TotalQuantity = pi.Product.Quantity
                })).OrderBy(pi => pi.Name).ToListAsync();
            }

            return model;
        }

        public async Task<DeleteInventoryViewModel?> GetInventoryToDeleteByIdAsync(string? id)
        {
            DeleteInventoryViewModel? model = null;
            if (id != null)
            {
                Guid validId = Guid.Empty;
                bool isValid = IsGuidValid(id, ref validId);
                if (isValid)
                {
                    model = await _inventoryRepository.GetAllAttached().Where(i => i.Id == validId && i.IsDeleted == false).Select(i => new DeleteInventoryViewModel()
                    {
                        Id = i.Id.ToString(),
                        Name = i.City,
                    }).FirstOrDefaultAsync();
                    if (model != null)
                    {
                        return model;
                    }
                }
            }
            return model;
        }

        public async Task<EditInventoryViewModel> GetInventoryForEditByIdAsync(string? id)
        {
            Guid categoryId = Guid.Empty;
            EditInventoryViewModel? model = null;
            bool isValid = IsGuidValid(id, ref categoryId);
            if (isValid)
            {
                Inventory inventory = await _inventoryRepository.GetByIdAsync(categoryId);
                model = new EditInventoryViewModel()
                {
                    Id = inventory.Id.ToString(),
                    Name = inventory.City,
                };
            }
            return model;
        }

        public async Task<AddProductToInventoryViewModel> GetProductToAddForm(string? id)
        {
            Guid inventoryId = Guid.Empty;
            bool isValid = IsGuidValid(id, ref inventoryId);
            AddProductToInventoryViewModel model = null;
            if (isValid)
            {
                model = await _inventoryRepository.GetAllAttached()
                    .Where(i => i.IsDeleted == false && i.Id == inventoryId).Select(i =>
                        new AddProductToInventoryViewModel()
                        {
                            InventoryId = id,
                            Products = new List<ProductViewModel>()
                        }).FirstOrDefaultAsync();
                model.Products = await _productRepository.GetAllAttached().Where(p => p.IsDeleted == false).Select(p =>
                    new ProductViewModel()
                    {
                        Id = p.Id.ToString(),
                        Name = p.Name
                    }).ToListAsync();

            }

            return model;
        }

        public async Task<bool> SoftDeleteInventoryAsync(string? id)
        {
            bool isDeleteSuccessful = false;
            if (id == null)
            {
                return isDeleteSuccessful;
            }
            Guid validId = Guid.Empty;
            bool isValid = IsGuidValid(id, ref validId);
            if (isValid)
            {
                Inventory? inventory = await _inventoryRepository.FirstOrDefaultAsync(i => i.Id == validId);
                if (inventory == null)
                {
                    return isDeleteSuccessful;
                }

                inventory.IsDeleted = true;
                bool isInventoryDeleted = await _inventoryRepository.UpdateAsync(inventory);
                IEnumerable<ProductInventoryDeleteViewModel>? inventoriesToRemove = await _productInventoryRepository.GetAllAttached()
                    .Where(i => i.InventoryId == validId).Select(i => new ProductInventoryDeleteViewModel()
                    {
                        InventoryId = i.InventoryId,
                        ProductId = i.ProductId
                    }).ToListAsync();
                if (inventoriesToRemove != null)
                {
                    foreach (var productInventory in inventoriesToRemove)
                    {
                        await _productInventoryRepository.DeleteAsync(new
                        { productInventory.ProductId, productInventory.InventoryId });
                    }
                }

                if (isInventoryDeleted)
                {
                    isDeleteSuccessful = true;
                }
            }

            return isDeleteSuccessful;
        }

        public async Task<List<ProductViewModel>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAttached()
                .Where(p => !p.IsDeleted)
                .Select(p => new ProductViewModel()
                {
                    Id = p.Id.ToString(),
                    Name = p.Name
                })
                .ToListAsync();
        }
    }
}
