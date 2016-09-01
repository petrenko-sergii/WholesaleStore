using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WholesaleStore.Models;

namespace WholesaleStore.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserType Usertype { get; set; }
    }
}