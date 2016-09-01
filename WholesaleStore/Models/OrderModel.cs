using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WholesaleStore.Models
{
    public class OrderModel
    {
        public List<Order> Orders { get; set; }
        public string CurrentCustomerName { get; set; }
        public OrderModel ()
	    {
            Orders = new List<Order>();

	    }
    }
}
