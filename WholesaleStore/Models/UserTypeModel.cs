using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WholesaleStore.Models
{
    public class UserTypeModel
    {
        public string NewUserName { get; set; }
        public List<UserType> UserTypes { get; set; }
        public int CurrentUserTypeId { get; set; }
        public UserTypeModel()
        {
            UserTypes = new List<UserType>();
            NewUserName = "";
            CurrentUserTypeId = new int();
        }
    }
}