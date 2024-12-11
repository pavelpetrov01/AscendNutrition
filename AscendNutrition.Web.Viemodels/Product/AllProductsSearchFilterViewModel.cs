using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AscendNutrition.Web.ViewModels.Product
{
    public class AllProductsSearchFilterViewModel
    {
        public IEnumerable<IndexViewModel>? AllProducts { get; set; }

       
        public string? SearchQuery { get; set; }

        public string? CategoryFilter { get; set; }

        public IEnumerable<string>? AllCategories { get; set; }
       
        public decimal? MaxPrice { get; set; }

        public int? CurrentPage { get; set; } = 1;

        public int? TotalPages { get; set; }

        public int? EntitiesPerPage { get; set; } = 6;

    }
}
