﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AscendNutrition.Common
{
    public static class EntityValidationConstants
    {
        public static class Product
        {
            public const int IdMinRange = 36;
            public const int IdMaxRange = 36;
            public const double PriceMinValue = 1.00;
            public const double PriceMaxValue = 3000.00;
            public const int ProductNameMinLength = 7;
            public const int ProductNameMaxLength = 50;
            public const int BrandMinLength = 2;
            public const int BrandMaxLength = 50;
            public const int ImageUrlMinLength = 12;
            public const int ImageUrlMaxLength = 2048;
            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 500;
        }

        public static class ApplicationUser
        {
            public const int IdMinRange = 36;
            public const int IdMaxRange = 36;
            public const int FirstNameMinLength = 2;
            public const int FirstNameMaxLength = 50;
            public const int LastNameMinLength = 2;
            public const int LastNameMaxLength = 100;
            public const int AddressMinLength = 5;
            public const int AddressMaxLength = 100;
            public const int CityMinLength = 4;
            public const int CityMaxLength = 75;
        }

        public static class Category
        {
            public const int IdMinRange = 36;
            public const int IdMaxRange = 36;
            public const int CategoryNameMinLength = 5;
            public const int CategoryNameMaxLength = 50;
        }

        public static class Order
        {
            public const int IdMinRange = 36;
            public const int IdMaxRange = 36;
            public const double PriceMinValue = 1.00;
            public const double PriceMaxValue = 5000.00;
        }

        public static class Inventory
        {
            public const int IdMinRange = 36;
            public const int IdMaxRange = 36;
            public const int CityMinLength = 4;
            public const int CityMaxLength = 75;
        }

        public static class Promotion
        {
            public const int IdMinRange = 36;
            public const int IdMaxRange = 36;
            public const int PromotionNameMinLength = 10;
            public const int PromotionNameMaxLength = 100;
        }

        public static class OrderItem
        {
            public const int IdMinRange = 36;
            public const int IdMaxRange = 36;
            public const double PriceMinValue = 1.00;
            public const double PriceMaxValue = 3000.00;
        }

        public static class Review
        {
            public const int IdMinRange = 36;
            public const int IdMaxRange = 36;
            public const int CommentMinLength = 2;
            public const int CommentMaxLength = 500;
            public const double ReviewMinRange = 0;
            public const double ReviewMaxRange = 4;
        }
    }
}
