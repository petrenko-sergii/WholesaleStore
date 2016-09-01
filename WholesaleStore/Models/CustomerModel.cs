using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WholesaleStore.Models
{
    public class CustomerModel
    {
        public Customer CustomerObject { get; set; }
        public List<Customer> Customers { get; set; }
        public CustomerModel()
        {
            Customers = new List<Customer>();
            CustomerObject = new Customer();
        }
    }
}