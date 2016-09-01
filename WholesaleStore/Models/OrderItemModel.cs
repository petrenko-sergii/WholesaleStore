using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WholesaleStore.Models;


namespace WholesaleStore.Models
{
        public class OrderItemModel
    {
        public Order Order { get; set; }
        public List<OrderItem> Items { get; set; }
        public List<Product> Products { get; set; }
        public int CurrentUserId { get; set; }
        public int CurrentOrderId { get; set; }
        public OrderItemModel()
        {
            CurrentUserId = new int();
            CurrentOrderId = new int();
        }
    }
}