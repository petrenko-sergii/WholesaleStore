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

                    #region ModelFilling
                    foreach (var cs in context.CUSTOMER.ToList())
                    {
                        model.Customers.Add(new Customer() { Id = cs.ID, Name = cs.NAME, Address = cs.C_ADDRESS, Email = cs.EMAIL });
                    }

                    foreach (var un in context.UNIT.ToList())
                    {
                        model.Units.Add(new Unit() { Id = un.ID, Name = un.NAME });
                    }

                    foreach (var ct in context.CATEGORY.ToList())
                    {
                        model.Categories.Add(new Category() { Id = ct.ID, Name = ct.NAME });
                    }

                    foreach (var pr in context.PRODUCT.ToList())
                    {
                        model.Products.Add(new Product() { Id = pr.ID, Name = pr.NAME, BarCode = pr.BARCODE, Price = pr.PRICE, Amount = pr.AMOUNT });
                    }
                    #endregion
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
