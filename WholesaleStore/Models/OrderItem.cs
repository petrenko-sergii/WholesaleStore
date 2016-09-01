using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WholesaleStore.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public float Amount { get; set; }
        public decimal Price { get; set; }        
        public Product Product { get; set; }
    }
}