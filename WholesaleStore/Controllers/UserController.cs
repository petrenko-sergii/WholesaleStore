using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WholesaleStore.Models;

namespace WholesaleStore.Controllers
{
    public class UserController : Controller
    {
        public ActionResult ShowUsers()
        {
            var model = new UsersModel();

            using (var context = new WHOLESALE_STOREEntities())
            {
                foreach (var temp in context.USER.ToList())
                {
                    var usertype = new UserType() { Name = temp.USERTYPE.NAME };

                    model.Users.Add(new User() { Id = temp.ID, Name = temp.NAME, Usertype = usertype });
                }
                model.Users = model.Users.OrderBy(u => u.Usertype.Name).ToList();
            }
            return View("~/Views/User/ShowUsers.cshtml", model);

        }
    }
}
