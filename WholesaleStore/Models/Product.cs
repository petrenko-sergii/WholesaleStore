using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WholesaleStore.Models;


namespace WholesaleStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BarCode { get; set; }
        public float Amount { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public Unit Unit { get; set; }
        //public Product ProductObject { get; set; }
        //public Product()
        //{
        //    ProductObject = new Product();
        //}
    }
}