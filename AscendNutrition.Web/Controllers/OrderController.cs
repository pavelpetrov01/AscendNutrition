using AscendNutrition.Web.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AscendNutrition.Data.Models;
using AscendNutrition.Data.Repository.Interfaces;
using AscendNutrition.Services.Data;
using AscendNutrition.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Providers;
using AscendNutrition.Data.Models.Enums.Order;

namespace AscendNutrition.Web.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IRepository<Order, Guid> _orderRepository;
        private readonly IRepository<OrderItem, object> _orderItemRepository;
        private readonly IRepository<Product, Guid> _productRepository;
        public OrderController(IOrderService orderService, IRepository<OrderItem, object> orderItemRepository, IRepository<Order, Guid> orderRepository, IRepository<Product,Guid> productRepository)
        {
            _orderService = orderService;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _orderItemRepository = orderItemRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            var order = await _orderService.CheckIfUserHasActiveOrderAsync(userId);
            CartViewModel model = null;
            if (order != null)
            {
                var cartItems = await _orderService.GetOrderItemsAsync(userId);
                model = new CartViewModel()
                {
                    CartItems = cartItems.ToList(),
                };
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(string? id, int quantity)
        {
            if (quantity == 0 || id == null)
            {
                return RedirectToAction("Index", "Product");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
          
            await _orderService.CreateOrEditOrderAsync(userId,id,quantity);

            var cartItems = await _orderService.GetOrderItemsAsync(userId);
            var model = new CartViewModel()
            {
                CartItems = cartItems.ToList(),
            };

            
            return RedirectToAction("Index", "Order");
        }

    }
}
