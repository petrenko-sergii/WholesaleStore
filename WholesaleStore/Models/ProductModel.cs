using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WholesaleStore.Models
{
    public class ProductModel
    {
        public List<Product> Products { get; set; }
        public decimal TotalStoreSum { get; set; }

        public List<Unit> Units { get; set; }
        public List<Category> Categories { get; set; }
        public Product ProductObject { get; set; }
        public string CurrentCategoryName { get; set; }
        public int CurrentCategoryId { get; set; }
        public int CurrentUnitId { get; set; }

        public ProductModel()
        {
            Products = new List<Product>();
            Units = new List<Unit>();
            Categories = new List<Category>();
            ProductObject = new Product();
            CurrentCategoryId = new int();
            CurrentUnitId = new int();
        }
    }
}