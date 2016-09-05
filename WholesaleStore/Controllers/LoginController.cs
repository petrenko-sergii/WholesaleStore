using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WholesaleStore.Models;

namespace WholesaleStore.Controllers
{
    public class LoginController : Controller
    {
        void CheckPasswordIsNull(string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                InsertError errorPassword = new InsertError();
                errorPassword.ErrorMessagePassword = "Enter the password!";
                TempData["errorPassword"] = errorPassword;
            }
        }
        public ActionResult LoginPage(string user)
        {
            #region ErrorMessages
            InsertError errLog = TempData["errorLog"] as InsertError;
            if (errLog != null)
            {
                ViewData["ErrorMessageLog"] = errLog.ErrorMessageLog;
            }

            InsertError errWrongLog = TempData["errorWrongLog"] as InsertError;
            if (errWrongLog != null)
            {
                ViewData["ErrorMessageWrongLog"] = errWrongLog.ErrorMessageWrongLog;
            }

            InsertError errPass = TempData["errorPassword"] as InsertError;
            if (errPass != null)
            {
                ViewData["ErrorMessagePassword"] = errPass.ErrorMessagePassword;
            }
            #endregion

            using (var context = new WHOLESALE_STOREEntities())
            {
               var LoggedUserDB = context.LOGGEDUSER.First();
               LoggedUserDB.VALUE = 0;
            }

            var view = View();
            view.ViewBag.user = user;
            return view;
        }
        public ActionResult CheckLogin(string user, string password)
        {
            using (var context = new WHOLESALE_STOREEntities())
            {
                if (String.IsNullOrEmpty(user))
                {
                    InsertError errorLog = new InsertError();
                    errorLog.ErrorMessageLog = "Enter user!";
                    TempData["errorLog"] = errorLog;
                    CheckPasswordIsNull(password);

                    return RedirectToAction("LoginPage", "Login");
                 }

                if (context.USER.Any(u => u.NAME.Contains(user)))
                {
                    var userDB = context.USER.Single(u => u.NAME.Contains(user));

                    if (String.IsNullOrEmpty(password))
                    {
                        InsertError errorPassword = new InsertError();
                        errorPassword.ErrorMessagePassword = "Enter the password!";
                        TempData["errorPassword"] = errorPassword;

                        return RedirectToAction("LoginPage", "Login", new { user });
                    }

                    if (userDB.PASSWORD.ToString() == password)
                    {
                        var LoggedUserDB = context.LOGGEDUSER.First();
                        LoggedUserDB.VALUE = userDB.ID;
                        context.SaveChanges();

                        return this.RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        InsertError errorPassword = new InsertError();
                        errorPassword.ErrorMessagePassword = "Wrong password! Try again!";
                        TempData["errorPassword"] = errorPassword;

                        return RedirectToAction("LoginPage", "Login", new { User = user });
                    }
                }
                else
                {
                    InsertError errorWrongLog = new InsertError();
                    errorWrongLog.ErrorMessageWrongLog = "Check user name!";
                    TempData["errorWrongLog"] = errorWrongLog;

                    return RedirectToAction("LoginPage", "Login", new { user });
                }
            }
        }
    }
}
