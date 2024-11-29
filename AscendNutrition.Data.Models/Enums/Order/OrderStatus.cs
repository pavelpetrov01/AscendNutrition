using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AscendNutrition.Data.Models.Enums.Order
{
    public enum OrderStatus
    {
        Pending = 0,
        Shipped = 1,
        Delivered = 2,
        Cancelled = 3,
        Refunded = 4,
    }
}
