using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WholesaleStore.Models;

namespace WholesaleStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new WHOLESALE_STOREEntities())
            {
                var LoggedUserDB = context.LOGGEDUSER.First();

                if (LoggedUserDB.VALUE != null)
                {
                    var model = new HomeModel();
                    var user = context.USER.Find(LoggedUserDB.VALUE);
                    var usertype = new UserType() { Name = user.USERTYPE.NAME };

                    model.LogUser = new User() { Id = user.ID, Name = user.NAME, Usertype = usertype };

                    var view = View(model);
                    view.ViewBag.userId = LoggedUserDB.VALUE;

                    return view;
                }
                else
                {
                    return RedirectToAction("LoginPage", "Login");
                }
            }

        }
    }
}
