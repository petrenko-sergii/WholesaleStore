using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WholesaleStore.Models
{
    public class Order
    {
        public Order()
        {
            Items = new List<OrderItem>();
        }
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public decimal TotalSum { get; set; }
        public DateTime Date { get; set; }
        public Customer Customer { get; set; }
        public User User { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}