using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WholesaleStore.Models
{
    public class HomeModel
    {
        public HomeModel()
        {
            Products = new List<Product>();
            Customers = new List<Customer>();
            Categories = new List<Category>();
            Orders = new List<Order>();
            Units = new List<Unit>();                 
        }

        public List<Product> Products { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Category> Categories { get; set; }
        public List<Order> Orders { get; set; }
        public List<Unit> Units { get; set; }
        public User LogUser { get; set; }       
    }
}