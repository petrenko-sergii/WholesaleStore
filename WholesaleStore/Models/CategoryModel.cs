using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WholesaleStore.Models
{
    public class CategoryModel
    {
        public List<Category> Categories { get; set; }
        public List<Unit> Units { get; set; }
        public Category CategoryObject { get; set; }
        public CategoryModel()
        {
            Categories = new List<Category>();
            Units = new List<Unit>();
            CategoryObject = new Category();
        }
    }
}