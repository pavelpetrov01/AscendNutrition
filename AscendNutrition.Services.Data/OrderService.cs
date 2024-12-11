using AscendNutrition.Services.Data.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AscendNutrition.Common;
using AscendNutrition.Data.Models;
using AscendNutrition.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using AscendNutrition.Web.ViewModels.Order;
using AscendNutrition.Data.Models.Enums.Order;

namespace AscendNutrition.Services.Data
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IRepository<Order, Guid> _orderRepository;
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly IRepository<OrderItem, object> _orderItemRepository;
        public OrderService(IRepository<Order,Guid> orderRepository, IRepository<Product, Guid> productRepository, IRepository<OrderItem, object> orderItemRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _orderItemRepository = orderItemRepository;
        }

        public async Task AddProductToCart(string? id, int quantity,string orderId)
        {
            Product product = await _productRepository.GetAllAttached()
                .Where(p => p.Id.ToString().ToLower() == id.ToLower() && p.IsDeleted == false).FirstOrDefaultAsync();
            OrderItem order = await _orderItemRepository.GetAllAttached()
                .Where(o => o.OrderId.ToString().ToLower() == orderId.ToLower())
                .FirstOrDefaultAsync();

        }

        public async Task<bool> CheckIfUserHasActiveOrderAsync(string? userId)
        {
            if (userId == null)
            {
                return false;
            }
            bool isActive = await _orderRepository.GetAllAttached().Where(o => o.CustomerId.ToString().ToLower() == userId.ToLower()).AnyAsync();
            return isActive;
        }

        public async Task CreateNewOrderAsync(string? userId)
        {
            Guid validId = Guid.Empty;
            bool isValid = IsGuidValid(userId, ref validId);
            if (isValid)
            {
                Order order = new Order()
                {
                    CustomerId = validId
                };
                await _orderRepository.AddAsync(order);
            }
        }

        public async Task<IEnumerable<CartItemViewModel>> GetOrderItemsAsync(string userId)
        {
            Guid validId = Guid.Empty;
            bool isValid = IsGuidValid(userId, ref validId);
            IEnumerable<CartItemViewModel> model = null;
            if (isValid)
            {
               model = await _orderRepository.GetAllAttached()
                    .Where(o => o.CustomerId == validId)
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)  
                    .SelectMany(o => o.OrderItems)   
                    .Select(oi => new CartItemViewModel
                    {
                        ProductId = oi.Product.Id.ToString(),  
                        Name = oi.Product.Name,  
                        Price = oi.Product.Price,  
                        Quantity = oi.Quantity,
                        ImageUrl = oi.Product.ImageUrl
                    })
                    .ToListAsync();
            }

            return model;
        }

        public async Task<Order> GetActiveOrderAsync(string userId)
        {
            Guid validId = Guid.Empty;
            IsGuidValid(userId, ref validId);
            return await _orderRepository.FirstOrDefaultAsync(o => o.CustomerId == validId);
        }

        public async Task CreateOrEditOrderAsync(string userId, string productId, int quantity)
        {
            Guid userGuid = Guid.Empty;
            Guid productGuid = Guid.Empty;
            IsGuidValid(productId, ref productGuid);
            IsGuidValid(userId, ref userGuid);
            var order = await GetActiveOrderAsync(userId);

            if (order == null)
            {
                var product = await _productRepository
                    .FirstOrDefaultAsync(p => p.Id == productGuid);

             
                var newOrder = new Order()
                {
                    CustomerId = userGuid,
                    OrderStatus = OrderStatus.Pending
                };

                
                await _orderRepository.AddAsync(newOrder);
               
                var orderId = newOrder.Id;

               
                var newOrderItem = new OrderItem()
                {
                    OrderId = orderId, 
                    Quantity = quantity,
                    ProductId = productGuid,
                    Price = product.Price
                };

                
                await _orderItemRepository.AddAsync(newOrderItem);
             
            }
            else
            {
               
                var existingOrderItem = await _orderItemRepository
                    .FirstOrDefaultAsync(o => o.OrderId == order.Id && o.ProductId == productGuid);

                if (existingOrderItem != null)
                {
                   
                    existingOrderItem.Quantity += quantity;
                    existingOrderItem.Price = existingOrderItem.Price * 2;

                 
                    await _orderItemRepository.UpdateAsync(existingOrderItem);
                }
                else
                {
                   
                    var newOrderItem = new OrderItem()
                    {
                        OrderId = order.Id,
                        Quantity = quantity,
                        ProductId = productGuid
                    };

                    
                    await _orderItemRepository.AddAsync(newOrderItem);
                }

            }

        }

    }
}
