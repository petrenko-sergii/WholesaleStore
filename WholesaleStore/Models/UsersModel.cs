using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WholesaleStore.Models;

namespace WholesaleStore.Models
{
    public class UsersModel
    {
        public int CurrentUserId { get; set; }
        public List<User> Users { get; set; }
        public InsertError ErrorMessageLog { get; set; }
        public InsertError ErrorMessagePassword { get; set; }
               
        public UsersModel()
        {
            Users = new List<User>();
            ErrorMessageLog = new InsertError();
            ErrorMessagePassword = new InsertError();
            CurrentUserId = new int();
        }

    }
}