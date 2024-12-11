using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AscendNutrition.Data.Models;
using AscendNutrition.Web.ViewModels.Order;

namespace AscendNutrition.Services.Data.Interfaces
{
    public interface IOrderService
    {
        Task<bool> CheckIfUserHasActiveOrderAsync(string? userId);
        Task CreateNewOrderAsync(string? userId);

        Task<IEnumerable<CartItemViewModel>> GetOrderItemsAsync(string userId);

        Task AddProductToCart(string? id, int quantity, string userId);

        Task<Order> GetActiveOrderAsync(string userId);

       

        Task CreateOrEditOrderAsync(string userId, string productId, int quantity);
    }
}
