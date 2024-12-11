using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AscendNutrition.Common
{
    public static class ModelValidationMessages
    {
        public static class Product
        {
            public const string ProductNameRequired = "Name is required!";
            public const string BrandNameRequired = "Brand name is required!";
            public const string PriceRequired = "Price is required!";
            public const string SuitabilityRequired = "Please choose an option!";
            public const string ServingsRequired = "The amount of servings must be specified!";
            public const string QuantityRequired = "Please enter a number!";
            public const string IdRequired = "ID cannot be empty!";


        }

        public static class Review
        {
            public const string UserNameRequired = "The creator of the review must have a name!";
            public const string ProductIdRequired = "ProductId cannot be empty!";
            public const string ReviewRequired = "Rating must be between 0 and 4!";
            public const string ReviewDateRequired = "The date of the review must be set to the day it's made!";
        }

        public static class Category
        {
            public const string CategoryIdRequired = "Identificator for category is mandatory!";
            public const string CategoryNameRequired = "The category must have a name!";
        }

        public static class Inventory
        {
            public const string InventoryNameRequired = "The name of the city must be specified!";
            public const string InventoryIdRequired = "Identificator for category is mandatory!";
            public const string ProductIdRequired = "Identificator for product is mandatory!";
            public const string QuantityRequired = "Quantity of the product must be speicifed!";
            public const string TotalQuantityRequired = "Total quantity of the product across all inventories must be set!";
        }

        public static class ApplicationUser
        {
            public const string NameRequired = "Name of the user is required!";
            public const string UserIdRequired = "User Id is mandatory!";
            public const string EmailRequired = "User's email is required!";
        }

        public static class ProductInventory
        {
            public const string ProductIdRequired = "Product Id is required";
            public const string InventoryIdRequired = "Inventory Id is mandatory!";
        }
    }
}
